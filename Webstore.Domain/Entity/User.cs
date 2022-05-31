using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webstore.Domain.Enum;

namespace Webstore.Domain.Entity
{
    public class User
    {
        [Key]
        public Guid Id_user { get; set; }
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public int Age { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; } = null!;
        public Role Role { get; set; }
        public ICollection<Order> Orders { get; set; } 
       
    }
}
