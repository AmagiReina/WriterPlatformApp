using System.ComponentModel.DataAnnotations;

namespace WriterPlatformApp.DAL.Entities
{
    public class RatingType
    {
        [Key]
        public int Id { get; set; }

        public int RatingNumber { get; set; }
    }
}
