using MinorsApplication.ViewModels;

namespace MinorsApplication;

public partial class TeacherInfoPage : ContentPage
{
	public TeacherInfoPage(TeacherInfoPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}