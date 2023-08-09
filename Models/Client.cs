namespace CustomerDemo.Models;

public partial class Client
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int SourceId { get; set; }
    
    // public Client() { }
    //
    // public Client(string firstName, string lastName, string address, string email, string phone, int source)
    // {
    //     FirstName = firstName;
    //     LastName = lastName;
    //     Address = address;
    //     Email = email;
    //     Phone = phone;
    //     Source = source;
}
