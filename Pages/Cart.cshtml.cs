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

        [BindProperty(SupportsGet = true)]
        public int product_ID { get; set; }
        public Cart? Cart { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost(int product_ID)
        {
            // Use the product_ID parameter received from the form submission
            Product prod = _repo.Products.FirstOrDefault(x => x.product_ID == product_ID);

            if (prod != null)
            {
                // Add product to cart or perform other actions
                Cart = new Cart();

                Cart.AddItem(prod, 1);

                // Redirect to a different page after adding the product
                return RedirectToPage("/Cart"); // Redirect to the home page for example
            }

            // Handle invalid product ID or other errors
            return RedirectToPage("/Error"); // Redirect to an error page
        }
        //public void OnPost(int product_ID) 
        //{
        //    Product prod = _repo.Products
        //        .FirstOrDefault(x => x.product_ID == product_ID);

        //    Cart = new Cart();

        //    Cart.AddItem(prod, 1);
        //}
    }
}
