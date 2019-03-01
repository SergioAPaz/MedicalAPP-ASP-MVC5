using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MedicalApp.Models;

namespace MedicalApp.Controllers
{
    public class GestionDeUsuariosController : Controller
    {
        private MedicalAppEntities1 db = new MedicalAppEntities1();

        // GET: GestionDeUsuarios
        public ActionResult Index()
        {
            var cT_Users = db.CT_Users.Include(c => c.CT_Roles);
            return View(cT_Users.ToList());
        }

        // GET: GestionDeUsuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CT_Users cT_Users = db.CT_Users.Find(id);
            if (cT_Users == null)
            {
                return HttpNotFound();
            }
            return View(cT_Users);
        }

        // GET: GestionDeUsuarios/Create
        public ActionResult Create()
        {
            ViewBag.Rol = new SelectList(db.CT_Roles, "id", "Role");
            return View();
        }

        // POST: GestionDeUsuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,UserName,Rol,Name,BornDate,Password,LastLogin")] CT_Users cT_Users,int id)
        {

            CT_Users cT_Userss = db.CT_Users.Find(id);
            if (cT_Userss == null)
            {
                return HttpNotFound();
            }
            else
            {
                db.CT_Users.Add(cT_Users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
         

            ViewBag.Rol = new SelectList(db.CT_Roles, "id", "Role", cT_Users.Rol);
            return View(cT_Users);
        }

        // GET: GestionDeUsuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CT_Users cT_Users = db.CT_Users.Find(id);
            if (cT_Users == null)
            {
                return HttpNotFound();
            }
            ViewBag.Rol = new SelectList(db.CT_Roles, "id", "Role", cT_Users.Rol);
            return View(cT_Users);
        }

        // POST: GestionDeUsuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,UserName,Rol,Name,BornDate,Password,LastLogin")] CT_Users cT_Users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cT_Users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Rol = new SelectList(db.CT_Roles, "id", "Role", cT_Users.Rol);
            return View(cT_Users);
        }

        // GET: GestionDeUsuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CT_Users cT_Users = db.CT_Users.Find(id);
            if (cT_Users == null)
            {
                return HttpNotFound();
            }
            return View(cT_Users);
        }

        // POST: GestionDeUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CT_Users cT_Users = db.CT_Users.Find(id);
            db.CT_Users.Remove(cT_Users);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
