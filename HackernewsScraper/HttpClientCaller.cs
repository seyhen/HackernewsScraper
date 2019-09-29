using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HackernewsScraper
{
    public class HttpClientCaller
    {
        HttpClient client;

        public HttpClientCaller()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }


        public async Task<T> GetResponse<T>(string url)
        {
            T response = default(T);
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            {
                response = await responseMessage.Content.ReadAsAsync<T>();
            }
            return response;
        }

    }
}
