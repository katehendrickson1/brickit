//using AspNetCore;
using brickit.Models;
using brickit.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using System.Diagnostics;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System;

namespace brickit.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILegoRepository _repo;
        private readonly LegoDbContext _context; // Assuming ApplicationDbContext is your EF DbContext
        private readonly InferenceSession _session; // ONNX Runtime InferenceSession

        public HomeController( ILegoRepository temp, LegoDbContext context)
        {
            _repo = temp;
            _context = context;

            // Load the ONNX model
            _session = new InferenceSession("C:\\Users\\haile\\source\\repos\\brickit\\fraud_pred_edit.onnx");

        }
        

        public IActionResult Index()
        {
        var limit = 2;

        ViewBag.Customers = _repo.Customers.ToList().Take(limit);
        var orders = _repo.Orders.ToList().Take(limit);
        return View(orders);
        }

        public IActionResult testView()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Welcome()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _repo.Add(product);
            _repo.SaveChanges();
            return View();
        }

        public IActionResult AdminProductList()
        {
            var products = _repo.Products.ToList();

            return View(products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult EditProd(int id)
        {
            var recordToEdit = _repo.Products
                .Single(x => x.product_ID == id);
            return View("AddProduct", recordToEdit);
        }
        [HttpPost]
        public IActionResult EditProd(Product prod)
        {
            _repo.Update(prod);
            _repo.SaveChanges();

            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public IActionResult DeleteProd(int id)
        {
            var recordToDelete = _repo.Products
                .Single(x => x.product_ID == id);
            return View(recordToDelete);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProd(Product prod)
        {
            //var product = await _repo.Products.FirstOrDefaultAsync(p => p.product_ID == id);
            var product = prod;
  
            _repo.Remove(product); // Correctly removes the product from the repository
            _repo.SaveChanges();

            return RedirectToAction("ProductList"); // Assuming you have a ProductList action
        }


        //[HttpGet]
        //public IActionResult SignUp()
        //{
        //    return View("SignUp", new User());
        //}

        //[HttpPost]
        //public IActionResult SignUp(User response)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var res = response;
        //        //_repo.users.Add(res);
        //        _repo.SaveChanges();

        //        return View("Login", response); // add record to database
        //    }
        //    else
        //    {
        //        return View(response);
        //    }

        //}

        //public IActionResult Orders()
        //{
        //    var orders = _repo.Orders
        //        .Join(_repo.LineItems, o => o.transaction_ID, li => li.transaction_ID, (o, li) => new { Order = o, LineItem = li })
        //        .Join(_repo.Products, j => j.LineItem.product_ID, p => p.product_ID, (j, p) => new { j.Order, j.LineItem, Product = p })
        //        .Join(_repo.Customers, j => j.Order.customer_ID, c => c.customer_ID, (j, c) => new { j.Order, j.LineItem, j.Product, Customer = c })
        //        .Select(result => new OrderViewModel
        //        {
        //            TransactionID = result.Order.transaction_ID,
        //            Date = (DateTime)result.Order.date,
        //            CustomerName = result.Customer.first_name + " " + result.Customer.last_name,
        //            ProductName = result.Product.name,
        //            Quantity = (int)result.LineItem.qty,
        //            Price = (decimal)result.Product.price,
        //            // Add other properties you want to display
        //        })
        //        .ToList();

        //    return View(orders);
        //}

        [HttpGet]
        public IActionResult CustProductList()
        {
            var products = _repo.Products.ToList();

            return View(products);
        }

        [HttpGet]
        public IActionResult SeeProd(int id)
        {
            var recordToSee = _repo.Products
                .Single(x => x.product_ID == id);
            return View("IndividualProduct", recordToSee);
        }

        [HttpPost]
        public IActionResult Predict(InferenceSession session, int day_of_week_Mon, int day_of_week_Sat, int day_of_week_Sun, int day_of_week_Thu, int day_of_week_Tue, int day_of_week_Wed, int time_1, int time_2, int time_3, int time_4, int time_5, int time_6, int time_7, int time_8, int time_9, int time_10, int time_11, int time_12, int time_13, int time_14, int time_15, int time_16, int time_17, int time_18, int time_19, int time_20, int time_21, int time_22, int time_23, int time_24, int entry_mode_PIN, int entry_mode_Tap, int type_of_transaction_Online, int type_of_transaction_POS, int country_of_transaction_India, int country_of_transaction_Russia, int country_of_transaction_USA, int country_of_transaction_United_Kingdom, int shipping_address_India, int shipping_address_Russia, int shipping_address_USA, int shipping_address_United_Kingdom, int bank_HSBC, int bank_Halifax, int bank_Lloyds, int bank_Metro, int bank_Monzo, int bank_RBS, int type_of_card_Visa)
        {
            var class_type_dict = new Dictionary<int, string>
    {
        { 0, "not fraud" },
        { 1, "fraud" }
    };

            try
            {
                var input = new List<float> { day_of_week_Mon, day_of_week_Sat, day_of_week_Sun, day_of_week_Thu, day_of_week_Tue, day_of_week_Wed, time_1, time_2, time_3, time_4, time_5, time_6, time_7, time_8, time_9, time_10, time_11, time_12, time_13, time_14, time_15, time_16, time_17, time_18, time_19, time_20, time_21, time_22, time_23, time_24, entry_mode_PIN, entry_mode_Tap, type_of_transaction_Online, type_of_transaction_POS, country_of_transaction_India, country_of_transaction_Russia, country_of_transaction_USA, country_of_transaction_United_Kingdom, shipping_address_India, shipping_address_Russia, shipping_address_USA, shipping_address_United_Kingdom, bank_HSBC, bank_Halifax, bank_Lloyds, bank_Metro, bank_Monzo, bank_RBS, type_of_card_Visa };
                var inputTensor = new DenseTensor<float>(input.ToArray(), new[] { 1, input.Count });
                var inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("float_input", inputTensor)
        };

                using (var results = session.Run(inputs)) // makes the prediction with the inputs from the form (i.e. class_type 1-7)
                {
                    var prediction = results.FirstOrDefault(item => item.Name == "output_label")?.AsTensor<long>().ToArray();
                    if (prediction != null && prediction.Length > 0)
                    {
                        // Use the prediction to get the animal type from the dictionary
                        var fraud = class_type_dict.GetValueOrDefault((int)prediction[0], "Unknown");
                        ViewBag.Prediction = fraud;
                    }
                    else
                    {
                        ViewBag.Prediction = "Error: Unable to make a prediction.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Prediction = $"Error: {ex.Message}";
            }

            return View("Index");
        }



    }
}
