namespace brickit.Models
{
    public interface ILegoRepository
    {
        IQueryable<Customer> Customer { get; }
        IQueryable<Order> Orders { get; }
        IQueryable<Product> Products { get; }
        IQueryable<lineItem> LineItems { get; }
        IQueryable<User> users { get; }

    }
}
