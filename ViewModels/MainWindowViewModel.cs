using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        BuildAppointmentList(Appointments);
        
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

    [Reactive] public AvaloniaList<AppointmentItemViewModel>? AppointmentList { get; set; }

    public ReactiveCommand<Unit, Unit> CreateAppointment { get; }
    public ReactiveCommand<Unit, Unit> ListAppointments { get; }

    private void BuildAppointmentList(AvaloniaList<AppointmentRegisterViewModel>? appointments)
    {
        if (Appointments == null)
            return;
        
        foreach (var item in Appointments.Select(appointment => new AppointmentItemViewModel
                 {
                     CustomerName = appointment.CustomerName,
                     CustomerLastName = appointment.CustomerLastName,
                     AppointmentDate = appointment.AppointmentDate,
                     IsFatura = appointment.IsFatura,
                     AppointmentType = appointment.AppointmentType
                 }))
        {
        }
    }
}