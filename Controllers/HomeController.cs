// HomeController.cs
using System;
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
            ViewBag.GitHub = "https://github.com/LucasHSchuber?tab=repositories";
            ViewData["Bio"] = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Eius quaerat cum officiis omnis minima tempore voluptate nesciunt nam consectetur facere, alias ratione enim saepe atque porro asperiores voluptatem sed suscipit";
            return View();
        }


        public IActionResult Index()
        {
            var jsonStr = System.IO.File.ReadAllText(jsonFilePath);
            var taskList = JsonConvert.DeserializeObject<IEnumerable<TaskModel>>(jsonStr);

            string Citycookie = Request.Cookies["Citycookie"];
            ViewBag.City = Citycookie;
            string Usernamecookie = Request.Cookies["Usernamecookie"];
            ViewBag.Username = Usernamecookie;
            return View(taskList);
        }


        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(TaskModel task)
        {

            if (ModelState.IsValid)
            {

                // Get the current list of tasks
                var jsonStr = System.IO.File.ReadAllText(jsonFilePath);
                var tasks = JsonConvert.DeserializeObject<List<TaskModel>>(jsonStr);

                if (task != null)
                {

                    // Assign an ID to the new task
                    task.Id = tasks.Count + 1;

                    // Add the new task to the list
                    tasks.Add(task);

                    // Save the updated task list to the JSON file
                    var updatedJson = JsonConvert.SerializeObject(tasks, Formatting.Indented);
                    System.IO.File.WriteAllText(jsonFilePath, updatedJson);

                    //set cookie
                    Response.Cookies.Append("Citycookie", task.City);
                    Response.Cookies.Append("Usernamecookie", task.UserName);

                    return RedirectToAction("Index", "Home");

                }

                ModelState.Clear();
            }

            return View();

        }



        [HttpPost]

        public IActionResult Remove(int id)
        {

            var jsonStr = System.IO.File.ReadAllText(jsonFilePath);
            var tasks = JsonConvert.DeserializeObject<List<TaskModel>>(jsonStr);

            var remTask = tasks.FirstOrDefault(t => t.Id == id);

            if (remTask != null)
            {
                tasks.Remove(remTask);

                // Save the updated list
                var updatedJson = JsonConvert.SerializeObject(tasks, Formatting.Indented);
                System.IO.File.WriteAllText(jsonFilePath, updatedJson);
            }

            return Ok();
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
