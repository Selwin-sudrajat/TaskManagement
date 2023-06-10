using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Web;
using TaskManagement.Models;
using TaskManagement.Utilities;
using System.Threading.Tasks;

namespace TaskManagement.DTO
{
    public class TaskDTO
    {
        public List<tb_task> taskList {  get; set; }
        public List<TaskList> ResultObject { get; set; }
        public string tempsearch { get; set; }
        public string SearchData { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskDesc { get; set; }
        public string TaskStatus { get; set; }
        public int TaskPriority { get; set; }
        public DateTime DueDate { get; set; }
        public bool status_error { get; set; }
        public string status_msg { get; set; }

        public async Task<List<tb_task>> task_List()
        {
            taskList = new List<tb_task>();
            taskList = await Data.GetTask();

            return taskList;
        }
        public void ClearAll()
        {
            //TaskId = null;
            TaskName = null;
            TaskDesc = null;
            TaskStatus = null;
            //TaskPriority = null;
            //DueDate = null;
        }
        public async Task<string> AutoFill()
        {
            List<tb_task> ResultObject2 = new List<tb_task>();
            ResultObject2 = await task_List();
            this.ResultObject = new List<TaskList>();

            
            if (SearchData != null)
            {
                List<tb_task> search = new List<tb_task>();
                try
                {
                    tempsearch = taskList.Where(m => m.task_name.ToLower().Contains(SearchData.ToLower())).First().ToString();
                }
                catch (Exception ex)
                {
                    tempsearch = SearchData;
                }
                for (int k = 0; k < ResultObject2.Count(); k++)
                {
                    if (ResultObject2[k].task_name.ToLower().Contains(SearchData.ToLower()) )
                    {
                        search.Add(ResultObject2[k]);
                    }
                }
                ResultObject2 = search.Distinct().ToList();
                search.Clear();
            }
            for (int i = 0; i < ResultObject2.Count(); i++)
            {
                TaskList temp = new TaskList();
                temp.row = i + 1;
                temp.task_id = ResultObject2[i].task_id;
                temp.task_name = ResultObject2[i].task_name;
                temp.task_description = ResultObject2[i].task_description;
                temp.due_date = ResultObject2[i].due_date.Value.Date;
                temp.status = ResultObject2[i].status;
                temp.priority = ResultObject2[i].priority.Value;
                this.ResultObject.Add(temp);
            }
            ResultObject.OrderBy(m => m.priority);
            return "Done";
        }

        public async Task<string> InsertDB()
        {
            taskList = await task_List();
            List<tb_task> settask1 = new List<tb_task>();
            settask1 = taskList.Where(m => m.task_name == TaskName).ToList();
            if (settask1.Count() > 0)
            {
                status_error = false;
                status_msg = "Task already registered on database!";
            }
            else
            { 
                tb_task settask = new tb_task();
                //settask.task_id = TaskId;
                settask.task_name = TaskName;
                settask.task_description = TaskDesc;
                settask.due_date = DueDate;
                settask.priority = TaskPriority;
                settask.status = TaskStatus;

                HttpResponseMessage res = await Data.InsertTask(settask);
                if (res.IsSuccessStatusCode)
                {
                    status_error = true;
                    status_msg = "Task has been added!";
                }
                else
                {
                    status_error = false;
                    status_msg = "Add failed!";
                }
            }
            ClearAll();
            return "Done";
        }
        public async Task<string> Update()
        {
            tb_task tas = new tb_task();
            tas.task_id             = Convert.ToInt32(TaskId);
            tas.task_name           = TaskName;
            tas.task_description    = TaskDesc;
            tas.priority            = TaskPriority;
            tas.due_date            = DueDate;
            tas.status              = TaskStatus;

            HttpResponseMessage res = await Data.Update(tas.task_id, tas.task_name, tas.task_description, (int)tas.priority, (DateTime)tas.due_date, tas.status); 
            if (res.IsSuccessStatusCode)
            {
                status_error = true;
                status_msg = "Update successful!";
            }
            else
            {
                status_error = false;
                status_msg = "Update failed!";
            }
            
            ClearAll();
            return "Done";
        }
        public async Task<string> Delete()
        {
            //tb_task tas = new tb_task();
            //tas.task_id = Convert.ToInt32(TaskId);

            //HttpResponseMessage res = 
                await Data.Delete(TaskId.ToString());
            //if (res.IsSuccessStatusCode)
            //{
            //    status_error = true;
            //    status_msg = "Update successful!";
            //}
            //else
            //{
            //    status_error = false;
            //    status_msg = "Update failed!";
            //}

            ClearAll();
            return "Done";
        }
    }
}