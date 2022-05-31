using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Webstore.DAL;
using Webstore.Domain.Entity;
using Webstore.Domain.Helper;
using Webstore.Domain.ViewModel.Cart;
using Webstore.Service.Interfaces;

namespace Webstore.Controllers
{
    public class CartController : Controller
    {
        public List<Item> items { get; set; }
        public decimal total { get; set; }

        private readonly ApplicationDbContext _context;
        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(CartViewModel model)
        {
            return View();
        }

        public IActionResult Init()
        {
            if (User.Identity.IsAuthenticated)
            {
                items = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                if (items != null)
                {
                    total = items.Sum(i => i.Product.Price * i.Quantity);
                }
            }
            else
            {
                items = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                for (var i = 0; i < items.Count; i++)
                {
                    items.RemoveAt(i);
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", items);
            }
            return View("Index", new CartViewModel(items, total));
        }
        [HttpPost]
        public IActionResult AddToCart(Guid id_product, int quantity)
        {
            try
            {
                Product product = _context.Product.FirstOrDefault(x => x.Id_product == id_product && x.Quantity >= 1);
                items = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                if (product == null || product.Quantity <= 0)
                {
                    return View("Error", $"This item is out of stock");
                }
                else
                {
                    if (items == null)
                    {
                        items = new List<Item>();
                        items.Add(new Item
                        {
                            Product = product,
                            Quantity = quantity
                        });
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", items);
                    }
                    else
                    {
                        int index = Exists(items, id_product);
                        if (index == -1)
                        {
                            items.Add(new Item
                            {
                                Product = product,
                                Quantity = quantity
                            });
                        }
                        else if (quantity <= product.Quantity && (items[index].Quantity + quantity <= product.Quantity))
                        {
                            items[index].Quantity += quantity;
                        }
                        else
                        {
                            items[index].Quantity += product.Quantity - quantity;
                        }
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", items);
                    }
                }
                return RedirectToAction("ReadProducts", "Product");
            }
            catch (Exception ex)
            {
                return View("Error", $"This product is no longer available");
            }
        }

        public IActionResult DeleteFromCart(Guid id_product)
        {
            items = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = Exists(items, id_product);
            items.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", items);
            return RedirectToAction("Init");
        }

        public IActionResult Update(int[] quantities)
        {
            items = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (var i = 0; i < items.Count; i++)
            {
                items[i].Quantity = quantities[i];
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", items);
            return RedirectToAction("Init");
        }

        public IActionResult CreateOrder()
        {
            try
            {
                items = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");

                User user = _context.User.FirstOrDefault(x => x.Email == (User.Identity.Name));

                DateTime date = DateTime.Now;

                Order order = new(user.Id_user, date);
                _context.Add(order);
                _context.SaveChanges();
                Guid id_order = order.IdOrder;
                for (var i = 0; i < items.Count; i++)
                {
                    try
                    {
                        Product product = items[i].Product;
                        if (product.Quantity > 0 && (product.Quantity - items[i].Quantity) >= 0)
                        {
                            Guid id_product = product.Id_product;
                            int quantity = items[i].Quantity;
                            decimal pricePerUnit = product.Price;
                            OrderProduct orderProduct = new(id_order, id_product, quantity, pricePerUnit);
                            _context.Add(orderProduct);
                            _context.SaveChanges();

                            product.Quantity -= items[i].Quantity;
                            _context.Update(product);
                            _context.SaveChanges();
                        }
                        else
                        {
                            return View("Error", $"One or more items from the order are no longer available, but the available items are purchased");
                        }
                    }
                    catch (Exception ex)
                    {
                        return View("Error", $"One or more items from the order are no longer available, but the available items are purchased");
                    }
                }
                for (var i = 0; i < items.Count; i++)
                {
                    items.RemoveAt(i);
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", items);
                return RedirectToAction("ReadProducts", "Product");
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }

        }

        private int Exists(List<Item> cart, Guid id)
        {
            for (var i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id_product == id)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
