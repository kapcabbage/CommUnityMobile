using FreshWithSQLite.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FreshWithSQLite.Services
{
    public class ChartDataService
    {

        public async Task<Report> GetCurrentData()
        {
            var requestUrl = "http://10.0.2.2:8000/raport3.json";

            var httpClient = new HttpClient();

            var userJson = await httpClient.GetStringAsync(requestUrl);

            var facebookProfile = JsonConvert.DeserializeObject<Report>(userJson);
            
            return facebookProfile;
        }
    }
}
