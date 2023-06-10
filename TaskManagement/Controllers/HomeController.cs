using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagement.Models;
using System.Threading.Tasks;
using TaskManagement.DTO;
using TaskManagement.Utilities;

namespace TaskManagement.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> Index(TaskDTO task)
        {
            ViewBag.Message = "home";
            if (task != null)
            {
                await task.AutoFill();
            }
            return View(task);
        }

        [HttpPost]
        public async Task<ActionResult> Insert(TaskDTO Data, string tabbutton, string insert)
        {
            ModelState.Clear();
            if (Data != null)
            {
                await Data.InsertDB();
                await Data.AutoFill();
            }
            TaskDTO model = new TaskDTO();
            return View("Index", Data);
        }

        [HttpPost]
        public async Task<ActionResult> Update(TaskDTO Data, string update, int? page_now)
        {
            ModelState.Clear();
            if (Data != null)
            {
                await Data.Update();
                await Data.AutoFill();
            }
            TaskDTO model = new TaskDTO();
            model.status_error = Data.status_error;
            model.status_msg = Data.status_msg;
            return View("Index", Data);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(TaskDTO Data, string update, int? page_now)
        {
            ModelState.Clear();
            if (Data != null)
            {
                await Data.Delete();
                await Data.AutoFill();
            }
            //TaskDTO model = new TaskDTO();
            //model.status_error = Data.status_error;
            //model.status_msg = Data.status_msg;
            return View("Index", Data);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}