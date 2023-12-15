using ContactAppEF.Data;
using ContactAppEF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ContactAppEF
{
    public class WebRoleProvider : RoleProvider
    {
        //private readonly MyContext _myContext = new MyContext();
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

        public override string[] GetRolesForUser(string username)
        {
            using(MyContext myContext = new MyContext())
            {
                var userRoles = myContext.Users.Where(u => u.Name == username).Include(u => u.Role).FirstOrDefault().Role.Name;
                string[] userArray = new string[1];
                userArray[0] = userRoles;
                return userArray;
            }
            //var result = (from user in _myContext.Users
            //              join role in _myContext.Roles on user.RoleId equals role.Id where user.Name==username select role.Name).ToArray();
            //return result;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
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