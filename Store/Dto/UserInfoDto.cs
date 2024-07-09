using System.ComponentModel.DataAnnotations;

namespace API.Dto
{
    public class UserInfoDto
    {
        [Required]
        [RegularExpression("^[A-Za-z'-]", ErrorMessage = "Firstname can only contain letters, hyphens, or apostrophes.")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression("^[A-Za-z'-]", ErrorMessage = "Lastname can only contain letters, hyphens, or apostrophes.")]
        public string LastName { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
    }
}
