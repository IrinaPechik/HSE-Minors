using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MinorsApplication.Models;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace MinorsApplication.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {

        [ObservableProperty]
        string userName;
        [ObservableProperty]
        string password;

        [RelayCommand]
        public async void SignIn(string name)
        {
            // Здесь также будем проверять по БД, был ли пользователь зареган.
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    App.Current.MainPage.DisplayAlert("Ошибка", "Заполните все поля", "Ok");
                });
                return;
            }

            var status = Functions.Functions.CheckStatus(userName);
            if (status == "Студент")
            {
                StudentInfo currentStudent = await App.UserInfoService.GetStudentByEmail(userName);
                // Хэшируем введённый пользователем пароль.
                var hashpassword = Functions.Functions.hashPassword(password);
                if (currentStudent == null || currentStudent.Password != hashpassword)
                {
                    await MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        App.Current.MainPage.DisplayAlert("Ошибка", "Неверный логин или пароль", "Ok");
                    });
                    return;
                }
                var navigationParametr = new Dictionary<string, object>
                {
                    {"Student", currentStudent }
                };
                await Shell.Current.GoToAsync($"{nameof(StudentPersonalAccount)}", navigationParametr);
            }
            else if (status == "Преподаватель")
            {
                TeacherInfo currentTeacher = await App.UserInfoService.GetTeacherByEmail(userName);
                // Хэшируем введённый пользователем пароль.
                var hashPassword = Functions.Functions.hashPassword(password);
                if (currentTeacher == null || currentTeacher.Password != hashPassword)
                {
                    await MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        App.Current.MainPage.DisplayAlert("Ошибка", "Неверный логин или пароль", "OK");
                    });
                    return;
                }
                var navigationParametr = new Dictionary<string, object>
                {
                    {"Teacher", currentTeacher }
                };
                await Shell.Current.GoToAsync($"{nameof(TeacherPersonalAccount)}", navigationParametr);
            }
            else
            {
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    App.Current.MainPage.DisplayAlert("Ошибка", "Неверный адрес электронной почты", "OK");
                });
                return;
            }

        }

        [RelayCommand]
        public async void CreateAccount()
        {
            await Shell.Current.GoToAsync(nameof(AccountCreation));
        }
    }
}
