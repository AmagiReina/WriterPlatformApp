using System.ComponentModel.DataAnnotations;


namespace WriterPlatformApp.DAL.Entities
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        public string GenreName { get; set; }
    }
}
