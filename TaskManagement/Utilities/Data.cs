using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TaskManagement.DTO;
using TaskManagement.Models;
using TaskManagement.Utilities;
using System.Web.Script.Serialization;
using System.Text;

namespace TaskManagement.Utilities
{
    public static class Data
    {
        public static async Task<List<tb_task>> GetTask()
        {
            List<tb_task> Model = new List<tb_task>();
            string url = "api/Task";
            HttpResponseMessage Response = await Utility.SendHTTPGetAll(url);
            if (Response.IsSuccessStatusCode)
                //Deserialize payload
                Model = JsonConvert.DeserializeObject<List<tb_task>>(Response.Content.ReadAsStringAsync().Result);

            return Model;
        }

        public static async Task<HttpResponseMessage> InsertTask(tb_task model)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Constant.Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("sso-token", Constant.sso_token);
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                response = await client.PostAsync("api/Task", new StringContent(
                                new JavaScriptSerializer().Serialize(model), Encoding.UTF8, "application/json"));
            }
            return response;
        }
        public static async Task<HttpResponseMessage> Update(int task_id, string task_name, string task_description, int priority, DateTime due_date, string status)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            tb_task UpdatedData = new tb_task();
            UpdatedData = await Data.GetIDTask(Convert.ToString(task_id));

            UpdatedData.task_name = task_name;
            UpdatedData.task_description = task_description;
            UpdatedData.priority = priority;
            UpdatedData.due_date = due_date;
            UpdatedData.status = status;

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Constant.Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                response = await client.PutAsync("api/Task/" + UpdatedData.task_id, new StringContent(
                                new JavaScriptSerializer().Serialize(UpdatedData), Encoding.UTF8, "application/json"));
            }
            return response;
        }
        public static async Task<tb_task> GetIDTask(string id)
        {

            string hashed = Utility.GetStringSha256Hash(id);
            tb_task Model = new tb_task();
            string url = "api/Task/searchid?id=";
            HttpResponseMessage Response = await Utility.SendHTTPGet(url, id);
            if (Response.IsSuccessStatusCode)
                //Deserialize payload
                Model = JsonConvert.DeserializeObject<tb_task>(Response.Content.ReadAsStringAsync().Result);

            return Model;
        }
        public static async Task<tb_task> Delete(string id)
        {
            string hashed = Utility.GetStringSha256Hash(id);
            tb_task Model = new tb_task();
            string url = "api/Task/deleteid?id=" + id;
            HttpResponseMessage Response = await Utility.SendHTTPDelete(url);
            if (Response.IsSuccessStatusCode)
                //Deserialize payload
                Model = JsonConvert.DeserializeObject<tb_task>(Response.Content.ReadAsStringAsync().Result);

            return Model;
        }
        public static async Task<List<tb_task>> GetTaskDelete(string id)
        {
            string hashed = Utility.GetStringSha256Hash(id);
            List<tb_task> Model = new List<tb_task>();
            string url = "api/Task/deleteid?id=" + id;
            HttpResponseMessage Response = await Utility.SendHTTPGetAll(url);
            if (Response.IsSuccessStatusCode)
                //Deserialize payload
                Model = JsonConvert.DeserializeObject<List<tb_task>>(Response.Content.ReadAsStringAsync().Result);

            return Model;
        }
    }
}