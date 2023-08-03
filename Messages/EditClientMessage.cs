using CommunityToolkit.Mvvm.Messaging.Messages;
using CustomerDemo.Models;

namespace CustomerDemo.Messages;

public class EditClientMessage : ValueChangedMessage<Client>
{
    public EditClientMessage(Client value) : base(value)
    {
        
    }
}