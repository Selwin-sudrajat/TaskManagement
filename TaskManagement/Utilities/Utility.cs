using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using TaskManagement.Controllers;
using TaskManagement.Models;
using System.Web.Mvc;

namespace TaskManagement.Utilities
{
    public class Utility
    {
        public static string GetStringSha256Hash(string text)
        {
            if (String.IsNullOrEmpty(text))
                return String.Empty;

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }

        public static async Task<T> TryDeserialize<T>(HttpResponseMessage Response)
        {
            var model = (T)Activator.CreateInstance(typeof(T));
            var errorStack = new Stack<Newtonsoft.Json.Serialization.ErrorEventArgs>();
            var settings = new JsonSerializerSettings
            {
                Converters = { new StringEnumConverter() },
                Error = (o, e) => errorStack.Push(e)
            };

            try
            {
                model = JsonConvert.DeserializeObject<T>(Response.Content.ReadAsStringAsync().Result, settings);
            }

            catch (JsonException ex)
            {
                var last = errorStack.Last();
                var member = last.ErrorContext.Member;
                var path = last.ErrorContext.Path;
                var objectsStack = String.Join(", ", errorStack.Where(e => e.CurrentObject != null).Select(e => e.CurrentObject.ToString()));

                await Utility.Logger("<<[" + Utility.GetStringSha256Hash(DateTime.Now.ToString() + Response.Content.ReadAsStringAsync().Result) + "][" + objectsStack.ToString() + "]" + "[" + DateTime.Now + "]" + "[" + Response.Content.ReadAsStringAsync().Result + "]" + "[" + ex.Message + "]" + "[" + path + "]" + "[" + member + "]>>");
            }

            return model;
        }
        public static async Task Logger(string eventlog)
        {
        }
        public static async Task<HttpResponseMessage> SendHTTPGet(string url, string payload)
        {
            HttpResponseMessage Res;
            using (var client = new HttpClient())
            {
                //Passing service base url  
                string token = "null";
                //string readasstring

                client.BaseAddress = new Uri(Constant.Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("sso-token", token);
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string escaped_payload = Uri.EscapeDataString(payload);
                //Sending request to find web api REST service resource using HttpClient  

                Res = await client.GetAsync(url + escaped_payload);
            }

            //If request fail, write log 
            //if (!Res.IsSuccessStatusCode)
            //    await Utility.Logger("[GET]" + "[Method:EmployeeInfo]" + "[HttpStatusCode:" + Res.StatusCode + "]" + "[Request:" + Res.RequestMessage.RequestUri + "]");
            string read = Res.StatusCode.ToString();
            string readall = await Res.Content.ReadAsStringAsync();
            //if (read.ToLower().Contains("unauthorized")) Constant.token_status = readall;
            return Res;
        }

        public static async Task<HttpResponseMessage> SendHTTPDelete(string url)
        {
            HttpResponseMessage response;
            using (HttpClient client = new HttpClient())
            {
                string token = "null";

                // Set base address and headers
                client.BaseAddress = new Uri(Constant.Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("sso-token", token);

                // Send DELETE request
                response = await client.DeleteAsync(url);
            }

            // If the request fails, you can log the details here
            return response;
        }
        public static async Task<HttpResponseMessage> SendHTTPGetValidasi(string url, string payload)
        {
            HttpResponseMessage Res;
            using (var client = new HttpClient())
            {
                //Passing service base url  
                string token = "null";
                //string readasstring

                client.BaseAddress = new Uri(Constant.Baseurl);
                client.DefaultRequestHeaders.Clear();
                //client.DefaultRequestHeaders.Add("sso-token", token);
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string escaped_payload = Uri.EscapeDataString(payload);
                //Sending request to find web api REST service resource using HttpClient  

                await client.GetAsync(url + escaped_payload);
                Res = new HttpResponseMessage();
                Res.StatusCode = System.Net.HttpStatusCode.OK;
            }

            //If request fail, write log 
            //if (!Res.IsSuccessStatusCode)
            //    await Utility.Logger("[GET]" + "[Method:EmployeeInfo]" + "[HttpStatusCode:" + Res.StatusCode + "]" + "[Request:" + Res.RequestMessage.RequestUri + "]");
            //string read = Res.StatusCode.ToString();
            //string readall = await Res.Content.ReadAsStringAsync();
            //if (read.ToLower().Contains("unauthorized")) Constant.token_status = readall;
            return Res;
        }


        public static async Task<HttpResponseMessage> SendHTTPGet(string url, string payload, string token)
        {
            HttpResponseMessage Res;
            using (var client = new HttpClient())
            {
                //Passing service base url        

                client.BaseAddress = new Uri(Constant.Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("sso-token", token);
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string escaped_payload = Uri.EscapeDataString(payload);
                //Sending request to find web api REST service resource using HttpClient  
                Res = await client.GetAsync(url + escaped_payload);

            }

            //If request fail, write log 
            //if (!Res.IsSuccessStatusCode)
            //    await Utility.Logger("[GET]" + "[Method:EmployeeInfo]" + "[HttpStatusCode:" + Res.StatusCode + "]" + "[Request:" + Res.RequestMessage.RequestUri + "]");
            string read = Res.StatusCode.ToString();
            string readall = await Res.Content.ReadAsStringAsync();
            //if (read.ToLower().Contains("unauthorized")) Constant.token_status = readall;
            return Res;
        }

        public static async Task<HttpResponseMessage> SendHTTPGetAll(string url)
        {

            HttpResponseMessage Res;
            using (var client = new HttpClient())
            {
                string token = "null";

                //Passing service base url  
                client.BaseAddress = new Uri(Constant.Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("sso-token", token);

                //string escaped_payload = Uri.EscapeDataString(payload);
                //Sending request to find web api REST service resource using HttpClient  
                Res = await client.GetAsync(url);
            }

            //If request fail, write log 
            //if (!Res.IsSuccessStatusCode)
            //    await Utility.Logger("[GET]" + "[Method:EmployeeInfo]" + "[HttpStatusCode:" + Res.StatusCode + "]" + "[Request:" + Res.RequestMessage.RequestUri + "]");

            return Res;
        }

        public static async Task<HttpResponseMessage> SendHTTPGetAll(string url, string token)
        {

            HttpResponseMessage Res;
            using (var client = new HttpClient())
            {

                //Passing service base url  
                client.BaseAddress = new Uri(Constant.Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("sso-token", token);

                //string escaped_payload = Uri.EscapeDataString(payload);
                //Sending request to find web api REST service resource using HttpClient  
                Res = await client.GetAsync(url);
            }

            //If request fail, write log 
            //if (!Res.IsSuccessStatusCode)
            //    await Utility.Logger("[GET]" + "[Method:EmployeeInfo]" + "[HttpStatusCode:" + Res.StatusCode + "]" + "[Request:" + Res.RequestMessage.RequestUri + "]");

            return Res;
        }
        public static async Task<HttpResponseMessage> SendHTTPPostExternal(string server_address, string url, Object payload)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Clear();
                var complete_url = server_address + url;

                //serialize payload
                var serialized_payload = new StringContent(new JavaScriptSerializer().Serialize(payload), Encoding.UTF8, "application/json");

                //Sending request to find web api REST service resource using HttpClient  
                try
                {
                    Response = await client.PostAsync(complete_url, serialized_payload);
                    var print = Response.Content.ReadAsStringAsync().Result;
                }
                catch (Exception ex)
                {
                    //await Utility.Logger("<<" + "[" + Utility.GetStringSha256Hash(DateTime.Now.ToString()) + "]" + "[" + ex.Message + "]" + "[" + ex.Message + "]" + "[" + ex.Message + "]" + ">>");
                }

            }
            ////If request fail, write log 
            //if (!Response.IsSuccessStatusCode)
            //    await Utility.Logger("[POST]" + "[Method:SendHTTPPostExternal]" + "[HttpStatusCode:" + Response.StatusCode + "]" + "[Request:" + Response.RequestMessage.RequestUri + "]");

            return Response;
        }

        public static async Task<HttpResponseMessage> SendHTTPGetExternal(string server_address, string url, Object payload)
        {
            HttpResponseMessage Res;
            using (var client = new HttpClient())
            {

                HttpCookie myCookie = HttpContext.Current.Request.Cookies["token"];
                string token = "null";
                if (myCookie != null || myCookie.Value != null) token = myCookie.Value;

                //Passing service base url  
                client.BaseAddress = new Uri(server_address);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("sso-token", token);

                //Sending request to find web api REST service resource using HttpClient  
                Res = await client.GetAsync(url + payload);
            }

            //If request fail, write log 
            //if (!Res.IsSuccessStatusCode) await Utility.Logger("[GET]" + "[Method:EmployeeInfo]" + "[HttpStatusCode:" + Res.StatusCode + "]" + "[Request:" + Res.RequestMessage.RequestUri + "]");

            var a = Res.Content.ReadAsStringAsync().Result;
            return Res;
        }

        public static async Task<T> GetData<T>(string login, string url)
        {
            string server_address = Constant.Baseurl;
            HttpResponseMessage Response = await Utilities.Utility.SendHTTPGetExternal(server_address, url, login);
            var model = await Utility.TryDeserialize<T>(Response);

            return model;
        }

        public static async Task<DateTime> AddTimeOffset(DateTime input)
        {
            var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);
            input = input.ToUniversalTime();
            DateTime output = input + offset;
            return output;
        }
    }
}