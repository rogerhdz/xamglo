using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace XamarinPO.Helpers
{
    /// <summary>
    /// Manager who executes api calls returning an object of type T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HttpManager<T> where T : class
    {
        /// <summary>
        /// Generates a Get call to api receiving a configuration object 
        /// </summary>
        /// <param name="configuration">Location of api method to consume</param>
        /// <returns> HttpManagerResult<T/> object with a list of required items, success flag and message</returns>
        public async Task<HttpManagerResult<T>> HttpGetList(HttpManagerConfiguration configuration)
        {
            var result = new HttpManagerResult<T>();
            var client = new HttpClient();
            client.BaseAddress = new Uri(configuration.Server);
            var response = await client.GetAsync(configuration.Method);
            var jsonResult = await response.Content.ReadAsStringAsync();

            result.Success = response.IsSuccessStatusCode;
            result.Items = JsonConvert.DeserializeObject<List<T>>(jsonResult);
            result.Message = response.IsSuccessStatusCode ? "Success" : jsonResult;
            return result;
        }
    }

    public class HttpManagerConfiguration
    {
        public string Server { get; set; }
        public string Method { get; set; }
    }

    public class HttpManagerResult<T>
    {
        public List<T> Items { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
