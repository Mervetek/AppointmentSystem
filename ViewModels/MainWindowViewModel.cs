using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using AppointmentSystem.Utils;
using AppointmentSystem.Views;
using Avalonia.Collections;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace AppointmentSystem.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        Appointments = FileOperations.GetAllAppointments(Path.Combine(documentsPath + "/appointment.json"));
        
        CreateAppointment = ReactiveCommand.Create(() =>
        {
            var newWindow = new AppointmentRegister
            {
                DataContext = NewAppointment
            };
            newWindow.Show();
        });
    }

    [Reactive] public AvaloniaList<AppointmentRegisterViewModel>? Appointments { get; set; }
    [Reactive] private AppointmentRegisterViewModel NewAppointment { get; set; } = new();
    
    public ReactiveCommand<Unit, Unit> CreateAppointment { get; }
    public ReactiveCommand<Unit, Unit> ListAppointments { get; }
}