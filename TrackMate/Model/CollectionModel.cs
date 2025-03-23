using System.ComponentModel.DataAnnotations;


namespace TrackMate.Model
{
    public class CollectionModel
    {
        [Required(ErrorMessage = "Collection Name is required.")]
        public string CollectionName { get; set; }

        [Required(ErrorMessage = "Created By is required.")]
        public string CreatedBy { get; set; }
    }
}
