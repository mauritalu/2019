using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JSONTest.Models;

namespace JSONTest.Controllers
{
    public class RabbitController : Controller
    {
        // GET: Rabbit
        public ActionResult Index()
        {
            var studentList = new List<RabbitModel>{
                            new RabbitModel() { StudentId = 1, StudentName = "John", Age = 18 } ,
                            new RabbitModel() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                            new RabbitModel() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                            new RabbitModel() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                            new RabbitModel() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                            new RabbitModel() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                            new RabbitModel() { StudentId = 4, StudentName = "Rob" , Age = 19 }
                        };
            // Get the students from the database in the real application

            return View(studentList);
        }
    }
}