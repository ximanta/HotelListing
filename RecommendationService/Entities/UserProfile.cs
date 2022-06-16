using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace RecommendationService.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string EmailId { get; set; }
        [Required]
        public char Gender { get; set; }
        public int Age { get; set; }

        public string country { get; set;  }
    }
}
