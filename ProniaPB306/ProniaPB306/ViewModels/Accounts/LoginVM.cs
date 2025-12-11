using System.ComponentModel.DataAnnotations;

namespace ProniaPB306.ViewModels
{
    public class LoginVM
    {
        [MaxLength(128)]
        [MinLength(4)]
        public string UserNameOrEmail { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsPersistant { get; set; }

    }
}
