using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MinorsApplication.Models;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace MinorsApplication.ViewModels
{
    [QueryProperty(nameof(Student), "Student")]
    [QueryProperty(nameof(SelectedTeacher), "SelectedTeacher")]
    public partial class StudentPersonalAccountViewModel : ObservableObject
    {
        [ObservableProperty]
        StudentInfo student;
        [ObservableProperty]
        TeacherInfo selectedTeacher;

        [RelayCommand]
        async Task GoBack()
        {
            // Исправить для выхода после регистрации!!
            await Shell.Current.GoToAsync($"{nameof(MainPage)}");
        }

        [RelayCommand]
        async void SelectMinor()
        {
            if (!student.CanSelectMinor)
            {
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    App.Current.MainPage.DisplayAlert("Ошибка", "Вы не можете выбрать майнор, т.к. уже подали заявку или уже были зачислены на майнор.\n" +
                        "Вы можете попробовать отозвать заявку.",
                        "OK");
                });
                return;
            }
            var teachers = await App.UserInfoService.GetAllTeachers();
            var availableTeachers = teachers.Where(x => x.NumberOfVacantSeats > 0).ToList();
            var navigationParametr = new Dictionary<string, object>
            {
                {"AvailableTeachers", availableTeachers },
                {"CurrentStudent", student }
            };
            await Shell.Current.GoToAsync($"{nameof(SelectionMinorPage)}", navigationParametr);
        }

        [RelayCommand]
        async void DeleteApplication()
        {
            if (student.StatusOfApplication == StudentInfo.Status.NotExist)
            {
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    App.Current.MainPage.DisplayAlert("Ошибка", "Вы не можете отозвать заявку, т.к. её не существует.", "OK");
                });
                return;
            }
            if (student.StatusOfApplication == StudentInfo.Status.Одобрена)
            {
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    App.Current.MainPage.DisplayAlert("Ошибка", "Вы не можете отозвать заявку, т.к. уже были зачислены на майнор.", "OK");
                });
                return;
            }
            var teachers = await App.UserInfoService.RemoveApplication(student.Email);
        }
    }
}
