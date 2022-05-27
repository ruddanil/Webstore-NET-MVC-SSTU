using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webstore.Domain.Entity
{
    public class Product
    {
        [Key]
        public Guid Id_product { get; set; } 
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Img { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool Trashed { get; set; }
    }
}
