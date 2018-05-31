using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using Entities;
using RMSDemo.Utilities;

namespace RMSDemo.Controllers
{
    [LoginCheck]
    public class AddDeviceController : BaseController
    {
        public ActionResult Index()
        {
            Service Service = new Service();
            List<Device> Devicelist = Service.GetDeviceModels();
            ViewBag.DeviceModels = Devicelist;
            return View();
        }

        public ActionResult AddNewDevice(string Name, string DeviceId, string IMEI, string ModelId, bool Enable, string MotorEfficiency, string Head,
           string MotorPower, bool MotorControl, string BenificiaryName, string WorkOrderNo, string District, string Phone, string VFD, string Address, string Interval)
        {
            Service Service = new Service();
            var DeviceAdded = Service.AddRegisteredDevice(Name, DeviceId, IMEI, ModelId, Enable, MotorEfficiency, Head, MotorPower, MotorControl, BenificiaryName, WorkOrderNo, District, Phone, VFD, Address, Interval);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult GetRegisteredDevice()
        {
            Service Service = new Service();
            List<RegisteredDevice> Devicelist = Service.GetRegisteredDevices();
            return Json(Devicelist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateRegisteredDevice(int Id, string Name, string DeviceId, string IMEI, string RegisteredDeviceId, bool Enabled, string MotorEfficiency, string Head,
           string MotorPower, bool MotorControl, string BenificiaryName, string WorkOrderNo, string District, string Phone, string VFD, string Address)
        {
            Service Service = new Service();
            var DeviceUpdated = Service.UpdateRegisteredDevice(Id, Name, DeviceId, IMEI, RegisteredDeviceId, Enabled, MotorEfficiency, Head, MotorPower, MotorControl, BenificiaryName, WorkOrderNo, District, Phone, VFD, Address);
            return Json("Successfull", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteRegsiteredDevice(string id)
        {
            Service Service = new Service();
            bool IsSuccess = Service.DeleteRegisteredDevices(int.Parse(id));
            if (IsSuccess)
                return Json("Success", JsonRequestBehavior.AllowGet);
            else
                return Json("Failed", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDeviceModels()
        {
            Service Service = new Service();
            List<Device> Devicelist = Service.GetDeviceModels();
            return Json(Devicelist, JsonRequestBehavior.AllowGet);
        }
    }
}