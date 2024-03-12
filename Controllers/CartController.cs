using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TemporalBoxApi.JwtContext;
using TemporalBoxApi.Models;

namespace TemporalBoxApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly Context _context;
        public CartController(Context context)
        {
            _context = context;
        }

        //add to cart method
        [HttpPost("AddToCart")]
        public void AddCart(CartItem cartItem)
        {
            if(cartItem.CartId != null && cartItem.ProductId!=0)
            {
                var product = _context.Products.Find(cartItem.ProductId);
                cartItem.Price = product.ProductPrice;
                 cartItem.DateCreated = DateTime.Now;
                _context.CartItems.Add(cartItem);
                _context.SaveChanges();
            }
        }

        //get cart items
        [HttpGet("GetCart")]
        public List<CartItem> GetCart(string cartId) { 
         var data = _context.CartItems.Where(x=> x.CartId == cartId).Include(m=>m.Product).ToList();
         return data;
        }

        [HttpPut("UpdateCart")]
        public void UpdateCart(CartItem obj)
        {
           var cartItem = _context.CartItems.Find(obj.ItemId);
        
            if (cartItem != null)
            {
                cartItem.Price = obj.Price;
                cartItem.Quantity = obj.Quantity;
                _context.SaveChanges();
            }
        }

        [HttpDelete("DeleteCart")]
        public void DeleteCart(int id)
        {
            if(id != 0) { 
            var cartItem = _context.CartItems.Find(id);
            _context.CartItems.Remove(cartItem);
            _context.SaveChanges();

            }
        }


    }
}
