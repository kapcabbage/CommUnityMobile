using FreshMvvm;
using Xamarin.Forms;

namespace FreshWithSQLite.Pages
{
	/// <summary>
	/// Update class to inherit from FreshBaseContentPage
	/// </summary>
	public partial class CreateMessagePage : FreshBaseContentPage
	{
		public CreateMessagePage()
		{
			InitializeComponent();
            //ToolbarItems.Add(new ToolbarItem("", "", () => {
            //    Application.Current.MainPage = new NavigationPage(new LaunchPage((App)Application.Current));
            //}));
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            var basePageModel = this.BindingContext as FreshMvvm.FreshBasePageModel;
            if (basePageModel != null)
            {
                if (basePageModel.IsModalAndHasPreviousNavigationStack())
                {
                    if (ToolbarItems.Count < 2)
                    {
                        var closeModal = new ToolbarItem("Close Modal", "", () => {
                            basePageModel.CoreMethods.PopModalNavigationService();
                        });

                        ToolbarItems.Add(closeModal);
                    }
                }
            }

        }
    }
}
