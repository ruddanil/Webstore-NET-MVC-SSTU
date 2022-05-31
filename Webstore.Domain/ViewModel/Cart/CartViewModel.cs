using Webstore.Domain.Entity;

namespace Webstore.Domain.ViewModel.Cart
{
    public class CartViewModel
    {
        public List<Item> Items { get; set; }
        public decimal Total { get; set; }

        public CartViewModel(List<Item> items, decimal total)
        {
            Items = items;
            Total = total;
        }
    }
}
