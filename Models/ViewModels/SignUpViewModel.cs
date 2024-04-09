namespace brickit.Models.ViewModels
{
    public class SignUpViewModel
    {
        public IQueryable<User> users { get; set; } 
        public IQueryable<Customer> Customer { get; set; } 
    }
}
