using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MinorsApplication.Models;

namespace MinorsApplication.ViewModels
{
    [QueryProperty(nameof(Email), "Email")]
    [QueryProperty(nameof(Password), "Password")]
    public partial class StudentInfoPageViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        [ObservableProperty]
        string email;
        [ObservableProperty]
        string password;
        [ObservableProperty]
        string fullName;
        [ObservableProperty]
        string educationalProgram;
        [ObservableProperty]
        string faculty;
        [ObservableProperty]
        int? courseNumber;
        [ObservableProperty]
        int? percentile;

        [RelayCommand]
        public async void SaveData()
        {
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(educationalProgram)
                || string.IsNullOrEmpty(faculty) || courseNumber == null || percentile == null
                || courseNumber <= 0 || percentile < 0 || courseNumber > 5 || percentile > 100)
            {
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    App.Current.MainPage.DisplayAlert("Ошибка", "Неверные данные", "Ok");
                });
                return;
            }
            StudentInfo currentStudent = new StudentInfo(email, password, fullName, faculty, educationalProgram, (int)courseNumber, (int)percentile);
            await App.UserInfoService.AddNewStudent(currentStudent);
            var navigationParametr = new Dictionary<string, object>
            {
                {"Student", currentStudent }
            };
            await Shell.Current.GoToAsync($"{nameof(StudentPersonalAccount)}", navigationParametr);
        }
    }
}
