using MinorsApplication.ViewModels;

namespace MinorsApplication;

public partial class AccountCreation : ContentPage
{
	public AccountCreation(AccountCreationViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

}