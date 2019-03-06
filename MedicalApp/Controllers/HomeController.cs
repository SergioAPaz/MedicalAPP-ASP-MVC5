using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MedicalApp.Models;
using MedicalApp.Models.dbOwnModels;
using MedicalApp.Models.OwnModels;


namespace MedicalApp.Controllers
{

    public class HomeController : Controller
    {
        private MedicalAppEntities1 db = new MedicalAppEntities1();

        public ActionResult Index()
        {
            var TTareas = db.Tareas.Include(c => c.CT_Users);
            return View(TTareas.ToList());
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

       
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                Tareas TTareas = db.Tareas.Find(id);
                db.Tareas.Remove(TTareas);
                db.SaveChanges();
                
            }
            return RedirectToAction("Index");

        }



    }
}