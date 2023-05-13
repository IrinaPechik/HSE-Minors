using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MinorsApplication.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinorsApplication.ViewModels
{
    [QueryProperty(nameof(Email), "Email")]
    [QueryProperty(nameof(Password), "Password")]
    public partial class TeacherInfoPageViewModel : ObservableObject
    {
        [ObservableProperty]
        string email;
        [ObservableProperty]
        string password;
        [ObservableProperty]
        string fullName;
        [ObservableProperty]
        string minor;
        [ObservableProperty]
        int? numberOfSeats;
        [ObservableProperty]
        int? numberOfVacantSeats;

        [RelayCommand]
        public async void SaveData()
        {
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(minor)
                || numberOfSeats == null || numberOfVacantSeats == null
                || numberOfSeats < 0 || numberOfSeats < 0)
            {
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    App.Current.MainPage.DisplayAlert("Ошибка", "Неверные данные", "Ok");
                });
                return;
            }
            TeacherInfo currentTeacher = new TeacherInfo(email, password, fullName, minor, (int)numberOfSeats, (int)numberOfVacantSeats);
            await App.UserInfoService.AddNewTeacher(currentTeacher);
            var navigationParametr = new Dictionary<string, object>
            {
                {"Teacher", currentTeacher }
            };
            await Shell.Current.GoToAsync($"{nameof(TeacherPersonalAccount)}", navigationParametr);
        }
    }
}
