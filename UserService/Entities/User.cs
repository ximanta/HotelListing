using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UserService.entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string EmailId { get; set; }
        [Required]
        [JsonIgnore]
        public string Password { get; set; }
        [Required]
        public char Gender { get; set; }
        public int Age { get; set; }

        public static implicit operator Task<object>(User v)
        {
            throw new NotImplementedException();
        }
    }
}
