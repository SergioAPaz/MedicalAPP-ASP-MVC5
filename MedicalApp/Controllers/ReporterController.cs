using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicalApp.Models;
using MedicalApp.Models.dbOwnModels;
using ClosedXML;
using ClosedXML.Excel;
using System.IO;
using MedicalApp.Models.OwnModels;
using System.Text;
using System.Security.Cryptography;

namespace MedicalApp.Controllers
{
    public class ReporterController : Controller
    {
        Encryptor crypto = new Encryptor();
        // GET: Reporter
        public ActionResult Index()
        {
            try
            {
                ViewModel mymodel = new ViewModel();
                mymodel.PkUser = crypto.Encrypt(Session["PKUser"].ToString());
                return View(mymodel);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Login");
                throw;
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExportData1(ViewModel vm)
        {
            string query = "";
            try
            {
                 query = "SELECT * FROM [MedicalApp].[dbo].[Tareas] where Asignado='" + crypto.Decrypt(vm.PkUser) + "' ";
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Login");
                throw;
            }

            String constring = ConfigurationManager.ConnectionStrings["MiConexionLocal"].ConnectionString;
            SqlConnection con = new SqlConnection(constring);
           
            DataTable dt = new DataTable();
            dt.TableName = "Tareas";
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            da.Fill(dt);
            con.Close();

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= MisTareasAsignadas "+" "+ DateTime.Now+".xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            return RedirectToAction("Index", "ExportData");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult XlsTareas2(ViewModel vm)
        {
            string query = "";
            try
            {
                query = "SELECT * FROM [MedicalApp].[dbo].[Tareas] where Asignador='" + crypto.Decrypt(vm.PkUser) + "' ";
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Login");
                throw;
            }

            String constring = ConfigurationManager.ConnectionStrings["MiConexionLocal"].ConnectionString;
            SqlConnection con = new SqlConnection(constring);

            DataTable dt = new DataTable();
            dt.TableName = "Tareas";
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            da.Fill(dt);
            con.Close();

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= MisTareasAsignadas " + " " + DateTime.Now + ".xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            return RedirectToAction("Index", "ExportData");
        }



        // GET: Reporter/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reporter/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reporter/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reporter/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reporter/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reporter/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reporter/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


    }
}
