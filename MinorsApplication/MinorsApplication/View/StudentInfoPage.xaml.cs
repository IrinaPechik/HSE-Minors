using MinorsApplication.ViewModels;

namespace MinorsApplication;

public partial class StudentInfoPage : ContentPage
{
	public StudentInfoPage(StudentInfoPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}