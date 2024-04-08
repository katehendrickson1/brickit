using System.Runtime.InteropServices;

namespace brickit.Models
{
    public class Order
    {
        
        public int transactionID { get; set; }
        public Customer customer { get; set; }  //gateway to customers table..
        public string date { get; set; }
        public string dayOfWeek { get; set; }
        public int time { get; set; }   
        public string entryMode { get; set; }
        public int amount { get; set; }
        public string typeOfTransaction { get; set; }
        public string shippingAddress { get; set; }
        public string bank { get; set; }
        public string typeOfCard { get; set; }
        public int fraud { get; set; }
        





    }
}
