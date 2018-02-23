using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public sealed class RoleCache
    {
        private List<Roles> RolesList;

        private RoleCache()
        {
        }

        public List<Roles> Roles
        {
            get
            {
                if (RolesList == null)
                {
                    GetRolesfromDB();
                }
                return RolesList;
            }
        }

        public static RoleCache Instance
        {
            get { return Nested.instance; }
        }

        private void GetRolesfromDB()
        {
            Service service = new Service();
            RolesList = service.GetRoles();
        }

        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly RoleCache instance = new RoleCache();
        }
    }
}