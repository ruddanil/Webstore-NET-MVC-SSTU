using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webstore.Domain.Entity
{
   
    public class Order
    {
        private Order(Guid idUser, DateTime date)
        {
            IdUser = idUser;
            Date = date;
        }

        public static Order OrderConstr(Guid idUser, DateTime date)
        {
            return new Order(idUser, date);
        }

        public Guid IdOrder { get; set; }
        public Guid IdUser { get; set; }
        public DateTime Date { get; set; }

        public virtual User IdUserNavigation { get; set; } = null!;
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        
        public Order(Guid idOrder, Guid idUser, DateTime date, User idUserNavigation, ICollection<OrderProduct> orderProducts)
        {
            IdOrder = idOrder;
            IdUser = idUser;
            Date = date;
            IdUserNavigation = idUserNavigation;
            OrderProducts = orderProducts;
        }
    }
}
