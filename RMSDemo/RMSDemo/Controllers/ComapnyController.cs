using DataAccess;
using Entities;
using RMSDemo.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMSDemo.Controllers
{
    [LoginCheck]
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
    }
}