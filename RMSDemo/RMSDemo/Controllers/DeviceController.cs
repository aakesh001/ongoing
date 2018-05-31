using DataAccess;
using Entities;
using RMSDemo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMSDemo.Controllers
{
    [LoginCheck]
    public class DeviceController : BaseController
    {
        // GET: Device
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddDevice(string Name, string OutFreq, string DcBusVolt, string OutputCurr, string InputPow
            , string OutputVolt, string OutputFreq, string DcBusVoltageFormat, string OutputCurrForm, string inptPwrFor, string OutVoltFor,
            string BaudRate, string Parity, string StopBit)
        {
            Service service = new Service();
            service.AddDevice(Name, OutFreq, DcBusVolt, OutputCurr, InputPow, OutputVolt, OutputFreq, DcBusVoltageFormat, OutputCurrForm, inptPwrFor, OutVoltFor, BaudRate, Parity, StopBit);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult GetModemResult()
        {
            Service Service = new Service();
            List<Device> Devicelist = Service.GetDevices();
            return Json(Devicelist, JsonRequestBehavior.AllowGet);
        }

        // usp_upd_Device
        [HttpPost]
        public JsonResult UpdateDevice(string id, string Name, string OutPutFrequency, string DCBusVoltage, string OutPutCurrent, string InputPower
            , string OutPutVoltage, string OutPutFreqFormat, string DcBusVoltageFormat, string OutPutCurrentFormat, string InputPowerFormat, string OutPutVolatageFormat,
            string BaudRate, string Parity, string StopBit)
        {
            Service Service = new Service();
            var DeviceUpdate = Service.UpdateDevice(int.Parse(id), Name, OutPutFrequency, DCBusVoltage, OutPutCurrent, InputPower
             , OutPutVoltage, OutPutFreqFormat, DcBusVoltageFormat, OutPutCurrentFormat, InputPowerFormat, OutPutVolatageFormat,
              BaudRate, Parity, StopBit);
            return Json(DeviceUpdate, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteDevice(string id)
        {
            Service Service = new Service();
            bool IsSuccess = Service.DeleteDevices(int.Parse(id));
            if (IsSuccess)
                return Json("Success", JsonRequestBehavior.AllowGet);
            else
                return Json("Failed", JsonRequestBehavior.AllowGet);
        }
    }
}