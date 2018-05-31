using RMSDemo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMSDemo.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (UIHelper.UserSession != null)
            {
                if (UIHelper.UserSession.RoleName.ToLowerInvariant().Equals("distributor"))
                {
                    return Redirect("/StateVillage/Index");
                }
                else
                    if (UIHelper.UserSession.RoleName.ToLowerInvariant().Equals("superadmin"))
                {
                    return Redirect("/Device");
                }
            }
            else
            {
                return Redirect("/Account");
            }
            return Redirect("/Account");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}