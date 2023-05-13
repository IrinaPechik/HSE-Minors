using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MinorsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinorsApplication.ViewModels
{
    [QueryProperty(nameof(Teacher), "Teacher")]
    public partial class TeacherPersonalAccountViewModel : ObservableObject
    {
        [ObservableProperty]
        TeacherInfo teacher;

        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync($"..");
        }

        [RelayCommand]
        async void ViewStudentsApplications()
        {
            var students = await App.UserInfoService.GetStudentsByTeacherId(teacher.TeacherId);
            var navigationParametr = new Dictionary<string, object>
            {
                {"StudentsByTeacherId", students },
                {"CurrentTeacher", teacher }
            };
            await Shell.Current.GoToAsync($"{nameof(ViewApplications)}", navigationParametr);
        }
    }
}
