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

        #region State

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

        public ActionResult UpdateState(int Id, string StateName)
        {
            try
            {
                Service service = new Service();
                bool IsSuccess = service.UpdateState(Id, StateName);
                return Json(new { Result = "OK", });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
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

        #endregion State

        #region District

        public ActionResult AddDistrict(string DistrictName, int StateID)
        {
            try
            {
                Service service = new Service();
                service.AddDistrict(DistrictName, StateID);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        public JsonResult DeleteDistrict(int ID)
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

        public JsonResult GetDistrict()
        {
            try
            {
                Service service = new Service();
                List<District> Districts = service.GetDistricts();
                return Json(new { Result = "OK", Records = Districts });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public JsonResult GetDistrictByStateID(int stateID)
        {
            try
            {
                Service service = new Service();
                List<District> Districts = service.GetDistricts(stateID);
                return Json(new { Result = "OK", Records = Districts });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public ActionResult UpdateDistrict(int Id, string districtName, int stateID)
        {
            try
            {
                Service service = new Service();
                bool IsSuccess = service.UpdateDistrict(Id, districtName, stateID);
                return Json(new { Result = "OK", });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        #endregion District

        #region Village

        public ActionResult AddVillage(string VilageName, int StateID, int DistID)
        {
            try
            {
                Service service = new Service();
                service.AddVillage(VilageName, StateID, DistID);

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

        public JsonResult GetVillage(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                Service service = new Service();
                List<Village> Villages = service.GetVillages();
                Villages = SortVillage(Villages, jtSorting);
                Villages = jtPageSize > 0
                       ? Villages.Skip(jtStartIndex).Take(jtPageSize).ToList() //Paging
                       : Villages.ToList(); //No paging
                return Json(new { Result = "OK", Records = Villages });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        private List<Village> SortVillage(List<Village> Village, string sorting)
        {
            switch (sorting)
            {
                case "VillageName ASC":
                    Village = Village.OrderBy(p => p.VillageName).ToList();
                    break;

                case "VillageName DESC":
                    Village = Village.OrderByDescending(p => p.VillageName).ToList();
                    break;

                default:
                    Village = Village.OrderBy(p => p.Id).ToList(); //Def
                    break;
            }
            return Village;
        }

        //public JsonResult GetVillagebyDistrictID(int distID)
        //{
        //}

        public ActionResult UpdateVillage(int Id, string VillageName, int StateID, int DistrictID)
        {
            try
            {
                Service service = new Service();
                bool IsSuccess = service.UpdateVillage(Id, VillageName, StateID, DistrictID);
                return Json(new { Result = "OK", });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        #endregion Village

        #region Mandal

        public JsonResult GetVillagefofrMandal(int Id, int distID)
        {
            try
            {
                Service service = new Service();
                List<Village> Villages = service.GetVillages(Id, distID);
                return Json(new { Result = "OK", Records = Villages });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public ActionResult AddMandal(string mandalName, int DistIdMandal, int VillageIdMandal, int StateIDMandal)
        {
            try
            {
                Service service = new Service();
                service.AddMandal(mandalName, VillageIdMandal, StateIDMandal, DistIdMandal);
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

        public JsonResult GetMandal(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                Service service = new Service();
                List<Mandal> Mandals = service.GetMandal();
                Mandals = SortMandal(Mandals, jtSorting);
                Mandals = jtPageSize > 0
                       ? Mandals.Skip(jtStartIndex).Take(jtPageSize).ToList() //Paging
                       : Mandals.ToList(); //No paging
                return Json(new { Result = "OK", Records = Mandals });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        private List<Mandal> SortMandal(List<Mandal> Mandal, string sorting)
        {
            switch (sorting)
            {
                case "MandalName ASC":
                    Mandal = Mandal.OrderBy(p => p.MandalName).ToList();
                    break;

                case "MandalName DESC":
                    Mandal = Mandal.OrderByDescending(p => p.MandalName).ToList();
                    break;

                default:
                    Mandal = Mandal.OrderBy(p => p.Id).ToList(); //Def
                    break;
            }
            return Mandal;
        }

        public ActionResult UpdateMandal(int Id, string VillageName, int StateID, int DistrictID, int VillageID)
        {
            try
            {
                Service service = new Service();
                bool IsSuccess = service.UpdateMandal(Id, VillageName, StateID, DistrictID, VillageID);
                return Json(new { Result = "OK", });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        #endregion Mandal
    }
}