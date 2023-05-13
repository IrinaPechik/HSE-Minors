namespace MinorsApplication;

public partial class AppShell : Shell
{
    public AppShell()
    {
        // Загружает определение интерфейса страницы из файла AppShell.xaml.
        InitializeComponent();
        // Регистрируем страницу с выбором преподавателем студента.
        Routing.RegisterRoute(nameof(ViewApplications), typeof(ViewApplications));
        // Регистрируем страницу с заполнением информации о пользователе.
        Routing.RegisterRoute(nameof(AccountCreation), typeof(AccountCreation));
        // Регистрируем страницу с заполнение информации о студенте.
        Routing.RegisterRoute(nameof(StudentInfoPage), typeof(StudentInfoPage));
        // Регистрируем страницу с заполнение информации о преподавателе.
        Routing.RegisterRoute(nameof(TeacherInfoPage), typeof(TeacherInfoPage));
        // Регистрируем страницу с персональной информацией преподавателя.
        Routing.RegisterRoute(nameof(TeacherPersonalAccount), typeof(TeacherPersonalAccount));
        // Регистрируем страницу с персональной информацией студента.
        Routing.RegisterRoute(nameof(StudentPersonalAccount), typeof(StudentPersonalAccount));
        // Регистрируем страницу с выбором студентом майнора.
        Routing.RegisterRoute(nameof(SelectionMinorPage), typeof(SelectionMinorPage));
    }
}
