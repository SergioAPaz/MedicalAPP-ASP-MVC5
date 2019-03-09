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
using System.Dynamic;


namespace MedicalApp.Controllers
{

    public class HomeController : Controller
    {
        private MedicalAppEntities1 db = new MedicalAppEntities1();

        public ActionResult Index()
        {
            ViewBag.Asignado = new SelectList(db.CT_Users, "id", "UserName");
            ViewBag.Asignador = new SelectList(db.CT_Users, "id", "UserName");


            ViewModel mymodel = new ViewModel();
            mymodel.TareasIE = db.Tareas.Include(c => c.CT_Users);
            return View(mymodel);

            //ViewBag.Rol = new SelectList(db.CT_Roles, "id", "Role");
            //return View();

            //var TTareas = db.Tareas.Include(c => c.CT_Users);
            //return View(TTareas.ToList());
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
        public ActionResult Create()
        {
            ViewBag.Asignado = new SelectList(db.CT_Users, "id", "UserName");
            ViewBag.Asignador = new SelectList(db.CT_Users, "id", "UserName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModel tareaf)
        {
            if (ModelState.IsValid)
            {
                //Se pasan los campos de uno por uno a la tabla Tareas de SQL debido a que los datos provienen de un ViewModel(ClaseAuxiliar) para poder modificar 
                //el modelo sin que se pierdan los cambios al recrear la base de datos
                db.Tareas.Add(new Tareas{
                    Fecha =DateTime.Now,
                    Asignador = tareaf.TareasFC.Asignador,
                    TituloTarea = tareaf.TareasFC.TituloTarea,
                    Descripcion = tareaf.TareasFC.Descripcion,
                    Asignado = tareaf.TareasFC.Asignado,
                    FechaLimite = tareaf.TareasFC.FechaLimite,
                    Adjunto = tareaf.TareasFC.Adjunto,
                });
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ShowModal1"] = "FaltanDatos";
                return RedirectToAction("Index");
            }

            //ViewBag.Asignado = new SelectList(db.CT_Users, "id", "UserName");
            //ViewBag.Asignador = new SelectList(db.CT_Users, "id", "UserName");
            //return View(tareaf);
        }



    }
}