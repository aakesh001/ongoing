using RMSDemo.Models;
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
                if (!ModelState.IsValid)
                {
                    return this.BadRequest();
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