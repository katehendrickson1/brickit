namespace brickit.Models
{
    public interface ILegoRepository
    {
        List<Customer> Customers { get; }
        List<Order> Orders { get; }
        List<Product> Products { get; }
        List<lineItem> LineItems { get; }

    }
}
