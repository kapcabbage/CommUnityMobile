using FreshMvvm;
using FreshWithSQLite.Navigation;
using FreshWithSQLite.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FreshWithSQLite.PageModels
{
    public class SettingsPageModel : FreshBasePageModel
    {
        private IDialogService _dialogService;
      
        public ICommand SaveCommand { get; private set; }
        public ICommand AddCommand { get; private set; }

        private string _message;
       
        public string WelcomeMessage
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                RaisePropertyChanged();
            }

        }



        //private string _channel;

        //public string Channel
        //{
        //    get
        //    {
        //        return _channel;
        //    }
        //    set
        //    {
        //        _channel = value;
        //        RaisePropertyChanged();
        //    }
        //}

        private int _channel;

        public int Channel
        {
            get
            {
                return _channel;
            }
            set
            {
                _channel = value;
                RaisePropertyChanged();
            }
        }

        private bool _isOn;

        public bool isOn
        {
            get
            {
                return _isOn;
            }
            set
            {
                _isOn = value;
                RaisePropertyChanged();
            }
        }
        public override void Init(object initData)
        {

            base.Init(initData);
            _dialogService = new DialogService();

            SaveCommand = new Command(() =>
            {
                OnPost();
                _dialogService.ShowMessage("Settings confirmed", "Hurray");
            });

            AddCommand = new Command(() =>
            {

                //_dialogService.ShowMessage("Hello from FAB button", "ViewModel");
                var app = Application.Current as App;
                //we set the MasterDetailPage to the MainPage of App(this MainPage is one property of App)
                //get the MainPage and cast it to MainPage(this is the page I defined)

                //set the detail of this page with new page!
                var master = app.MainPage as CustomMasterDetail;
                master.Switch("New post");
            });

        }

        public void OnPost()
        {
            var requestUrl = "";
            if (Device.OS == TargetPlatform.Android)
            {
                requestUrl = "http://10.0.3.2:8080/test/";
            }
            else if (Device.OS == TargetPlatform.iOS)
            {

            }
            else if (Device.OS == TargetPlatform.WinPhone)
            {
                requestUrl = "http://localhost:8080/test/";
            }
            else if (Device.OS == TargetPlatform.Windows)
            {
                requestUrl = "http://localhost:8080/test/";
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get,requestUrl);
            if (isOn)
            {
                if (Channel == 1 || Channel == null)
                {
                    request.Headers.Add("SetChannel", "1");
                }
                else
                {
                    request.Headers.Add("SetChannel", "2");
                }
                request.Headers.Add("WelcomeEnable", "1");
                request.Headers.Add("SetWelcome", WelcomeMessage);
            }
            else
            {
                request.Headers.Add("WelcomeDisable", "1");
            }
            PostRequest(request);
        }


        async void PostRequest(HttpRequestMessage request)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);

            // Get the response
            var responseString = await response.Content.ReadAsStringAsync();
        }
    }
}
