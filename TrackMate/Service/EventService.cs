namespace TrackMate.Service
{
    public class EventService
    {
        // Event to notify changes
        public event Action OnCollectionChanged;

        // Trigger the collection change event
        public void NotifyCollectionChanged()
        {
            OnCollectionChanged?.Invoke();
        }
    }

}
