using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MinorsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MinorsApplication.ViewModels
{
    [QueryProperty(nameof(StudentsByTeacherId), "StudentsByTeacherId")]
    [QueryProperty(nameof(CurrentTeacher), "CurrentTeacher")]
    public partial class ViewApplicationsViewModel : ObservableObject
    {
        [ObservableProperty]
        List<StudentInfo> studentsByTeacherId;
        [ObservableProperty]
        TeacherInfo currentTeacher;

        public IReadOnlyList<object> selectedStudents;

        // Отвечаем на заявку студентам от текущего преподавателя.
        // Кого преподаватель одобрил - тому отправляем статус одобрен
        // Кого нет - отправляем - отклонено.
        [RelayCommand]
        public async void SendApprovalToStudents()
        {
            // Если ни один студент не был выбран
            // или было выбраное студентов больше, чем вакантных мест - не даём отправить ответ.
            if (selectedStudents == null)
            {
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    App.Current.MainPage.DisplayAlert("Ошибка", "Вы не выбрали ни одного студента.", "OK");
                });
                return;
            }
            if (selectedStudents.Count > currentTeacher.NumberOfVacantSeats)
            {
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    App.Current.MainPage.DisplayAlert("Ошибка", "Мест на вашем майноре меньше, чем количество студентов, которым " +
                        "Вы хотите одобрить заявку.", "OK");
                });
                return;
            }
            var approvedStudents = selectedStudents.Cast<StudentInfo>().ToList();
            // Обновляем информацию в базе данных.
            await App.UserInfoService.ApproveApplications(approvedStudents, currentTeacher);
            await Shell.Current.GoToAsync($"{nameof(TeacherPersonalAccount)}", new Dictionary<string, object> { { "Teacher", currentTeacher } });
        }

        [RelayCommand]
        public async void SendRejectionToStudents()
        {
            // Если ни один студент не был выбран - не даём отправить ответ.
            if (selectedStudents == null)
            {
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    App.Current.MainPage.DisplayAlert("Ошибка", "Вы не выбрали ни одного студента.", "OK");
                });
                return;
            }
            var rejectedStudents = selectedStudents.Cast<StudentInfo>().ToList();
            // Обновляем информацию в базе данных.
            await App.UserInfoService.RejectApplications(rejectedStudents, currentTeacher);
            await Shell.Current.GoToAsync($"{nameof(TeacherPersonalAccount)}", new Dictionary<string, object> { { "Teacher", currentTeacher } });
        }
    }
}
