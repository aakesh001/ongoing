using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMSDemo.Utilities
{
    public class LoginCheck : ActionFilterAttribute
    {
        public string RoleName { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            DateTime date1 = new DateTime(2018, 3, 15);
            if (DateTime.Now < date1)
            {
                HttpContext ctx = HttpContext.Current;
                if (HttpContext.Current.Session["LogInUserDetails"] == null)
                {
                    filterContext.Result = new RedirectResult("~/Account/Index");
                    return;
                }
                base.OnActionExecuting(filterContext);
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Account/Index");
                return;
            }
        }
    }
}