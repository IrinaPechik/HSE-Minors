using MinorsApplication.Services;

namespace MinorsApplication;

public partial class App : Application
{
    public static UserInfoService UserInfoService { get; private set; }
    public App(UserInfoService userService)
    {
        InitializeComponent();
        MainPage = new AppShell();
        UserInfoService = userService;
    }
}
