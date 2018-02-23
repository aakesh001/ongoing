﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using RMSDemo.Utilities;
using Entities;
using RMSDemo.Models;
using System.IO;
using System.Data;

namespace RMSDemo.Controllers
{
    [LoginCheck(RoleName = "Executive")]
    public class FarmerController : BaseController
    {
        // GET: Farmer
        public ActionResult Index()
        {
            try
            {
                FarmerViewModel FarmerViewModel = new FarmerViewModel();
                Service Service = new Service();
                FarmerViewModel.Companies = Service.GetCompany(UIHelper.UserSession.Id);
                //FarmerViewModel.Devices = Service.GetDitributorDevice(UIHelper.UserSession.Id);
                FarmerViewModel.States = Service.GetStates();
                FarmerViewModel.Villages = Service.GetVillages();
                FarmerViewModel.Mandal = Service.GetMandal();
                return View(FarmerViewModel);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Something wrong happended";
                return View();
            }
        }

        [HttpPost]
        public ActionResult InsertFarmer(int Devices, int Company, string farmerName, string Pono, string District, string Phone, int State, int Village, int Mandal, string Department, string Lattitude, string Longitude)
        {
            try
            {
                Service Service = new Service();
                Service.InsertFarmer(farmerName, Devices, Pono, District, Phone, State, Village, Mandal, Department, Lattitude, Longitude, UIHelper.UserSession.Id, Company);
                ViewBag.Error = "Farmer Added Succesfuly";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Something wrong happended";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public JsonResult GetFarmers()
        {
            try
            {
                Service Service = new Service();
                List<Farmer> Farmers = Service.GetFarmers(UIHelper.UserSession.Id);
                return Json(new { Result = "OK", Records = Farmers });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR" });
            }
        }

        [HttpPost]
        public JsonResult GetDevice(int CompanyId)
        {
            Service Service = new Service();
            List<RegisteredDevice> Companies = Service.GetAssignedDevice(UIHelper.UserSession.Id, CompanyId);
            return Json(Companies, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ImportExcel()
        {
            return View();
        }

        [ActionName("Importexcel")]
        [HttpPost]
        public ActionResult Importexcel1()
        {
            if (Request.Files["FileUpload1"].ContentLength > 0)
            {
                string extension = System.IO.Path.GetExtension(Request.Files["FileUpload1"].FileName).ToLower();
                string query = null;
                string connString = "";

                string[] validFileTypes = { ".xls", ".xlsx", ".csv" };

                string path1 = string.Format("{0}/{1}", Server.MapPath("~/Content/Uploads"), Request.Files["FileUpload1"].FileName);
                if (!Directory.Exists(path1))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Content/Uploads"));
                }
                if (validFileTypes.Contains(extension))
                {
                    if (System.IO.File.Exists(path1))
                    { System.IO.File.Delete(path1); }
                    Request.Files["FileUpload1"].SaveAs(path1);
                    if (extension == ".csv")
                    {
                        DataTable dt = UIHelper.ConvertCSVtoDataTable(path1);
                        ViewBag.Data = dt;
                    }
                    //Connection String to Excel Workbook
                    else if (extension.Trim() == ".xls")
                    {
                        connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path1 + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        DataTable dt = UIHelper.ConvertXSLXtoDataTable(path1, connString);
                        ViewBag.Data = dt;
                    }
                    else if (extension.Trim() == ".xlsx")
                    {
                        connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        DataTable dt = UIHelper.ConvertXSLXtoDataTable(path1, connString);
                        ViewBag.Data = dt;
                    }
                }
                else
                {
                    ViewBag.Error = "Please Upload Files in .xls, .xlsx or .csv format";
                }
            }

            return View();
        }
    }
}