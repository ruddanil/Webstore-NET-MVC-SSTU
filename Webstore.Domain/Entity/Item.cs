using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webstore.Domain.Entity
{
    public class Item
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
