using FreshMvvm;
using FreshWithSQLite.PageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FreshWithSQLite.Pages
{
    public partial class FacebookLoginPage : FreshBaseContentPage
    {
        private string ClientId = "1708445106115290";
        
        public BindableObject getContext()
        {
            return BindingContext as BindableObject;
        }

        public FacebookLoginPage()
        {
            InitializeComponent();


            var apiRequest =
    "https://www.facebook.com/dialog/oauth?client_id="
    + ClientId
    + "&display=popup&response_type=token&redirect_uri=http://www.facebook.com/connect/login_success.html";


            var webView = new WebView
            {
                Source = apiRequest,
                HeightRequest = 1
            };
            webView.Navigated += WebViewOnNavigated;

            Content = webView;
        }


        private void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {

            var accessToken = ExtractAccessTokenFromUrl(e.Url);

            if (accessToken != "")
            {
                var pageModel = BindingContext as FacebookLoginPageModel;
                pageModel.CoreMethods.SwitchOutRootNavigation("MainAppStack");
                //pageModel.CoreMethods.PushPageModel<CreateMessagePageModel>();

            }
        }

        private string ExtractAccessTokenFromUrl(string url)
        {
            if (url.Contains("access_token") && url.Contains("&expires_in="))
            {
                var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");

                if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
                {
                    at = url.Replace("http://www.facebook.com/connect/login_success.html#access_token=", "");
                }

                var accessToken = at.Remove(at.IndexOf("&expires_in="));

                return accessToken;
            }

            return string.Empty;
        }
    }
}
