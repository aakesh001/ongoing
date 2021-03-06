﻿using RMSDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DataAccess;
using Entities;
using RMSDemo.Utilities;
using System.Web;

namespace RMSDemo.Controllers
{
    public class DetailController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok("It works!");
        }

        [HttpGet]
        [Route("Detail/Get")]
        public IHttpActionResult GetDetail(string imei, string lat, string lon, string ccid, string opname, string signal, string serial,
            string stime, string btime)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return this.BadRequest();
                }
                Service Service = new Service();
                var APIResponse = Service.GetDeviceDetailAPI(imei, lat, lon, ccid, opname, signal, serial, stime, btime);
                if (APIResponse != null)
                {
                    return this.Ok(APIResponse);
                }
                return (Json(new { Error = "NotFound" }));
            }
            catch (Exception ex)
            {
                return this.BadRequest();
            }
        }

        [HttpPost]
        [Route("Detail/Regsiter")]
        public IHttpActionResult RegisterUser(RMSUser user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return this.BadRequest();
                }
                Service service = new Service();
                user.UserName = user.UserName.ToLowerInvariant();
                user.Password = URLEncryptor.Encrypt(user.Password, String.Empty, String.Empty, String.Empty, 0, String.Empty, 0);
                service.RegsiterUser(user);
                return this.Ok("UserRegsiterd");
            }
            catch (Exception ex)
            {
                return this.BadRequest();
            }
        }

        [HttpPost]
        [Route("Detail/AddDist")]
        public IHttpActionResult InsertDistributor(Distributor Comp)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return this.BadRequest();
                }
                Service service = new Service();
                service.InsertDistributor(Comp.DistributorName);
                return this.Ok("Distributor Added");
            }
            catch (Exception ex)
            {
                return this.BadRequest();
            }
        }

        [HttpGet]
        [Route("Detail/GetDist")]
        public IHttpActionResult GetDistributor()
        {
            try
            {
                Service Service = new Service();
                var APIResponse = Service.GetDistributor();

                return this.Ok(APIResponse);
            }
            catch (Exception ex)
            {
                return this.BadRequest();
            }
        }

        [HttpPost]
        [Route("Detail/AddComp")]
        public IHttpActionResult InsertCompany(Company Comp)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return this.BadRequest();
                }
                Service service = new Service();
                service.InsertCompany(Comp.CompanyName, Comp.DistributorID);
                return this.Ok("Company Added");
            }
            catch (Exception ex)
            {
                return this.BadRequest();
            }
        }

        [HttpGet]
        [Route("Detail/AddQueue")]
        public IHttpActionResult InsertLiveQueu([FromUri] LiveQueue queue)
        {
            //string imei, string did, string freq, string dbvolt, string cur, string ipo, string ov
            try
            {
                List<DeviceData> Data = GetDeviceData();
                if (!ModelState.IsValid)
                {
                    return this.BadRequest();
                }
                if (queue.dc_bus_voltage < 1)
                {
                    queue.dc_bus_voltage = 318.1m;
                }
                if (queue.Energy == null)
                {
                    queue.Energy = (queue.output_voltage * queue.current * Convert.ToDecimal(0.25) / 1000).ToString("0.0");
                }
                if (Data != null && Data.Count > 0)
                {
                    var DeviceDetail = Data.Find(f => f.Imei.Equals(queue.imei));
                    if (DeviceDetail != null)
                    {
                        queue.WaterFlow = ((queue.in_power * DeviceDetail.Efficiency * Convert.ToDecimal(3.6) * 1000) / (DeviceDetail.Head * Convert.ToDecimal(9.81) * 100)).ToString("0.0");
                    }
                }

                Service service = new Service();
                RebootReset rebootReset = service.InsertLiveQueue(queue);
                if (rebootReset != null)
                {
                    return this.Ok(rebootReset);
                }
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.BadRequest();
            }
        }

        private List<DeviceData> GetDeviceData()
        {
            var DeviceDatas = new List<DeviceData>();
            List<DeviceData> deviceData = new List<DeviceData>();

            if (System.Web.HttpContext.Current.Cache["RegisteredDeviceData"] == null)
            {
                Service service = new Service();
                DeviceDatas = service.GetDeviceData();
                HttpRuntime.Cache.Insert("", DeviceDatas, null, DateTime.Now.AddMinutes(15), System.Web.Caching.Cache.NoSlidingExpiration);
            }
            else
            {
                DeviceDatas = HttpRuntime.Cache.Get("RegisteredDeviceData") as List<DeviceData>;
            }
            return DeviceDatas;
        }

        //[HttpGet]
        //[Route("Detail/GetComp")]
        //public IHttpActionResult GetCompany()
        //{
        //    try
        //    {
        //        Service Service = new Service();
        //        var APIResponse = Service.GetCompany();

        //        return this.Ok(APIResponse);
        //    }
        //    catch (Exception ex)
        //    {
        //        return this.BadRequest();
        //    }
        // }
    }
}