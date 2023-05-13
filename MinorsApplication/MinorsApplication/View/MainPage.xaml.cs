using MinorsApplication.ViewModels;
using System.Security.AccessControl;

namespace MinorsApplication;

public partial class MainPage : ContentPage
{
	public MainPage(LoginViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}

