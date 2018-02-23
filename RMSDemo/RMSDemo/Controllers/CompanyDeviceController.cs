using DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMSDemo.Models;
using RMSDemo.Utilities;

namespace RMSDemo.Controllers
{
    [LoginCheck]
    public class CompanyDeviceController : BaseController
    {
        public ActionResult Index()
        {
            Service Service = new Service();
            List<Distributor> Distributors = Service.GetDistributor();
            ComapnyDeviceViewModel ComapnyDeviceViewModel = new ComapnyDeviceViewModel();
            ComapnyDeviceViewModel.Distributor = Distributors;
            return View(ComapnyDeviceViewModel);
        }

        [HttpPost]
        public JsonResult GetCompany(int distID)
        {
            Service Service = new Service();
            List<Company> Companies = Service.GetCompany(distID);
            return Json(Companies, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetUnAssignedDevice()
        {
            Service Service = new Service();
            List<RegisteredDevice> Companies = Service.GetUnAssignedDevice();
            return Json(Companies, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateCompanyDevice(List<string> assignedValues, string DisttributorName, string CompName)
        {
            try
            {
                Service Service = new Service();
                int DistributorID = 0;
                int CompanyID = 0;
                if (!string.IsNullOrEmpty(DisttributorName))
                {
                    DistributorID = int.Parse(DisttributorName);
                    //return Error
                }
                if (!string.IsNullOrEmpty(CompName))
                {
                    CompanyID = int.Parse(CompName);
                    //return Error
                }
                foreach (var id in assignedValues)
                {
                    bool Companies = Service.UpdateUnAssignedDevice(int.Parse(id), DistributorID, CompanyID);
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}