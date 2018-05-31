using Entities;
using RMSDemo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMSDemo.Controllers
{
    public class BaseController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            SetUserLoginFromCookies(requestContext);

            base.Initialize(requestContext);
        }

        private void SetUserLoginFromCookies(System.Web.Routing.RequestContext requestContext)
        {
            if (requestContext.HttpContext.Request.Cookies["LoginCredsC"] != null && requestContext.HttpContext.Session["LogInUserDetails"] == null)
            {
                string[] IsangoUser = requestContext.HttpContext.Request.Cookies["LoginCredsC"].Value.ToString().Split(',');
                RMSUser User = UIHelper.SetUserLogin(IsangoUser[0], IsangoUser[1]);
                if (User != null)
                {
                    requestContext.HttpContext.Session["LogInUserDetails"] = User;
                }
            }
        }
    }
}