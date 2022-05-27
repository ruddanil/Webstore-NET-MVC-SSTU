using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webstore.Domain.ViewModel.Product
{
    public class ProductViewModel
    {
        public Guid Id_product { get; set; }

        [Display(Name = "Title")]
        [Required]
        [MinLength(2)]
        public string Title { get; set; } = null!;

        [Display(Name = "Description")]
        [Required]
        [MinLength(2)]
        public string? Description { get; set; }

        [Display(Name = "Image URL")]
        public string? Img { get; set; }

        [Display(Name = "Price")]
        [Required]
        public decimal Price { get; set; }

        [Display(Name = "Quantity")]
        [Required]
        public int Quantity { get; set; }
    }
}
