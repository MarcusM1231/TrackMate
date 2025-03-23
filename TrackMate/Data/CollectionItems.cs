namespace TrackMate.Data
{
    public class CollectionItems
    {
        public Guid ItemID { get; set; }
        public Guid CollectionID { get; set; }
        public int Status { get; set; }
        public DateTime DateAdded { get; set; }
        public string CreatedBy { get; set; }
        public string ItemName { get; set; }
        public DateTime? DateCompleted { get; set; }
    }
}
