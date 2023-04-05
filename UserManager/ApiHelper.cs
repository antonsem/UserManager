using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace UserManager
{
    internal class ApiHelper
    {
        private const string TOKEN = "0bf7fb56e6a27cbcadc402fc2fce8e3aa9ac2b40d4190698eb4e8df9284e2023";
        private const string API = "https://gorest.co.in/public/v2/";

        public static HttpClient Client { get; private set; }

        public static void Initialize()
        {
            Client = new HttpClient
            {
                BaseAddress = new Uri(API)
            };
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
        }
    }
}
