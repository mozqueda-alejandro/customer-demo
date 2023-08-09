using System;
using System.Linq;
using CustomerDemo.Models;
using FluentValidation;

namespace CustomerDemo.Validators;

public class ClientValidator : AbstractValidator<Client>
{
    public ClientValidator()
    {
        RuleFor(client => client.FirstName).NotEmpty().Must(BeAValidName).WithMessage("First name is required.");
        RuleFor(client => client.LastName).NotEmpty().Must(BeAValidName).WithMessage("Last name is required.");
        RuleFor(client => client.Address).NotEmpty().WithMessage("Address is required.");
        RuleFor(client => client.Email).NotEmpty().EmailAddress().WithMessage("Valid email is required.");
        RuleFor(client => client.Phone).NotEmpty().WithMessage("Phone number is required.");
        // RuleFor(client => client.SourceId).NotEmpty().WithMessage("Source is required.");
        // Add more validation rules as needed
    }
    
    protected bool BeAValidName(string name)
    {
        name = name.Replace(" ", "");
        name = name.Replace("-", "");
        return name.All(char.IsLetter);
    }
}