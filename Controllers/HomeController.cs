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


        [Route("/Tasker/Create")]

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [Route("/Tasker/Create")]
        public IActionResult Create(TaskModel task)
        {

            if (ModelState.IsValid)
            {

                // Get the current list of tasks
                var jsonStr = System.IO.File.ReadAllText(jsonFilePath);
                var tasks = JsonConvert.DeserializeObject<List<TaskModel>>(jsonStr);

                if (tasks.Count > 0)
                {

                    var last = tasks.Last();
                    task.Id = last.Id + 1;

                    tasks.Add(task);

                    // Save the updated task list to the JSON file
                    var updatedJson = JsonConvert.SerializeObject(tasks, Formatting.Indented);
                    System.IO.File.WriteAllText(jsonFilePath, updatedJson);

                    //set cookie
                    Response.Cookies.Append("Citycookie", task.City);
                    Response.Cookies.Append("Usernamecookie", task.UserName);

                    return RedirectToAction("Index", "Home");

                }
                else
                {

                    task.Id = 1;

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





        public IActionResult Remove(int id)
        {
            var jsonStr = System.IO.File.ReadAllText(jsonFilePath);
            var tasks = JsonConvert.DeserializeObject<List<TaskModel>>(jsonStr);

            var remTask = tasks.FirstOrDefault(t => t.Id == id);

            if (remTask != null)
            {
                tasks.Remove(remTask);

                var updatedJson = JsonConvert.SerializeObject(tasks, Formatting.Indented);
                System.IO.File.WriteAllText(jsonFilePath, updatedJson);

                return RedirectToAction("Index", "Home");
                
            }

            return NotFound();
        }



        [Route("/Tasker/About")]
        public IActionResult About()
        {
            ViewBag.GitHub = "https://github.com/LucasHSchuber?tab=repositories";
            ViewData["Bio"] = "Developer and designer from Stockholm, Sweden. Now looking for job in the tech industry. Lorem ipsum dolor sit amet consectetur adipisicing elit. Eius quaerat cum officiis omnis minima tempore voluptate nesciunt nam consectetur facere, alias ratione enim saepe atque porro asperiores voluptatem sed suscipit";
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
