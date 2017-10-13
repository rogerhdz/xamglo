using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using XamarinPO.Extensions;

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
        public async Task<HttpManagerResult> HttpGetList(HttpManagerConfiguration configuration)
        {
            var result = new HttpManagerResult();
            var client = new HttpClient();

            try
            {
                client.BaseAddress = new Uri(configuration.Server);
                var response = await client.GetAsync(configuration.Method);
                var jsonResult = await response.Content.ReadAsStringAsync();

                result.Success = response.IsSuccessStatusCode;
                result.ObjectResult = result.Success ? JsonConvert.DeserializeObject<List<T>>(jsonResult) : new List<T>();
                result.Message = response.IsSuccessStatusCode ? "Success" : jsonResult;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ObjectResult = new List<T>();
                result.Message = ex.Message;
            }
            return result;
        }



        public async Task<HttpManagerResult> HttpJsonPost(HttpManagerConfiguration configuration, HttpContent content)
        {
            var result = new HttpManagerResult();
            var client = new HttpClient();
            HttpResponseMessage response;
            string resultMessage = string.Empty;

            try
            {
                client.BaseAddress = new Uri(configuration.Server);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP POST
                response = await client.PostAsync(configuration.Method, content);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ObjectResult = ex.MessagePlusInner();
                result.Message = "Request Failed";
                return result;
            }

            try
            {
                resultMessage = await response.Content.ReadAsStringAsync();

                T complexResulObject = JsonConvert.DeserializeObject<T>(resultMessage);
                result.Success = response.IsSuccessStatusCode;
                result.ObjectResult = complexResulObject;
                result.Message = response.IsSuccessStatusCode ? "Success" : resultMessage;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ObjectResult = ex.MessagePlusInner();
                result.Message = response.IsSuccessStatusCode ? "Success" : resultMessage;
            }
            return result;
        }

        public async Task<HttpManagerResult> HttpJsonGet(HttpManagerConfiguration configuration)
        {
            var result = new HttpManagerResult();
            var client = new HttpClient();
            HttpResponseMessage response;
            string resultMessage = string.Empty;
            try
            {
                client.BaseAddress = new Uri(configuration.Server);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                response = await client.GetAsync(configuration.Method);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ObjectResult = ex.MessagePlusInner();
                result.Message = "Request Failed";
                return result;
            }

            try
            {
                resultMessage = await response.Content.ReadAsStringAsync();

                T complexResulObject = JsonConvert.DeserializeObject<T>(resultMessage);
                result.Success = response.IsSuccessStatusCode;
                result.ObjectResult = complexResulObject;
                result.Message = response.IsSuccessStatusCode ? "Success" : resultMessage;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ObjectResult = ex.MessagePlusInner();
                result.Message = response.IsSuccessStatusCode ? "Success" : resultMessage;
            }
            return result;
        }

        /// <summary>
        /// Generates a Get call to api receiving a configuration object 
        /// </summary>
        /// <param name="configuration">Location of api method to consume</param>
        /// <returns> HttpManagerResult<T/> object with a list of required items, success flag and message</returns>
        public async Task<HttpManagerResult> HttpGetAzureList(HttpManagerConfiguration configuration)
        {
            var result = new HttpManagerResult();
            var client = new HttpClient();

            try
            {
                client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");
                client.BaseAddress = new Uri(configuration.Server);
                var response = await client.GetAsync(configuration.Method);
                var jsonResult = await response.Content.ReadAsStringAsync();

                result.Success = response.IsSuccessStatusCode;
                result.ObjectResult = result.Success ? JsonConvert.DeserializeObject<List<T>>(jsonResult) : new List<T>();
                result.Message = response.IsSuccessStatusCode ? "Success" : jsonResult;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ObjectResult = new List<T>();
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// Generates a Get call to api receiving a configuration object 
        /// </summary>
        /// <param name="configuration">Location of api method to consume</param>
        /// <param name="objectToUpdate">Object to send to api</param>
        /// <returns> HttpManagerResult<T/> object with a list of required items, success flag and message</returns>
        public async Task<HttpManagerResult> HttpPatchAzure(HttpManagerConfiguration configuration, T objectToUpdate)
        {
            var result = new HttpManagerResult();
            var client = new HttpClient();

            try
            {
                client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");
                client.BaseAddress = new Uri(configuration.Server);
                var request = new HttpRequestMessage(new HttpMethod("PATCH"), configuration.Method)
                {
                    Content = new StringContent(LowercaseJsonSerializer.SerializeObject(objectToUpdate), Encoding.UTF8, "application/json")
                };

                var response = await client.SendAsync(request);
                var jsonResult = await response.Content.ReadAsStringAsync();

                result.Success = response.IsSuccessStatusCode;
                result.ObjectResult = null;
                result.Message = response.IsSuccessStatusCode ? "Success" : jsonResult;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ObjectResult = null;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<HttpManagerResult> HttpGetAzureTuple(HttpManagerConfiguration configuration)
        {
            var result = new HttpManagerResult();
            var client = new HttpClient();

            try
            {
                client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");
                client.BaseAddress = new Uri(configuration.Server);
                var response = await client.GetAsync(configuration.Method);
                var jsonResult = await response.Content.ReadAsStringAsync();

                result.Success = response.IsSuccessStatusCode;
                if (result.Success)
                    result.ObjectResult = JsonConvert.DeserializeObject<T>(jsonResult);
                result.Message = response.IsSuccessStatusCode ? "Success" : jsonResult;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }
    }


    public class HttpManagerConfiguration
    {
        public string Server { get; set; }
        public string Method { get; set; }
    }

    public class HttpManagerResult
    {
        public object ObjectResult { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
