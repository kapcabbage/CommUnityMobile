using FreshMvvm;
using FreshWithSQLite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FreshWithSQLite.PageModels
{
    public class LoginPageModel : FreshBasePageModel
    {
        public FacebookLogin _loginResult
        {
            get;set;
        }
       
        public ICommand LoginFacebook
        {
            get
            {
                return new Command(x => {
                    CoreMethods.PushPageModel<FacebookLoginPageModel>(_loginResult, true, true);

                });
            }
        }
      
    }
}
