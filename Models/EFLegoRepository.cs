
namespace brickit.Models
{
    public class EFLegoRepository : ILegoRepository
    {
        private LegoDbContext _context;
        public EFLegoRepository(LegoDbContext temp)
        {
            _context = temp;
        }
        
        
        public List<Customer> Customers => _context.customers.ToList();

        public List<Order> Orders => _context.orders.ToList();

        public List<Product> Products => _context.products.ToList();

        public List<lineItem> LineItems => _context.lineItems.ToList();
    }
}
