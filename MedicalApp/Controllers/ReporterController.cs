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


namespace MedicalApp.Controllers
{
    public class ReporterController : Controller
    {
        // GET: Reporter
        public ActionResult Index()
        {
            ViewModel mymodel = new ViewModel();
            mymodel.PkUser = Convert.ToInt16(Session["PKUser"]);
            return View(mymodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExportData1(ViewModel vm)
        {
           
            String constring = ConfigurationManager.ConnectionStrings["MiConexionLocal"].ConnectionString;
            SqlConnection con = new SqlConnection(constring);
            string query = "SELECT * FROM [MedicalApp].[dbo].[Tareas] where Asignado='"+ vm.PkUser+ "' ";
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
