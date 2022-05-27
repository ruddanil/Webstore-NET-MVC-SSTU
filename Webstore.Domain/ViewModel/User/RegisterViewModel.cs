using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webstore.Domain.ViewModel.User
{
    public class RegisterViewModel
    {

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string? MiddleName { get; set; }

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public int Age { get; set; }

        [Required]
        public string? Phone { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$", ErrorMessage = "Not a valid email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm password")]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string PasswordConfirm { get; set; }
    }
}
