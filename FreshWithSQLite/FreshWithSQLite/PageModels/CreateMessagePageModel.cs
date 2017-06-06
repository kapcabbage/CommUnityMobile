using FreshMvvm;
using FreshWithSQLite.Core;
using FreshWithSQLite.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FreshWithSQLite.PageModels
{
	public class CreateMessagePageModel : FreshBasePageModel
	{
		// Use IoC to get our repository.
		//private Repository _repository = FreshIOC.Container.Resolve<Repository>();



        /// <summary>
        /// Called whenever the page is navigated to.
        /// Either use a supplied Contact, or create a new one if not supplied.
        /// FreshMVVM does not provide a RaiseAllPropertyChanged,
        /// so we do this for each bound property, room for improvement.
        /// </summary>
        private Message _message;

        public string MessageText
        {
            get
            {
                return _message.Text;
            }
            set
            {
                _message.Text = value;
                RaisePropertyChanged();
            }
        }
        public override void Init(object initData)
        {
            _message = initData as Message;
            if (_message == null)
            {
                _message = new Models.Message();

            }
            base.Init(initData);


        }


        public ICommand SaveCommand
        {
            get
            {
                return new Command(x => {

                    OnPost(_message);
                    MessageText = string.Empty;
                });
            }
        }

        public void OnPost(Message message)
        {
            var discordHookUrl = "https://discordapp.com/api/webhooks/310811550086725642/tdYcphQ8QZla2YIq18g8nS36IcUYGv2ND8nhSiHyHYqR9sft-qZdT6OUhQOV9bGJMVot"; //Adres webhooka (generowany w Discordzie)
            var hookurl = discordHookUrl + "/slack"; //Adres wygenerowany przez Discorda trzeba uzupełnić końcówką "/slack". 
            string sContentType = "application/json"; // or application/xml

            string s = "{\"attachments\":[{\"text\":\"" + message.Text + " \\nWysłane za pomocą CommUnity\",\"color\": \"#3edda1\"}]}";

            s = s.Replace(System.Environment.NewLine, "\\n");
            PostRequest(hookurl, new StringContent(s, Encoding.UTF8, sContentType));
        }

     
        async void PostRequest(string URL, StringContent content)
        {


            var myHttpClient = new HttpClient();
            var response = await myHttpClient.PostAsync(URL, content);

            var json = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(response.StatusCode);
            Debug.WriteLine(json);
        }
    }
}
