using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace RMSDemo.Utilities
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
                if (user.RoleName.ToLowerInvariant().Equals("distributor"))
                {
                    if (user.LogoPath != null || user.LogoPath != "")
                    {
                        user.LogoPath = "/Uploads/" + user.LogoPath;
                    }
                }
                else if (user.RoleName.ToLowerInvariant().Equals("executive"))
                {
                    int CompanyId = Service.GetCompanyId(user.Id);
                    string FileNAame = Service.GetLogoPath(CompanyId);
                    UIHelper.UserSession = user;
                    UIHelper.UserSession.LogoPath = "/Uploads/" + FileNAame;
                }

                return user;
            }
            return null;
        }

        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }

                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    if (rows.Length > 1)
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < headers.Length; i++)
                        {
                            dr[i] = rows[i].Trim();
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }

            return dt;
        }

        public static DataTable ConvertXSLXtoDataTable(string strFilePath, string connString)
        {
            OleDbConnection oledbConn = new OleDbConnection(connString);
            DataTable dt = new DataTable();
            try
            {
                oledbConn.Open();
                using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn))
                {
                    OleDbDataAdapter oleda = new OleDbDataAdapter();
                    oleda.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    oleda.Fill(ds);

                    dt = ds.Tables[0];
                }
            }
            catch
            {
            }
            finally
            {
                oledbConn.Close();
            }

            return dt;
        }
    }
}