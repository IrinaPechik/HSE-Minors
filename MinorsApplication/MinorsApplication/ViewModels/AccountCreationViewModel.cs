using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace MinorsApplication.ViewModels
{
    public partial class AccountCreationViewModel : ObservableObject
    {
        [ObservableProperty]
        string userName;
        [ObservableProperty]
        string password;
        [ObservableProperty]
        string repeatedPassword;
        string status;

        [RelayCommand]
        public async void Register(string name)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password)
                || string.IsNullOrEmpty(repeatedPassword))
            {
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    App.Current.MainPage.DisplayAlert("Ошибка", "Неверный логин или пароль", "OK");
                });
                return;
            }
            if (password != repeatedPassword)
            {
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    App.Current.MainPage.DisplayAlert("Ошибка", "Пароли не совпадают", "OK");
                });
                return;
            }
            status = Functions.Functions.CheckStatus(name);
            if (status == string.Empty)
            {
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    App.Current.MainPage.DisplayAlert("Ошибка", "Неверный адрес электронной почты", "OK");
                });
                return;
            }
            // Шифруем пароль.
            password = Functions.Functions.hashPassword(password);
            // Если статус - студент, перекидываем на заполнение данных для студента.
            if (status == "Студент")
            {
                var doesExist = await App.UserInfoService.GetStudentByEmail(name);
                if (doesExist != null)
                {
                    await MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        App.Current.MainPage.DisplayAlert("Ошибка", "Пользователь с таким адресом электронной почты" +
                            " уже был зарегистрирован.", "OK");
                    });
                    return;
                }
                await Shell.Current.GoToAsync($"{nameof(StudentInfoPage)}?Email={name}&Password={password}");
            }
            else if (status == "Преподаватель")
            {
                var doesExist = await App.UserInfoService.GetTeacherByEmail(name);
                if (doesExist != null)
                {
                    await MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        App.Current.MainPage.DisplayAlert("Ошибка", "Пользователь с таким адресм электронной почты" +
                            " уже был зарегистрирован", "OK");
                    });
                    return;
                }
                // Если статус - преподаватель, перекидываем на заполнение данных для преподавателя.
                await Shell.Current.GoToAsync($"{nameof(TeacherInfoPage)}?Email={name}&Password={password}");

            }
        }
    }
}
