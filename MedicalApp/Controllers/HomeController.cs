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
using System.IO;
using System.Diagnostics;
using System.Text;

namespace MedicalApp.Controllers
{

    public class HomeController : Controller
    {
        private MedicalAppEntities1 db = new MedicalAppEntities1();

        public ActionResult Index()
        {
            ViewBag.Asignado = new SelectList(db.CT_Users, "id", "Name");
            ViewBag.Asignador = new SelectList(db.CT_Users, "id", "UserName");
            
            ViewModel mymodel = new ViewModel();
            if (Session["FullUserName"] != null)
            {
                mymodel.FulluserName = Session["FullUserName"].ToString();
                mymodel.UserName = Session["User"].ToString();
                mymodel.PkUser = Session["PKUser"].ToString();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
          

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
            TempData["ShowModal1"] = "TareaEliminada";
            return RedirectToAction("Index");

        }



        
        public ActionResult Downloads(string Adjunto)
        {
            string filename = Convert.ToString(Adjunto);
            string serverpath = Server.MapPath("~/App_Data/uploads/");
            string filepath = serverpath + filename;
            return File(filepath, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }



        public ActionResult Create()
        {
            ViewBag.Asignado = new SelectList(db.CT_Users, "id", "UserName");
            ViewBag.Asignador = new SelectList(db.CT_Users, "id", "UserName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModel tareaf, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                //Se pasan los campos de uno por uno a la tabla Tareas de SQL debido a que los datos provienen de un ViewModel(ClaseAuxiliar) para poder modificar 
                //el modelo sin que se pierdan los cambios al recrear la base de datos
                string FileExtension="Empty";
                long FileName2=0;
                bool ExisteArchivo = false;
                try
                {
                    if (file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        FileExtension = Path.GetExtension(fileName);
                        FileName2 = (long)(DateTime.UtcNow - new DateTime(2015, 1, 1)).TotalMilliseconds;
                        var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), string.Concat(Convert.ToString(FileName2), FileExtension));
                        file.SaveAs(path);
                        ExisteArchivo = true;
                    }
                    
                }
                catch (Exception )
                {

                }
                
                try
                {
                    if (ExisteArchivo==true)
                    {
                        db.Tareas.Add(new Tareas
                        {
                            Fecha = DateTime.Now,
                            Asignador = tareaf.TareasFC.Asignador,
                            TituloTarea = tareaf.TareasFC.TituloTarea,
                            Descripcion = tareaf.TareasFC.Descripcion,
                            Asignado = tareaf.TareasFC.Asignado,
                            FechaLimite = tareaf.TareasFC.FechaLimite,
                            Adjunto = string.Concat(Convert.ToString(FileName2), FileExtension),
                        });
                    }
                    else
                    {
                        db.Tareas.Add(new Tareas
                        {
                            Fecha = DateTime.Now,
                            Asignador = tareaf.TareasFC.Asignador,
                            TituloTarea = tareaf.TareasFC.TituloTarea,
                            Descripcion = tareaf.TareasFC.Descripcion,
                            Asignado = tareaf.TareasFC.Asignado,
                            FechaLimite = tareaf.TareasFC.FechaLimite,
                            Adjunto = tareaf.TareasFC.Adjunto,
                        });
                    }
                    

                    db.SaveChanges();
                    TempData["ShowModal1"] = "Exito";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["msg"] = "<script>alert('Error al generar tarea. ');</script>";

                    SendErrorEmail(ex.ToString());
                    return RedirectToAction("Index");
                }
                
            }
            else
            {
                TempData["ShowModal1"] = "1";
                return RedirectToAction("Index");
            }

            //ViewBag.Asignado = new SelectList(db.CT_Users, "id", "UserName");
            //ViewBag.Asignador = new SelectList(db.CT_Users, "id", "UserName");
            //return View(tareaf);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProgrammedTask(ViewModel tareaf, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                //Se pasan los campos de uno por uno a la tabla Tareas de SQL debido a que los datos provienen de un ViewModel(ClaseAuxiliar) para poder modificar 
                //el modelo sin que se pierdan los cambios al recrear la base de datos
                string FileExtension = "Empty";
                long FileName2 = 0;
                bool ExisteArchivo = false;
                try
                {
                    if (file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        FileExtension = Path.GetExtension(fileName);
                        FileName2 = (long)(DateTime.UtcNow - new DateTime(2015, 1, 1)).TotalMilliseconds;
                        var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), string.Concat(Convert.ToString(FileName2), FileExtension));
                        file.SaveAs(path);
                        ExisteArchivo = true;
                    }

                }
                catch (Exception)
                {

                }

                try
                {
                    if (ExisteArchivo == true)
                    {
                        db.Tareas.Add(new Tareas
                        {
                            Fecha = DateTime.Now,
                            Asignador = tareaf.TareasFC.Asignador,
                            TituloTarea = tareaf.TareasFC.TituloTarea,
                            Descripcion = tareaf.TareasFC.Descripcion,
                            Asignado = tareaf.TareasFC.Asignado,
                            FechaLimite = tareaf.TareasFC.FechaLimite,
                            Adjunto = string.Concat(Convert.ToString(FileName2), FileExtension),
                        });
                    }
                    else
                    {
                        db.Tareas.Add(new Tareas
                        {
                            Fecha = DateTime.Now,
                            Asignador = tareaf.TareasFC.Asignador,
                            TituloTarea = tareaf.TareasFC.TituloTarea,
                            Descripcion = tareaf.TareasFC.Descripcion,
                            Asignado = tareaf.TareasFC.Asignado,
                            FechaLimite = tareaf.TareasFC.FechaLimite,
                            Adjunto = tareaf.TareasFC.Adjunto,
                        });
                    }


                    db.SaveChanges();
                    TempData["ShowModal1"] = "Exito";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["msg"] = "<script>alert('Error al generar tarea. ');</script>";

                    SendErrorEmail(ex.ToString());
                    return RedirectToAction("Index");
                }

            }
            else
            {
                TempData["ShowModal1"] = "1";
                return RedirectToAction("Index");
            }

            //ViewBag.Asignado = new SelectList(db.CT_Users, "id", "UserName");
            //ViewBag.Asignador = new SelectList(db.CT_Users, "id", "UserName");
            //return View(tareaf);
        }


        public void SendErrorEmail(string Error)
        {

            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            message.From = new System.Net.Mail.MailAddress("sergio.pazholguin@gmail.com");
            message.To.Add(new System.Net.Mail.MailAddress("sergio.pazholguin@gmail.com"));

            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = "subject";
            message.Body = "Error: "+Error+"";

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.Send(message);
        }

        



    }
}