namespace brickit.Models.ViewModels
{
    public class SignUpViewModel
    {
        public IQueryable<User> Users { get; set; }
        public IQueryable<Customer> Customers { get; set; }
    }
}
