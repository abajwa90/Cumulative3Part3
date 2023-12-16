using MiniviableProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MiniviableProject.Controllers
{
    public class TeachersController : Controller
    {
        TeachersDataController controller = new TeachersDataController();

        // GET: Teachers/list/{searchKey}
        public ActionResult List(string searchKey = "")
        {
            try
            {
                IEnumerable<Teacher> Teachers = controller.GetTeachers(searchKey);
                return View(Teachers);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("error", "Teachers");
            }
        }

        //GET : Teachers/Show/{id}
        public ActionResult Show(int id)
        {
            try
            {
                Teacher Teacher = controller.GetTeacherDetails(id);
                return View(Teacher);
            }
            catch (Exception ex)
            {
                
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("error", "Teachers");
            }
        }


        //GET : Teachers/Add
        public ActionResult Add()
        {
            try
            {
             
                return View();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("error", "Teachers");
            }
        }

    }
}
