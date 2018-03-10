using DataAccess;
using Entities;
using RMSDemo.Utilities;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RMSDemo.Controllers
{
    //[LoginCheck]
    public class CompanyController : BaseController
    {
        // GET: Comapny

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCompany(string Name, HttpPostedFileBase file, string OrgName, string Address, string Email, string PhNum)
        {
            try
            {
                Service service = new Service();
                string FileName = Path.GetFileName(file.FileName);
                string _Path = Path.Combine(Server.MapPath("~/Uploads"), FileName);
                file.SaveAs(_Path);
                service.InsertCompany(Name, UIHelper.UserSession.Id, file.FileName, OrgName, Address, Email, PhNum);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult GetCompanies()
        {
            Service Service = new Service();
            List<Company> Companies = Service.GetCompany(UIHelper.UserSession.Id);
            return Json(new { Result = "OK", Records = Companies }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateCompany(Company company)
        {
            try
            {
                Service service = new Service();
                bool IsSuccessfull = service.UpdateCompany(company.Id, company.CompanyName, company.DistributorID, company.LogoPath, company.OrganizationName
                    , company.Address, company.Email, company.PhoneNumber);

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR" });
            }
        }

        [HttpPost]
        public JsonResult DeleteCompany(int ID)
        {
            Service service = new Service();
            bool IsSuccessfull = service.DeleteCompany(ID);
            return Json(new { Result = "OK" });
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        [Route("CompanyLiveData")]
        public ActionResult LiveData()
        {
            var Devices = new List<RegisteredDevice>();
            Service Service = new Service();
            if (UIHelper.UserSession.RoleName.ToLowerInvariant().Equals("distributor"))
            {
                Devices = Service.GetAssignedDevice(UIHelper.UserSession.Id, 0);
            }
            else
                if (UIHelper.UserSession.RoleName.ToLowerInvariant().Equals("executeive"))
            {
                int CompanyId = Service.GetCompanyId(UIHelper.UserSession.Id);
                Devices = Service.GetAssignedDevice(0, CompanyId);
            }
            return View(Devices);
        }

        [HttpPost]
        public JsonResult GetCompanyLiveData(string Assigned)
        {
            try
            {
                Service Service = new Service();
                var LiveQueue = Service.GetLiveQueue(Assigned);
                return Json(LiveQueue, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetCompanyReports()
        {
            try
            {
                var Devices = new List<RegisteredDevice>();
                Service Service = new Service();
                if (UIHelper.UserSession.RoleName.ToLowerInvariant().Equals("distributor"))
                {
                    Devices = Service.GetAssignedDevice(UIHelper.UserSession.Id, 0);
                }
                else
                    if (UIHelper.UserSession.RoleName.ToLowerInvariant().Equals("executeive"))
                {
                    int CompanyId = Service.GetCompanyId(UIHelper.UserSession.Id);
                    Devices = Service.GetAssignedDevice(0, CompanyId);
                }
                return View(Devices);
            }
            catch (Exception ex)
            {
                return View(new List<RegisteredDevice>());
            }
        }

        [HttpGet]
        public ActionResult GetPdf(string DeviceID)
        {
            var a = UIHelper.UserSession;
            var converter = new HtmlToPdf();
            converter.Options.MinPageLoadTime = 20;

            // set the page timeout
            converter.Options.MaxPageLoadTime = 100;
            //  var doc = converter.ConvertUrl((Request.IsSecureConnection ? "http://" : "http://") + Request.Url.Authority + (Request.ApplicationPath == "/" ? Request.ApplicationPath : Request.ApplicationPath + "/") +
            //"/Company/LiveDataPDF?id=" + DeviceID + "&userID=" + UIHelper.UserSession.Id);
            string URL = ConfigurationManager.AppSettings["PDFUrl"] + "/Company/LiveDataPDF?Devid=" + DeviceID + "&userID=" + UIHelper.UserSession.Id;
            var doc = converter.ConvertUrl(URL);
            doc.Save(System.Web.HttpContext.Current.Response, true, "LiveData.pdf");
            doc.Close();

            return null;
        }

        public ActionResult LiveDataPDF(int userID, string Devid)
        {
            var Path = AppDomain.CurrentDomain.BaseDirectory + "ErrorLog";
            var FilePath = Path + "\\error.txt";
            System.IO.Directory.CreateDirectory(Path);
            try
            {
                var b = "id-" + Devid + " " + "UserID- " + userID.ToString();
                var Devices = new List<RegisteredDevice>();
                Service Service = new Service();
                Devices = Service.GetAssignedDevice(userID, 0).FindAll(f => f.IMEI.Equals(Devid));

                var d = "ListCount : " + Devices.Count;
                string[] lines = { b, d };

                System.IO.File.WriteAllLines(FilePath, lines);
                return View(Devices);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(FilePath, ex.Message);
                return View(new List<RegisteredDevice>());
            }
        }
    }
}