using System.ComponentModel.DataAnnotations.Schema;

namespace BizLand.Models
{
    public class Member
    {
        public int Id { get; set; }
        public int? PositionId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string LinkedinUrl { get; set; }
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public Position? Position { get; set; }


    }
}
