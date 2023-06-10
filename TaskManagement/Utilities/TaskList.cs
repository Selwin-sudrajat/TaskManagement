using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagement.Utilities
{
	public class TaskList
	{
		public int task_id  { get; set; }
		public int row { get; set; }
		public string task_name { get; set; }
		public string task_description { get; set; }
		public DateTime due_date { get; set; }
		public string status { get; set; }
		public int priority { get; set; }

	}

	public enum Status
	{
		OnHold,
		OnProgress,
		Done
	}
}