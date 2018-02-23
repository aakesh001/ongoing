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
    public class StateVillageController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddState(string stateName)
        {
            try
            {
                Service service = new Service();
                service.AddState(stateName);
                List<State> States = service.GetStates();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        public JsonResult GetStates()
        {
            try
            {
                Service service = new Service();
                List<State> States = service.GetStates();
                return Json(new { Result = "OK", Records = States });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public JsonResult DeleteState(int ID)
        {
            try
            {
                Service service = new Service();
                bool IsSccuesfull = service.DelState(ID);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR" });
            }
        }

        public ActionResult AddVillage(string VilageName, int StateID)
        {
            try
            {
                Service service = new Service();
                service.AddVillage(VilageName, StateID);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        public JsonResult DeleteVillage(int ID)
        {
            try
            {
                Service service = new Service();
                bool IsSccuesfull = service.DelVillage(ID);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR" });
            }
        }

        public JsonResult GetVillage()
        {
            try
            {
                Service service = new Service();
                List<Village> Villages = service.GetVillages();
                return Json(new { Result = "OK", Records = Villages });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public JsonResult GetVillagefofrMandal(int Id)
        {
            try
            {
                Service service = new Service();
                List<Village> Villages = service.GetVillages(Id);
                return Json(new { Result = "OK", Records = Villages });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public ActionResult AddMandal(string mandalName, int VillageIdMandal, int StateIDMandal)
        {
            try
            {
                Service service = new Service();
                service.AddMandal(mandalName, VillageIdMandal, StateIDMandal);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        public JsonResult DeleteMandal(int ID)
        {
            try
            {
                Service service = new Service();
                bool IsSccuesfull = service.DelMandal(ID);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR" });
            }
        }

        public JsonResult GetMandal()
        {
            try
            {
                Service service = new Service();
                List<Mandal> Mandals = service.GetMandal();
                return Json(new { Result = "OK", Records = Mandals });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}