using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using RMSDemo.Utilities;
using Entities;
using RMSDemo.Models;
using System.IO;
using System.Data;
using OfficeOpenXml;

namespace RMSDemo.Controllers
{
    [LoginCheck(RoleName = "Executive")]
    public class FarmerController : BaseController
    {
        // GET: Farmer
        public ActionResult Index()
        {
            try
            {
                FarmerViewModel FarmerViewModel = new FarmerViewModel();
                Service Service = new Service();
                FarmerViewModel.Companies = Service.GetCompany(UIHelper.UserSession.Id);
                //FarmerViewModel.Devices = Service.GetDitributorDevice(UIHelper.UserSession.Id);
                FarmerViewModel.States = Service.GetStates();
                FarmerViewModel.Villages = Service.GetVillages();
                FarmerViewModel.Mandal = Service.GetMandal();
                return View(FarmerViewModel);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Something wrong happended";
                return View();
            }
        }

        [HttpPost]
        public ActionResult InsertFarmer(int Device, int Company, string farmerName, string Pono, string District, string Phone, int State, int Village, int Mandal, string Department, string Lattitude, string Longitude)
        {
            try
            {
                Service Service = new Service();
                Service.InsertFarmer(farmerName, Device, Pono, District, Phone, State, Village, Mandal, Department, Lattitude, Longitude, UIHelper.UserSession.Id, Company);
                ViewBag.Error = "Farmer Added Succesfuly";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Something wrong happended";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public JsonResult GetFarmers(string name, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                Service Service = new Service();
                List<Farmer> Farmers = Service.GetFarmers(UIHelper.UserSession.Id);
                if (!string.IsNullOrEmpty(name))
                {
                    Farmers = Farmers.Where(p => p.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                }
                Farmers = SortFarmer(Farmers, jtSorting);
                Farmers = jtPageSize > 0
                       ? Farmers.Skip(jtStartIndex).Take(jtPageSize).ToList() //Paging
                       : Farmers.ToList(); //No paging
                return Json(new { Result = "OK", Records = Farmers });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR" });
            }
        }

        private List<Farmer> SortFarmer(List<Farmer> farmer, string sorting)
        {
            switch (sorting)
            {
                case "Name ASC":
                    farmer = farmer.OrderBy(p => p.Name).ToList();
                    break;

                case "Name DESC":
                    farmer = farmer.OrderByDescending(p => p.Name).ToList();
                    break;

                case "IMEI ASC":
                    farmer = farmer.OrderBy(p => p.IMEI).ToList();
                    break;

                case "IMEI DESC":
                    farmer = farmer.OrderByDescending(p => p.IMEI).ToList();
                    break;

                case "DistributorFirstLastName ASC":
                    farmer = farmer.OrderBy(p => p.DistributorFirstLastName).ToList();
                    break;

                case "DistributorFirstLastName DESC":
                    farmer = farmer.OrderByDescending(p => p.DistributorFirstLastName).ToList();
                    break;

                case "FarmerCompanyName ASC":
                    farmer = farmer.OrderBy(p => p.FarmerCompanyName).ToList();
                    break;

                case "FarmerCompanyName DESC":
                    farmer = farmer.OrderByDescending(p => p.FarmerCompanyName).ToList();
                    break;

                case "Pono ASC":
                    farmer = farmer.OrderBy(p => p.Pono).ToList();
                    break;

                case "Pono DESC":
                    farmer = farmer.OrderByDescending(p => p.Pono).ToList();
                    break;

                default:
                    farmer = farmer.OrderBy(p => p.Name).ToList(); //Def
                    break;
            }
            return farmer;
        }

        [HttpPost]
        public JsonResult GetDevice(int CompanyId)
        {
            Service Service = new Service();
            List<RegisteredDevice> Companies = Service.GetAssignedDevice(UIHelper.UserSession.Id, CompanyId);
            return Json(Companies, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteFarmer(int Id)
        {
            Service Service = new Service();
            bool ISSuccesfull = Service.DeleteFarmer(Id);
            if (ISSuccesfull)
                return Json(new { Result = "OK" });
            else
                return Json(new { Result = "ERROR" });
        }

        [HttpPost]
        public JsonResult UpdateFarmer(Farmer Farmer)
        {
            try
            {
                Service Service = new Service();
                Service.UpdateFarmer(Farmer);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR" });
            }
        }

        public ActionResult ImportExcel()
        {
            return View();
        }

        [ActionName("Importexcel")]
        [HttpPost]
        public ActionResult Importexcel1()
        {
            List<(int, string)> ErrorList = new List<(int, string)>();
            Service Service = new Service();
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));

                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            Farmer Farmer = new Farmer();
                            for (int columnIterator = 1; columnIterator <= noOfCol; columnIterator++)
                            {
                                switch (columnIterator)
                                {
                                    case 1:
                                        Farmer.CompanyName = workSheet.Cells[rowIterator, columnIterator].Value.ToString().Trim();
                                        break;

                                    case 2:
                                        Farmer.DeviceName = "DIRMS" + workSheet.Cells[rowIterator, columnIterator].Value.ToString().Trim();
                                        break;

                                    case 3:
                                        Farmer.Name = workSheet.Cells[rowIterator, columnIterator].Value.ToString().Trim();
                                        break;

                                    case 4:
                                        Farmer.Pono = workSheet.Cells[rowIterator, columnIterator].Value.ToString().Trim();
                                        break;

                                    case 5:
                                        Farmer.District = workSheet.Cells[rowIterator, columnIterator].Value.ToString().Trim();
                                        break;

                                    case 6:
                                        Farmer.PhoneNumber = workSheet.Cells[rowIterator, columnIterator].Value.ToString().Trim();
                                        break;

                                    case 7:
                                        Farmer.StateId = int.Parse(workSheet.Cells[rowIterator, columnIterator].Value.ToString().Trim());
                                        break;

                                    case 8:
                                        Farmer.VillageId = int.Parse(workSheet.Cells[rowIterator, columnIterator].Value.ToString().Trim());
                                        break;

                                    case 9:
                                        Farmer.MadnalId = int.Parse(workSheet.Cells[rowIterator, columnIterator].Value.ToString().Trim());//MandalID
                                        break;

                                    case 10:
                                        Farmer.Department = workSheet.Cells[rowIterator, columnIterator].Value.ToString().Trim();
                                        break;

                                    case 11:
                                        Farmer.Lattitude = workSheet.Cells[rowIterator, columnIterator].Value.ToString().Trim();
                                        break;

                                    case 12:
                                        Farmer.Longitude = workSheet.Cells[rowIterator, columnIterator].Value.ToString().Trim();
                                        break;
                                }
                            }
                            bool ISSuccessFull = false;
                            try
                            {
                                ISSuccessFull = Service.InsertFarmer(Farmer.Name, Farmer.DeviceName, Farmer.Pono,
                                     Farmer.District, Farmer.PhoneNumber, Farmer.StateId, Farmer.VillageId, Farmer.MadnalId, Farmer.Department,
                                     Farmer.Lattitude, Farmer.Longitude, UIHelper.UserSession.Id, int.Parse(Farmer.CompanyName));
                            }
                            catch (Exception ex)
                            {
                                if (!ISSuccessFull)
                                {
                                    ErrorList.Add((rowIterator, "Error in Row number-"));
                                }
                            }
                        }
                    }
                }
            }
            ViewBag.ErrorList = ErrorList;
            return View();
        }
    }
}