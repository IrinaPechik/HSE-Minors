using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MinorsApplication.Models;


namespace MinorsApplication.ViewModels
{
    [QueryProperty(nameof(AvailableTeachers), "AvailableTeachers")]
    [QueryProperty(nameof(CurrentStudent), "CurrentStudent")]
    public partial class SelectionMinorViewModel : ObservableObject
    {
        [ObservableProperty]
        List<TeacherInfo> availableTeachers;
        [ObservableProperty]
        StudentInfo currentStudent;
        [ObservableProperty]
        TeacherInfo selectedTeacher;
        [ObservableProperty]
        string motivationLetter;

        // Отправляем заявку преподавателю майнора от текущего ученика.
        [RelayCommand]
        public async void SendApplication()
        {
            if (selectedTeacher == null)
            {
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    App.Current.MainPage.DisplayAlert("Ошибка", "Вы не можете отправить заявку, т.к. не выбрали преподавателя.", "OK");
                });
                return;
            }
            var curStudent = await App.UserInfoService.AddApplication(currentStudent.Email, selectedTeacher.TeacherId, motivationLetter);
            var navigationParametr = new Dictionary<string, object>
            {
                {"SelectedTeacher", selectedTeacher },
                {"Student", curStudent}
            };
            await Shell.Current.GoToAsync("..", navigationParametr);
        }
    }
}
