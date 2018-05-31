using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;

namespace PDFService.Utilities
{
    public static class UIHelper
    {
        public static RMSUser UserSession
        {
            get
            {
                if ((HttpContext.Current.Session["LogInUserDetails"]) != null)
                {
                    return (RMSUser)(HttpContext.Current.Session["LogInUserDetails"]);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Session["LogInUserDetails"] = value;
            }
        }

        public static RMSUser SetUserLogin(string userName, string passWord)
        {
            var Service = new Service();
            var user = Service.ValidateUser(userName, passWord);
            if (user != null)
            {
                return user;
            }
            return null;
        }
    }
}