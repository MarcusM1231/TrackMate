using Microsoft.EntityFrameworkCore;
using TrackMate.Data;

public class CollectionService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public CollectionService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    // Add Collection
    public async Task AddCollectionAsync(Collections collection)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        dbContext.Collections.Add(collection);
        await dbContext.SaveChangesAsync();
    }

    // Get Collections
    public async Task<List<Collections>> GetCollectionsAsync()
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        return await dbContext.Collections.ToListAsync();
    }

    // Add Collection Item
    public async Task AddCollectionItemAsync(CollectionItems collectionItem)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        dbContext.CollectionItems.Add(collectionItem);
        await dbContext.SaveChangesAsync();
    }

    // Get Collection Items
    public async Task<List<CollectionItems>> GetCollectionItemsAsync(Guid collectionId)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        return await dbContext.CollectionItems.Where(x => x.CollectionID == collectionId).ToListAsync();
    }

    public async Task UpdateCollectionItemAsync(CollectionItems item)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        var existingItem = await dbContext.CollectionItems.FindAsync(item.ItemID);

        if (existingItem != null)
        {
            // Update other fields
            existingItem.ItemName = item.ItemName;
            existingItem.Status = item.Status;
            existingItem.CreatedBy = item.CreatedBy;
            existingItem.DateAdded = item.DateAdded;

            // Set DateCompleted if status is 'Completed'
            if (item.Status == 2 && !existingItem.DateCompleted.HasValue)
            {
                existingItem.DateCompleted = DateTime.Now;
            }
            // Remove DateCompleted if status is not 'Completed'
            else if (item.Status != 2)
            {
                existingItem.DateCompleted = null;
            }

            // Save changes to the database
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteCollectionItemAsync(Guid itemId)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        var item = await dbContext.CollectionItems.FindAsync(itemId);
        if (item != null)
        {
            dbContext.CollectionItems.Remove(item);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteCollectionAsync(Guid collectionId)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        var collection = await dbContext.Collections.FindAsync(collectionId);
        if (collection != null)
        {
            // Remove all collection items associated with the collection
            var collectionItems = await dbContext.CollectionItems.Where(x => x.CollectionID == collectionId).ToListAsync();
            dbContext.CollectionItems.RemoveRange(collectionItems);

            // Remove the collection itself
            dbContext.Collections.Remove(collection);
            await dbContext.SaveChangesAsync();
        }
    }
}