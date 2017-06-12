using FreshWithSQLite.PageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FreshWithSQLite.Pages
{
    public partial class LoginPage : FreshMvvm.FreshBaseContentPage
    {

    
        public LoginPage(LoginPageModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
            ToolbarItems.Add(new ToolbarItem("", "", () => {
                Application.Current.MainPage = new NavigationPage(new LaunchPage((App)Application.Current));
            }));

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
