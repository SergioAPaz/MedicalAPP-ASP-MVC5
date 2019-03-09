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
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,UserName,Rol,Name,BornDate,Password,LastLogin")] CT_Users cT_Users)
        {
            //VALIDA PRIMERO SI EL USUARIO EXISTE YA EN LA DB ANTES DE SALVAR Y EN CASO DE QUE YA EXISTA MUESTRA UN MENSAJE
            bool isViewNameInvalid = db.CT_Users.Any(v => v.UserName == cT_Users.UserName);
            if (isViewNameInvalid)
            {
                TempData["ShowModal1"] = 1;   
                return RedirectToAction("Index", cT_Users);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (cT_Users.Password != null && cT_Users.UserName != null)
                    {
                        string Key = "1234567890abcdef"; //key must have 16 chars, other wise you may get error "key size in not valid".
                        string Password2 = cT_Users.Password;
                        EncryptionModel Crypt = new EncryptionModel();
                        string EncryptedPassword = (string)Crypt.Crypt(CryptType.ENCRYPT, CryptTechnique.RIJ, Password2, Key);

                        cT_Users.Password = EncryptedPassword;
                        cT_Users.BornDate = DateTime.Now.ToString();
                        db.CT_Users.Add(cT_Users);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["ShowModal1"] = "2";
                        return RedirectToAction("Create", cT_Users);
                    }

                   
                }
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
