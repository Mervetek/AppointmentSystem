using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace AppointmentSystem.ViewModels;

public class MessageBoxViewModel : ViewModelBase
{
    public MessageBoxViewModel()
    {
    }
    
    [Reactive] public string Title { get; set; }
    [Reactive] public string Content { get; set; }

}