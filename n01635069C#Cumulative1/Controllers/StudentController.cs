using n01635069C_Cumulative1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace n01635069C_Cumulative1.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        //GET: /Student/List
        public ActionResult List(string StudentSearchKey = null)
        {
            //pass teachers information into View
            StudentDataController controller = new StudentDataController();
            IEnumerable<Student> Students = controller.ListStudents(StudentSearchKey);

            return View(Students);
        }


        //GET: /Student/Show
        public ActionResult Show(int id)
        {
            StudentDataController controller = new StudentDataController();
            Student NewStudent = controller.FindStudent(id);

            return View(NewStudent);
        }
    }
}