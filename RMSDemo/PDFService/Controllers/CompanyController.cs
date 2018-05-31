using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using SelectPdf;
using PDFService.Utilities;
using PDFService.Models;

namespace PDFService.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        [HttpGet]
        public ActionResult GetPdf(string deviceID, string Password, string userID)
        {
            string FileNAame = "";
            Service service = new Service();
            RMSUser User = service.ValidateUser(userID.ToLowerInvariant(), Password);
            UIHelper.UserSession = User;
            if (User.RoleName.ToLowerInvariant().Equals("executive"))
            {
                int CompanyId = service.GetCompanyId(UIHelper.UserSession.Id);

                FileNAame = service.GetLogoPath(CompanyId);
                UIHelper.UserSession.LogoPath = "/Uploads/" + FileNAame;
            }
            else
            if (User.RoleName.ToLowerInvariant().Equals("distributor"))
            {
                if (User.LogoPath != null || User.LogoPath != "")
                {
                    UIHelper.UserSession.LogoPath = "/Uploads/" + User.LogoPath;
                }
            }
            var converter = new HtmlToPdf();
            converter.Options.MinPageLoadTime = 5;
            converter.Options.MaxPageLoadTime = 50;
            var doc = converter.ConvertUrl((Request.IsSecureConnection ? "http://" : "http://") + Request.Url.Authority + (Request.ApplicationPath == "/" ? Request.ApplicationPath : Request.ApplicationPath + "/") +
          "/Company/LiveDataPDF?Devid=" + deviceID + "&userID=" + User.Id + "&UserName=" + User.FirstName + User.LastName + "&Path=" + UIHelper.UserSession.LogoPath + "&role="
          + User.RoleName);

            doc.Save(System.Web.HttpContext.Current.Response, true, "LiveData.pdf");
            doc.Close();

            return null;
        }

        public ActionResult LiveDataPDF(int userID, string Devid, string UserName, string Path, string role)
        {
            try
            {
                LiveViewModel liveViewModel = new LiveViewModel();
                var Devices = new List<RegisteredDevice>();
                Service Service = new Service();
                if (role.ToLowerInvariant().Equals("distributor"))
                {
                    Devices = Service.GetAssignedDevice(userID, 0).FindAll(f => f.IMEI.Equals(Devid));
                }
                else
               if (role.ToLowerInvariant().Equals("executive"))
                {
                    int CompanyId = Service.GetCompanyId(userID);
                    Devices = Service.GetAssignedDevice(0, CompanyId).FindAll(f => f.IMEI.Equals(Devid));
                }

                liveViewModel.Devices = Devices;
                liveViewModel.Username = UserName;
                liveViewModel.Path = Path;
                return View(liveViewModel);
            }
            catch (Exception ex)
            {
                return View(new List<RegisteredDevice>());
            }
        }

        [HttpPost]
        public JsonResult GetCompanyLiveData(string Assigned)
        {
            try
            {
                Service Service = new Service();
                var LiveQueue = Service.GetLiveQueue(Assigned);
                return Json(new { LiveQueue.Item1, LiveQueue.Item2 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
    }
}