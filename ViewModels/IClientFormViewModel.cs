namespace CustomerDemo.ViewModels;

public interface IClientFormViewModel
{
    string FirstName { get; set; }
    string LastName { get; set; }
    string Address { get; set; }
    string Email { get; set; }
    string Phone { get; set; }
    string Source { get; set; }
}