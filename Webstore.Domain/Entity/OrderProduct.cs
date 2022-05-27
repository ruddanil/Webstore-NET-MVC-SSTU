using System;
using System.Collections.Generic;
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
    }
}
