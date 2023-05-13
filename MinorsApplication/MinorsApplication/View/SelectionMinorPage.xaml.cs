using MinorsApplication.Models;
using MinorsApplication.ViewModels;

namespace MinorsApplication;

public partial class SelectionMinorPage : ContentPage
{
	public SelectionMinorPage(SelectionMinorViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}