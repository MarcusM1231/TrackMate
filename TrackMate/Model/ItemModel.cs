using System.ComponentModel.DataAnnotations;

namespace TrackMate.Model
{
    public class ItemModel
    {
        [Required(ErrorMessage = "Collection Name is required.")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Created By is required.")]
        public string CreatedBy { get; set; }

    }
}
