using System;
using AppointmentSystem.Enums;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace AppointmentSystem.ViewModels;

public class AppointmentItemViewModel : ViewModelBase
{
    public AppointmentItemViewModel()
    {
        this.WhenAnyValue(model => model.AppointmentType).Subscribe(type =>
        {
            IsOnline = type == Enums.AppointmentType.Online;
        });
        this.WhenAnyValue(model => model.AppointmentDate).Subscribe(date =>
        {
            AppointmentDateStr = date.ToString();
        });
    }

    [Reactive] public string? CustomerName { get; set; }
    [Reactive] public string? CustomerLastName { get; set; }
    [Reactive] public bool IsFatura { get; set; }
    [Reactive] public AppointmentType? AppointmentType { get; set; }
    [Reactive] public DateTime? AppointmentDate { get; set; } = DateTime.Now;
    [Reactive] public string? AppointmentDateStr { get; set; }
    
    [Reactive] public bool IsOnline { get; set; }
}