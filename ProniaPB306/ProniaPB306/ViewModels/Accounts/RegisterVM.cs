using System.ComponentModel.DataAnnotations;

namespace ProniaPB306.ViewModels
{
    public class RegisterVM
    {
        [MinLength(4)]
        public string UserName { get; set; }
        [MaxLength(128)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [MinLength(3)]
        [MaxLength(25)]
        public string Name { get; set; }
        [MinLength(3)]
        [MaxLength(25)]
        public string Surname { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
