using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webstore.Domain.Entity
{
   
    public class OrderProduct
    {
        public Guid IdOrderProduct { get; set; }
        public Guid IdOrder { get; set; }
        public Guid IdProduct { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerUnit { get; set; }

        public virtual Order IdOrderNavigation { get; set; } = null!;

        public virtual Product IdProductNavigation { get; set; } = null!;

        public OrderProduct(Guid idOrder, Guid idProduct, int quantity, decimal pricePerUnit)
        {
            IdOrder = idOrder;
            IdProduct = idProduct;
            Quantity = quantity;
            PricePerUnit = pricePerUnit;
        }
    }
}
