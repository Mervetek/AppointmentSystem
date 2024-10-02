using System;
using System.Collections.Generic;
using AppointmentSystem.Utils;
using Avalonia.Collections;
using ReactiveUI.Fody.Helpers;
using System.IO;
namespace AppointmentSystem.ViewModels;

public class AppointmentListViewModel : ViewModelBase
{
    public AppointmentListViewModel()
    {
        var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        Appointments = FileOperations.GetAllAppointments(Path.Combine(documentsPath + "/appointment.json"));
    }

    [Reactive] public AvaloniaList<AppointmentRegisterViewModel>? Appointments { get; set; }
}