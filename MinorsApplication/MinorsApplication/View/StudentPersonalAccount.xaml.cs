using MinorsApplication.ViewModels;

namespace MinorsApplication;

public partial class StudentPersonalAccount : ContentPage
{
	public StudentPersonalAccount(StudentPersonalAccountViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}