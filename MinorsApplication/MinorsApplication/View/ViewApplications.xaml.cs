using MinorsApplication.ViewModels;

namespace MinorsApplication;

public partial class ViewApplications : ContentPage
{
	ViewApplicationsViewModel vm2;

    public ViewApplications(ViewApplicationsViewModel vm)
	{
		InitializeComponent();
		vm2 = vm;
		BindingContext = vm2;
	}

	public void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		var current = e.CurrentSelection;
		vm2.selectedStudents = current;
	}
}