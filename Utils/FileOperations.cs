using System;
using System.Collections.Generic;
using System.IO;
using AppointmentSystem.ViewModels;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Media;
using Newtonsoft.Json;

namespace AppointmentSystem.Utils;

public static class FileOperations
{
    public static AvaloniaList<AppointmentRegisterViewModel>? GetAllAppointments(string filePath)
    {
        if (File.Exists(filePath))
        {
            var jsonString = File.ReadAllText(filePath);
            var list = JsonConvert.DeserializeObject<AvaloniaList<AppointmentRegisterViewModel>>(jsonString);
            return list;
        }
        else
            return new AvaloniaList<AppointmentRegisterViewModel>();
    }

    public static IBrush CheckName(string? name)
    {
        return string.IsNullOrEmpty(name) ? Brushes.Red : Brushes.Chartreuse;
    }
}