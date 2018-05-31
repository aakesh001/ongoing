using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using RMSDemo.Models;
using RMSDemo.Utilities;

namespace RMSDemo.Controllers
{
    [LoginCheck]
    public class LiveController : BaseController
    {
        // GET: Live
        public ActionResult Index()
        {
            Service Service = new Service();
            List<Distributor> Distributors = Service.GetDistributor();
            ComapnyDeviceViewModel ComapnyDeviceViewModel = new ComapnyDeviceViewModel();
            ComapnyDeviceViewModel.Distributor = Distributors;
            return View(ComapnyDeviceViewModel);
        }

        [HttpPost]
        public JsonResult GetAssignedDevice(int distID, int CompanyId)
        {
            Service Service = new Service();
            List<RegisteredDevice> Companies = Service.GetAssignedDevice(distID, CompanyId);
            return Json(Companies, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetLiveData(string Distributor, string Company, string Assigned)
        {
            try
            {
                Service Service = new Service();
                var LiveQueue = Service.GetLiveQueue(Assigned);// Service.GetAssignedDevice(distID, CompanyId);
                return Json(LiveQueue, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult GetLiveDataBulk(string Assigned)
        {
            try
            {
                Service Service = new Service();
                var LiveQueues = Service.GetLiveQueueBulk(Assigned);
                return Json(LiveQueues, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult RebootDevice(string Assigned, bool Reboot, bool Reset)
        {
            try
            {
                Service Service = new Service();
                var LiveQueues = Service.UpdateRebootReset(Assigned, Reboot, Reset);
                return Json(LiveQueues, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
    }
}