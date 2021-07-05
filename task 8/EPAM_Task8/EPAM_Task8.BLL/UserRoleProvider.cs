using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using EPAM_Task8.SqlDAL;
using UpdatedEntity;

namespace EPAM_Task8.BLL
{
    public class UserRoleProvider : RoleProvider
    {
        public override bool IsUserInRole(string username, string roleName)
        {
            User user = MySqlDAL.GetUser(username);

            if (user.IsAdmin && roleName == "admin" || user.IsAdmin && roleName == "user")
            {
                return true;
            }

            if (!user.IsAdmin && roleName == "user")
            {
                return true;
            }

            return false;
        }

        public override string[] GetRolesForUser(string username)
        {
            User user = MySqlDAL.GetUser(username);

            if (user.IsAdmin)
            {
                return new string[] { "admin", "user" };
            }

            return new string[] { "user" };
        }





        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
