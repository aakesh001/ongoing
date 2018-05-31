using DataAccess;
using Entities;
using RMSDemo.Utilities;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RMSDemo.Controllers
{
    [LoginCheck]
    public class CompanyController : BaseController
    {
        // GET: Comapny

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCompany(string Name, HttpPostedFileBase file, string OrgName, string Address, string Email, string PhNum)
        {
            try
            {
                Service service = new Service();
                string FileName = Path.GetFileName(file.FileName);
                string _Path = Path.Combine(Server.MapPath("~/Uploads"), FileName);
                file.SaveAs(_Path);
                service.InsertCompany(Name, UIHelper.UserSession.Id, file.FileName, OrgName, Address, Email, PhNum);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult GetCompanies()
        {
            Service Service = new Service();
            List<Company> Companies = Service.GetCompany(UIHelper.UserSession.Id);
            return Json(new { Result = "OK", Records = Companies }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateCompany(Company company, HttpPostedFileBase file)
        {
            try
            {
                Service service = new Service();
                bool IsSuccessfull = service.UpdateCompany(company.Id, company.CompanyName, company.DistributorID, company.LogoPath, company.OrganizationName
                    , company.Address, company.Email, company.PhoneNumber);

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR" });
            }
        }

        [HttpPost]
        public JsonResult DeleteCompany(int ID)
        {
            Service service = new Service();
            bool IsSuccessfull = service.DeleteCompany(ID);
            return Json(new { Result = "OK" });
        }

        public ActionResult Dashboard()
        {
            Service service = new Service();

            if (UIHelper.UserSession.RoleID == "3")
            {
                var Data = service.GetDashboardUp(UIHelper.UserSession.Id, true);
                return View(Data);
            }
            else
            {
                var Data = service.GetDashboardUp(UIHelper.UserSession.Id, false);
                return View(Data);
            }
        }

        public JsonResult GetDashboardData(string name, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            Service service = new Service();
            var DashboardData = new List<Dashboard>();
            bool isCompanyUser;
            if (UIHelper.UserSession.RoleID == "3")
            {
                DashboardData = service.GetDashboardData(UIHelper.UserSession.Id, true);
            }
            else

                DashboardData = service.GetDashboardData(UIHelper.UserSession.Id, false);
            //if (System.Web.HttpContext.Current.Cache["DashboardData"] == null)
            //    {
            //        HttpContext.Cache.Insert("DashboardData", DashboardData, null, DateTime.Now.AddMinutes(15), System.Web.Caching.Cache.NoSlidingExpiration);
            //    }
            //    else
            //    {
            //        DashboardData = ControllerContext.HttpContext.Cache.Get("DashboardData") as List<Dashboard>;
            //    }
            //}

            if (!string.IsNullOrEmpty(name))
            {
                DashboardData = DashboardData.Where(p => p.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }
            DashboardData = SortDashBoardData(DashboardData, jtSorting);
            int Count = 1;
            foreach (var item in DashboardData)
            {
                item.SrNo = Count;
                Count++;
            }
            DashboardData = jtPageSize > 0
                   ? DashboardData.Skip(jtStartIndex).Take(jtPageSize).ToList() //Paging
                   : DashboardData.ToList(); //No paging

            return Json(new { Result = "OK", Records = DashboardData }, JsonRequestBehavior.AllowGet);
        }

        private List<Dashboard> SortDashBoardData(List<Dashboard> dashBoard, string sorting)
        {
            switch (sorting)
            {
                case "Name ASC":
                    dashBoard = dashBoard.OrderBy(p => p.Name).ToList();
                    break;

                case "Name DESC":
                    dashBoard = dashBoard.OrderByDescending(p => p.Name).ToList();
                    break;

                case "IMEI ASC":
                    dashBoard = dashBoard.OrderBy(p => p.IMEI).ToList();
                    break;

                case "IMEI DESC":
                    dashBoard = dashBoard.OrderByDescending(p => p.IMEI).ToList();
                    break;

                case "Frquency ASC":
                    dashBoard = dashBoard.OrderBy(p => p.Frquency).ToList();
                    break;

                case "Frquency DESC":
                    dashBoard = dashBoard.OrderByDescending(p => p.Frquency).ToList();
                    break;

                case "DcBusVoltage ASC":
                    dashBoard = dashBoard.OrderBy(p => p.DcBusVoltage).ToList();
                    break;

                case "DcBusVoltage DESC":
                    dashBoard = dashBoard.OrderByDescending(p => p.DcBusVoltage).ToList();
                    break;

                case "Current ASC":
                    dashBoard = dashBoard.OrderBy(p => p.Current).ToList();
                    break;

                case "Current DESC":
                    dashBoard = dashBoard.OrderByDescending(p => p.Current).ToList();
                    break;

                case "InPower ASC":
                    dashBoard = dashBoard.OrderBy(p => p.InPower).ToList();
                    break;

                case "InPower DESC":
                    dashBoard = dashBoard.OrderByDescending(p => p.InPower).ToList();
                    break;

                case "OutVolt ASC":
                    dashBoard = dashBoard.OrderBy(p => p.OutVolt).ToList();
                    break;

                case "OutVolt DESC":
                    dashBoard = dashBoard.OrderByDescending(p => p.OutVolt).ToList();
                    break;

                case "LastUpdate ASC":
                    dashBoard = dashBoard.OrderBy(p => p.LastUpdate).ToList();
                    break;

                case "LastUpdate DESC":
                    dashBoard = dashBoard.OrderByDescending(p => p.LastUpdate).ToList();
                    break;

                case "FarmerName ASC":
                    dashBoard = dashBoard.OrderBy(p => p.FarmerName).ToList();
                    break;

                case "FarmerName DESC":
                    dashBoard = dashBoard.OrderByDescending(p => p.FarmerName).ToList();
                    break;

                case "District ASC":
                    dashBoard = dashBoard.OrderBy(p => p.District).ToList();
                    break;

                case "District DESC":
                    dashBoard = dashBoard.OrderByDescending(p => p.District).ToList();
                    break;

                case "PhoneNumber ASC":
                    dashBoard = dashBoard.OrderBy(p => p.PhoneNumber).ToList();
                    break;

                case "PhoneNumber DESC":
                    dashBoard = dashBoard.OrderByDescending(p => p.PhoneNumber).ToList();
                    break;

                case "StateName ASC":
                    dashBoard = dashBoard.OrderBy(p => p.StateName).ToList();
                    break;

                case "StateName DESC":
                    dashBoard = dashBoard.OrderByDescending(p => p.StateName).ToList();
                    break;

                case "VillageName ASC":
                    dashBoard = dashBoard.OrderBy(p => p.VillageName).ToList();
                    break;

                case "VillageName DESC":
                    dashBoard = dashBoard.OrderByDescending(p => p.VillageName).ToList();
                    break;

                case "MandalName ASC":
                    dashBoard = dashBoard.OrderBy(p => p.MandalName).ToList();
                    break;

                case "MandalName DESC":
                    dashBoard = dashBoard.OrderByDescending(p => p.MandalName).ToList();
                    break;

                default:
                    //dashBoard = dashBoard.OrderBy(p => p.LastUpdate).ToList(); //Def
                    break;
            }
            return dashBoard;
        }

        [HttpPost]
        public JsonResult Uploadfile(HttpPostedFileBase file)
        {
            string FileName = Path.GetFileName(Request.Files[0].FileName);
            string _Path = Path.Combine(Server.MapPath("~/Uploads"), FileName);
            Request.Files[0].SaveAs(_Path);
            return Json(new { result = "ok" });
        }

        [Route("CompanyLiveData")]
        public ActionResult LiveData()
        {
            var Devices = new List<RegisteredDevice>();
            Service Service = new Service();
            if (UIHelper.UserSession.RoleName.ToLowerInvariant().Equals("distributor"))
            {
                Devices = Service.GetAssignedDevice(UIHelper.UserSession.Id, 0);
            }
            else
                if (UIHelper.UserSession.RoleName.ToLowerInvariant().Equals("executive"))
            {
                int CompanyId = Service.GetCompanyId(UIHelper.UserSession.Id);
                Devices = Service.GetAssignedDevice(0, CompanyId);
            }
            return View(Devices);
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

        public ActionResult GetCompanyReports()
        {
            try
            {
                var Devices = new List<RegisteredDevice>();
                Service Service = new Service();
                if (UIHelper.UserSession.RoleName.ToLowerInvariant().Equals("distributor"))
                {
                    Devices = Service.GetAssignedDevice(UIHelper.UserSession.Id, 0);
                }
                else
                    if (UIHelper.UserSession.RoleName.ToLowerInvariant().Equals("executive"))
                {
                    int CompanyId = Service.GetCompanyId(UIHelper.UserSession.Id);
                    Devices = Service.GetAssignedDevice(0, CompanyId);
                }
                return View(Devices);
            }
            catch (Exception ex)
            {
                return View(new List<RegisteredDevice>());
            }
        }

        [HttpGet]
        public ActionResult GetPdf(string deviceID, int userID)
        {
            var a = UIHelper.UserSession;
            var converter = new HtmlToPdf();
            converter.Options.MinPageLoadTime = 20;

            // set the page timeout
            converter.Options.MaxPageLoadTime = 100;
            var doc = converter.ConvertUrl((Request.IsSecureConnection ? "http://" : "http://") + Request.Url.Authority + (Request.ApplicationPath == "/" ? Request.ApplicationPath : Request.ApplicationPath + "/") +
          "/Company/LiveDataPDF?Devid=" + deviceID + "&userID=" + userID);
            //string URL = ConfigurationManager.AppSettings["PDFUrl"] + "/Company/LiveDataPDF?Devid=" + deviceID + "&userID=" + userID;
            //var doc = converter.ConvertUrl(URL);
            doc.Save(System.Web.HttpContext.Current.Response, true, "LiveData.pdf");
            doc.Close();

            return null;
        }

        public ActionResult LiveDataPDF(int userID, string Devid)
        {
            var Path = AppDomain.CurrentDomain.BaseDirectory + "ErrorLog";
            var FilePath = Path + "\\error.txt";
            System.IO.Directory.CreateDirectory(Path);
            try
            {
                var b = "id-" + Devid + " " + "UserID- " + userID.ToString();
                var Devices = new List<RegisteredDevice>();
                Service Service = new Service();
                Devices = Service.GetAssignedDevice(userID, 0).FindAll(f => f.IMEI.Equals(Devid));

                var d = "ListCount : " + Devices.Count;
                string[] lines = { b, d };

                System.IO.File.WriteAllLines(FilePath, lines);
                return View(Devices);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(FilePath, ex.Message);
                return View(new List<RegisteredDevice>());
            }
        }
    }
}