using FreshMvvm;
using FreshWithSQLite.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FreshWithSQLite.PageModels
{
    public class ModalPageModel : FreshBasePageModel
    {
        public ModalPageModel()
        {
        }

        public Command CloseCommand
        {
            get
            {
                return new Command(() => {
                    CoreMethods.PopPageModel(true);
                    var app = Application.Current as App;
                    //we set the MasterDetailPage to the MainPage of App(this MainPage is one property of App)
                    //get the MainPage and cast it to MainPage(this is the page I defined)
                    
                    //set the detail of this page with new page!
                    var master = app.MainPage as CustomMasterDetail;
                    master.Switch("New post");
                });
            }
        }
    }
}
