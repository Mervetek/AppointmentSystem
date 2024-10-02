using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Text.Json;
using System.Threading.Tasks;
using AppointmentSystem.Enums;
using AppointmentSystem.Utils;
using AppointmentSystem.Views;
using Avalonia.Collections;
using Avalonia.Media;
using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace AppointmentSystem.ViewModels;

public class AppointmentRegisterViewModel : ViewModelBase
{
    public AppointmentRegisterViewModel()
    {
        AppointmentDate = DateTime.Now;

        Save = ReactiveCommand.CreateFromTask(SaveToJson);
        Cancel = ReactiveCommand.Create(() => { });

        this.WhenAnyValue(model => model.CustomerName).Subscribe(s =>
        {
            if(string.IsNullOrEmpty(s))
                BackgroundColorName = Brushes.Transparent;
        });
        
        this.WhenAnyValue(model => model.CustomerLastName).Subscribe(s =>
        {
            if(string.IsNullOrEmpty(s))
                BackgroundColorSurname = Brushes.Transparent;
        });
    }

    private async Task SaveToJson()
    {
        BackgroundColorName = FileOperations.CheckName(CustomerName);
        BackgroundColorSurname = FileOperations.CheckName(CustomerLastName);

        if (BackgroundColorName == Brushes.Red || BackgroundColorSurname == Brushes.Red)
        {
            var messageBox = new MessageBox
            {
                DataContext = new MessageBoxViewModel
                {
                    Title = "Hata",
                    Content = "İsim Soyisim alanları boş bırakılamaz."
                }
            };
            messageBox.Show();
            return;
        }
        
        //dosya yolu olusturuluyor
        var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var appointments = FileOperations.GetAllAppointments(Path.Combine(documentsPath, "appointment.json"));

        //anlık veri alınıyor
        var appointment = new AppointmentRegisterViewModel
        {
            CustomerName = CustomerName,
            CustomerLastName = CustomerLastName,
            AppointmentDate = AppointmentDate,
            IsFatura = IsFatura,
            AppointmentType = AppointmentType
        };
        
        //liste olusturuluyor/ekleniyor
        appointments ??= new AvaloniaList<AppointmentRegisterViewModel>();
        appointments.Add(appointment);

        if (!Equals(BackgroundColorName, Brushes.Red) && !Equals(BackgroundColorSurname, Brushes.Red))
        {
            var updatedJsonString =
                JsonSerializer.Serialize(appointments, new JsonSerializerOptions { WriteIndented = true });
            
            // JSON dosyasına veri yazma
            await File.WriteAllTextAsync(Path.Combine(documentsPath, "appointment.json"), updatedJsonString);
            
            //refresh list
            FileOperations.GetAllAppointments(Path.Combine(documentsPath, "appointment.json"));
        }
    }

    [Reactive] public string? CustomerName { get; set; }
    [Reactive] public string? CustomerLastName { get; set; }
    [Reactive] public bool IsFatura { get; set; }
    [Reactive] public AppointmentType? AppointmentType { get; set; }
    [Reactive] public DateTime? AppointmentDate { get; set; }

    [Reactive] public IBrush BackgroundColorName { get; set; }
    [Reactive] public IBrush BackgroundColorSurname { get; set; }
    
    public ReactiveCommand<Unit, Unit> Cancel { get; }
    public ReactiveCommand<Unit, Unit> Save { get; }
}