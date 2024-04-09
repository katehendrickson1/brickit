﻿
namespace brickit.Models
{
    public class EFLegoRepository: ILegoRepository
    {
        private LegoDbContext _context;
        public EFLegoRepository(LegoDbContext temp)
        {
            _context = temp;
        }


        public IQueryable<Customer> Customer => _context.customer;
        public IQueryable<Order> Orders => _context.orders;
        public IQueryable<Product> Products => _context.products;
        public IQueryable<lineItem> LineItems => _context.lineItems;
        public IQueryable<User> users => _context.users;

    }
}