namespace brickit.Models.ViewModels
{
    public class OrderViewModel
    {
        public int TransactionID { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        // Add other properties you want to display on the Orders.cshtml page
    }

}
