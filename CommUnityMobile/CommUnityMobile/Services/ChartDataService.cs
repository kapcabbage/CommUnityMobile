using FreshWithSQLite.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;

namespace FreshWithSQLite.Services
{
    public class ChartDataService
    {

        public async Task<Report> GetCurrentData()
        {
            var requestUrl = "";
            if (Device.OS == TargetPlatform.Android)
            {
                requestUrl = "http://10.0.3.2:8000/raport3.json";
            }
            else if (Device.OS == TargetPlatform.iOS)
            {
             
            }
            else if (Device.OS == TargetPlatform.WinPhone)
            {
                requestUrl = "http://localhost:8000/raport3.json";
            }
            else if (Device.OS == TargetPlatform.Windows)
            {
                requestUrl = "http://localhost:8000/raport3.json";
            }
            

            var httpClient = new HttpClient();

            var userJson = await httpClient.GetStringAsync(requestUrl);

            var facebookProfile = JsonConvert.DeserializeObject<Report>(userJson);
            
            return facebookProfile;
        }
    }
}
