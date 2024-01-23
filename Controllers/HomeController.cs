// HomeController.cs
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using moment2_mvc.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace moment2_mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly string jsonFilePath = "tasks.json";

        public IActionResult About()
        {

            return View();
        }


        public IActionResult Index()
        {
            var jsonStr = System.IO.File.ReadAllText(jsonFilePath);
            var taskList = JsonConvert.DeserializeObject<IEnumerable<TaskModel>>(jsonStr);
            return View(taskList);
        }


        public IActionResult Create()
        {

            return View();
        }


        // [HttpPost]
        // public IActionResult Create(TaskList task)
        // {
        //     // Get the current list of tasks
        //     var tasks = ReadTasksFromJsonFile();

        //     // Assign an ID to the new task
        //     task.Id = tasks.Count + 1;

        //     // Add the new task to the list
        //     tasks.Add(task);

        //     // Save the updated task list to the JSON file
        //     SaveTasksToJsonFile(tasks);

        //     return RedirectToAction("Index");
        // }









        // private List<TaskList> ReadTasksFromJsonFile()
        // {
        //     // Read tasks from the JSON file
        //     if (System.IO.File.Exists(jsonFilePath))
        //     {
        //         var jsonStr = System.IO.File.ReadAllText(jsonFilePath);
        //         return JsonConvert.DeserializeObject<List<TaskList>>(jsonStr);
        //     }
        //     return new List<TaskList>();
        // }

        // private void SaveTasksToJsonFile(List<TaskList> tasks)
        // {
        //     // Save tasks to the JSON file
        //     var json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
        //     System.IO.File.WriteAllText(jsonFilePath, json);
        // }






        // private List<TaskModel> getList()
        // {
        //     var tasks = new List<TaskModel>
        //     {
        //         new TaskModel
        //         {
        //             Id = 1,
        //             Title = "Gör klart uppgiften",
        //             Description = "Måste göras klat asap",
        //             IsCompleted = false
        //         },
        //         new TaskModel
        //         {
        //             Id = 2,
        //             Title = "Ringa banken",
        //             Description = "Ansöka om lån",
        //             IsCompleted = false
        //         },
        //          new TaskModel
        //         {
        //             Id = 3,
        //             Title = "Ringa Simon",
        //             Description = "Surra lite",
        //             IsCompleted = false
        //         }
        //     };

        //     return tasks;
        // }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
