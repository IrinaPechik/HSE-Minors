using MinorsApplication.ViewModels;

namespace MinorsApplication;

public partial class TeacherPersonalAccount : ContentPage
{
	public TeacherPersonalAccount(TeacherPersonalAccountViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}