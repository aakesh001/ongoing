using DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMSDemo.Models;
using RMSDemo.Utilities;
using System.IO;

namespace RMSDemo.Controllers
{
    [LoginCheck]
    public class ReportsController : BaseController
    {
        // GET: Reports
        public ActionResult Index()
        {
            Service Service = new Service();
            List<Distributor> Distributors = Service.GetDistributor();
            ComapnyDeviceViewModel ComapnyDeviceViewModel = new ComapnyDeviceViewModel();
            ComapnyDeviceViewModel.Distributor = Distributors;
            return View(ComapnyDeviceViewModel);
        }

        [HttpPost]
        public JsonResult GetAssignedDevice()
        {
            Service Service = new Service();
            List<RegisteredDevice> Companies = Service.GetUnAssignedDevice();
            return Json(Companies, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetLiveDataForReport(string Assigned, string StartDate, string StartHour, string EndDate, string EndHour)
        {
            //System.Globalization.CultureInfo enUS = new System.Globalization.CultureInfo("en-US");
            try
            {
                //String jsonPostedData = Assigned + " " + StartDate + " " + StartHour + " " + EndDate + " " + EndHour;
                //string path = AppDomain.CurrentDomain.BaseDirectory;
                //if (!Directory.Exists(Path.Combine(path, "SofortLog")))
                //    Directory.CreateDirectory(Path.Combine(path, "SofortLog"));
                //path = Path.Combine(path, "SofortLog");
                //string callNameSuffix = DateTime.Now.ToString("ddMMyyyyhhmmssfff") + ".json";
                //System.IO.File.WriteAllText(path + "\\" + "Sofort_" + callNameSuffix, jsonPostedData);

                StartDate = StartDate.Trim() + " " + StartHour.Trim();
                EndDate = EndDate.Trim() + " " + EndHour.Trim();
                DateTime startDate = DateTime.ParseExact(StartDate, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                DateTime endDate = DateTime.ParseExact(EndDate, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                Service Service = new Service();
                var LiveQueues = Service.GetLiveQueueBulkReport(Assigned, startDate, endDate);
                return Json(LiveQueues, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}