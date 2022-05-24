using System.ComponentModel.DataAnnotations;

namespace Test.Host.DTOs
{
    public class UpdateUserDTO
    {
        [Required]
        public string BirthDate { get; set; }

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(200)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(200)]
        public string FirstName { get; set; }
    }

    public enum UpdateUserResult
    {
        Success,
        UserNotFound,
        Failure
    }
}
