using Microsoft.Extensions.Logging;
using MinorsApplication.Services;
using MinorsApplication.ViewModels;


namespace MinorsApplication;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "hseMinors.db3");
        builder.Services.AddSingleton<UserInfoService>(s => ActivatorUtilities.CreateInstance<UserInfoService>(s, dbPath));

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<LoginViewModel>();

        builder.Services.AddTransient<StudentPersonalAccount>();
        builder.Services.AddTransient<StudentPersonalAccountViewModel>();

        builder.Services.AddTransient<TeacherPersonalAccount>();
        builder.Services.AddTransient<TeacherPersonalAccountViewModel>();

        builder.Services.AddTransient<AccountCreation>();
        builder.Services.AddTransient<AccountCreationViewModel>();

        builder.Services.AddTransient<StudentInfoPage>();
        builder.Services.AddTransient<StudentInfoPageViewModel>();

        builder.Services.AddTransient<TeacherInfoPage>();
        builder.Services.AddTransient<TeacherInfoPageViewModel>();

        builder.Services.AddTransient<SelectionMinorPage>();
        builder.Services.AddTransient<SelectionMinorViewModel>();

        builder.Services.AddTransient<ViewApplications>();
        builder.Services.AddTransient<ViewApplicationsViewModel>();

        return builder.Build();
    }
}
