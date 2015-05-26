using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Security;
using Bll.Interface;
using Bll.Interface.Entities;
using MvcPL.Infrastructure;

namespace MvcPL.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(name, config);
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var roleService = (IRoleService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService));
            var userService  = (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));
            
            BllUser user = userService.GetByLogin(username);
            BllRole userRole = roleService.GetUserRole(user);
            string[] roles = roleName.Split(',').Select(x => x.Trim()).ToArray();
            return roles.Any(role => string.Equals(role, userRole.Name));
        }

        public override string[] GetRolesForUser(string username)
        {
            var roleService = (IRoleService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService));
            var userService = (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));

            BllUser user = userService.GetByLogin(username);
            return new[] { roleService.GetUserRole(user).Name };
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            var roleService = (IRoleService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService));

            BllRole role = roleService.GetByName(roleName);
            return role != null;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            var roleService = (IRoleService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService));
            var userService = (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));

            BllRole role = roleService.GetByName(roleName);
            return userService.GetUsersByRole(role).Select(x => x.Login).ToArray();
        }

        public override string[] GetAllRoles()
        {
            var roleService = (IRoleService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService));
            return roleService.GetAllRoles().Select(x => x.Name).ToArray();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}