using brickit.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace brickit.Pages
{
    public class CartModel : PageModel
    {
        private ILegoRepository _repo;
        public CartModel(ILegoRepository repo) 
        { 
            _repo = repo;
        }

        public Cart? Cart { get; set; }
        public void OnGet()
        {
        }

        public void OnPost(int product_ID) 
        {
            Product prod = _repo.Products
                .FirstOrDefault(x => x.product_ID == product_ID);

            Cart = new Cart();

            Cart.AddItem(prod, 1);
        }
    }
}
