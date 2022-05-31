using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webstore.Domain.Entity
{
   
    public class Order
    {
        public Order()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public Order(Guid idUser, DateTime date)
        {
            IdUser = idUser;
            Date = date;
        }

        public Guid IdOrder { get; set; }
        public Guid IdUser { get; set; }
        public DateTime Date { get; set; }

        public virtual User IdUserNavigation { get; set; } = null!;
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
