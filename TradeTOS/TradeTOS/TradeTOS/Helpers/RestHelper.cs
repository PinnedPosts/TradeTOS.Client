using TradeTOS.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace TradeTOS.Helpers
{
    public class RestHelper
    {
        string baseAddress;
        public RestHelper(string _baseAddress, bool hasToken = true)
        {
            baseAddress = _baseAddress;
        }

        private HttpClient CreateInstance()
        {
            HttpClient httpHandler;
            httpHandler = new HttpClient();
            httpHandler.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (AppConstants.CurrentUser!= null)
            {
                httpHandler.DefaultRequestHeaders.TryAddWithoutValidation("X-Token", AppConstants.CurrentUser.Token);
            }
            httpHandler.BaseAddress = new Uri(baseAddress);
            return httpHandler;
        }

        public async Task<RestResponse<T>> GetAsync<T>(string requestURL) where T : class
        {
            RestResponse<T> restResponse = new RestResponse<T>();

            try
            {
                using (var httpHandler = CreateInstance())
                {
                    var response = await httpHandler.GetAsync(requestURL);
                    restResponse.IsSuccess = response.IsSuccessStatusCode;
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        if (typeof(T) == typeof(string))
                        {
                            T responseModel = content as T;
                            restResponse.Content = responseModel;
                        }
                        else
                        {
                            restResponse.Content = JsonConvert.DeserializeObject<T>(content);
                        }
                    }
                    else
                    {
                        restResponse.ErrorMessage = await response.Content.ReadAsStringAsync();
                        restResponse.Content = default(T);
                    }
                }
            }
            catch (Exception ex)
            {
                restResponse.ErrorMessage = ex.Message;
                restResponse.Content = default(T);
            }
            return restResponse;
        }

        public async Task<RestResponse<Response>> PostAsync<Request, Response>(string requestURL, Request request) where Response : class
        {
            RestResponse<Response> restResponse = new RestResponse<Response>();
            try
            {
                using (var httpHandler = CreateInstance())
                {
                    string json = "";
                    if (typeof(Request) == typeof(string))
                    {
                        json = request.ToString();
                    }
                    else
                    {
                        json = JsonConvert.SerializeObject(request);
                    }
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = null;
                    response = await httpHandler.PostAsync(requestURL, content);
                    restResponse.IsSuccess = response.IsSuccessStatusCode;
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        if (typeof(Response) == typeof(string))
                        {
                            Response responseModel = responseContent as Response;
                            restResponse.Content = responseModel;
                        }
                        else
                        {
                            restResponse.Content = JsonConvert.DeserializeObject<Response>(responseContent);
                        }
                    }
                    else
                    {
                        restResponse.ErrorMessage = await response.Content.ReadAsStringAsync();
                        restResponse.Content = default(Response);
                    }
                }
            }
            catch (Exception ex)
            {
                restResponse.ErrorMessage = ex.Message;
                restResponse.Content = default(Response);
            }
            return restResponse;
        }

    }

    public class RestResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T Content { get; set; }
        public string ErrorMessage { get; set; }
    }

}

