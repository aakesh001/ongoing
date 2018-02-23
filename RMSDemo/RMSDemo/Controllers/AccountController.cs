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
    public class AccountController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string UserName, string Password)
        {
            Service service = new Service();
            Password = URLEncryptor.Encrypt(Password, String.Empty, String.Empty, String.Empty, 0, String.Empty, 0);
            RMSUser User = service.ValidateUser(UserName.ToLowerInvariant(), Password);
            if (User == null)
            {
                ViewBag.Errormessage = "UserName or Password wrong";
                return View();
            }
            else
            {
                UIHelper.UserSession = User;
                HttpCookie LoginCredentialsCookie = new HttpCookie("LoginCredsC");
                LoginCredentialsCookie.Value = UserName + "," + Password;
                LoginCredentialsCookie.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(LoginCredentialsCookie);
                if (User.RoleName.ToLowerInvariant().Equals("distributor"))
                {
                    return Redirect("/Company/Dashboard");
                }
                else if (User.RoleName.ToLowerInvariant().Equals("superadmin"))
                {
                    return Redirect("/Live/Index");
                }
                else if (User.RoleName.ToLowerInvariant().Equals("executive"))
                {
                    return Redirect("/Company/Dashboard");
                }
                return View();
            }
        }

        public ActionResult Logout()
        {
            UIHelper.UserSession = null;
            if (Request.Cookies["LoginCredsC"] != null)
            {
                Response.Cookies["LoginCredsC"].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("Index");
        }

        [LoginCheck]
        public ActionResult Register()
        {
            try
            {
                Service Service = new Service();
                List<Company> Companies = Service.GetCompany(UIHelper.UserSession.Id);
                return View(Companies);
            }
            catch (Exception ex)
            {
                return View(new List<Company>());
            }
        }

        [HttpPost]
        public ActionResult RegisterCompanyUser(string firstName, string lastName, string userName, string passWord, string email, string phoneNumber, string company, string userType)
        {
            try
            {
                Service service = new Service();
                int RoleId = GetRoleId("executive");

                //UserRegsitration
                RMSUser User = new RMSUser(firstName, lastName, userName, passWord, email, phoneNumber, RoleId.ToString());
                User.UserName = User.UserName.ToLowerInvariant();
                User.Password = URLEncryptor.Encrypt(User.Password, String.Empty, String.Empty, String.Empty, 0, String.Empty, 0);
                int UserID = service.RegsiterUser(User);
                if (UserID != 0)
                {
                    AddUserCompanyMapping(company, UserID, userType);
                }
                //Model For View
                List<Company> Companies = service.GetCompany(UIHelper.UserSession.Id);
                ViewBag.Message = "User Added Succesfully";
                return View("Register", Companies);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Something wrong happened";
                return View("Register");
            }
        }

        private int GetRoleId(string roleName)
        {
            List<Roles> Roles = RoleCache.Instance.Roles;
            return Roles.Where(w => w.RoleName.ToLowerInvariant().Equals(roleName)).Select(s => s.Id).FirstOrDefault();
        }

        private bool AddUserCompanyMapping(string compID, int userID, string typeID)
        {
            Service service = new Service();
            return service.CompanyUserMapping(int.Parse(compID), userID, int.Parse(typeID));
        }

        [HttpPost]
        public JsonResult GetUsers()
        {
            Service Service = new Service();
            List<Company> Companies = Service.GetCompany(UIHelper.UserSession.Id);
            string CompanyString = GetCompanyIdString(Companies);
            List<int> USerIDs = Service.GetUserIDbyCompanyID(CompanyString);
            string USerIDstring = GetUSerIdString(USerIDs);
            List<Roles> Roles = RoleCache.Instance.Roles;
            int RoleId = GetRoleId("executive");
            List<RMSUser> Users = Service.GetUSers(RoleId, USerIDstring);
            foreach (var user in Users)
            {
                user.Password = URLEncryptor.Decrypt(user.Password, String.Empty, String.Empty, String.Empty, 0, String.Empty, 0);
            }

            return Json(new { Result = "OK", Records = Users }, JsonRequestBehavior.AllowGet);
        }

        private string GetCompanyIdString(List<Company> companies)
        {
            var s = string.Join(",", companies.Select(p => p.Id.ToString()));
            return s;
        }

        private string GetUSerIdString(List<int> Users)
        {
            var s = string.Join(",", Users.Select(p => p.ToString()));
            return s;
        }

        [HttpPost]
        public JsonResult DeleteUser(int Id)
        {
            try
            {
                Service Service = new Service();
                bool IsSuccesfull = Service.DeleteUser(Id);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR" });
            }
        }
    }
}