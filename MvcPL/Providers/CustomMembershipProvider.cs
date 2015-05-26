using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Providers.Entities;
using System.Web.Security;
using Bll.Interface;
using Bll.Interface.Entities;

namespace MvcPL.Providers
{
    public class CustomMembershipProvider : MembershipProvider
    {
        public MembershipUser CreateUser(string userName, string password)
        {
            MembershipUser membershipUser = GetUser(userName, false);

            if (membershipUser != null)
            {
                return null;
            }

            var roleService = (IRoleService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService));
            var userService = (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));

            int roleId = roleService.GetByName("user").Id;
            var user = new BllUser()
            {
                Login = userName,
                Password = Crypto.HashPassword(password),
                RegistrationDateTime = DateTime.Now,
                RoleId = roleId
            };

            userService.Create(user);

            membershipUser = GetUser(userName, false);
            return membershipUser;

        }

        public override bool ValidateUser(string userName, string password)
        {
            var userService = (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));
            BllUser user = userService.GetByLogin(userName);
            if (user != null && Crypto.VerifyHashedPassword(user.Password, password))
            {
                return true;
            }

            return false;
        }

        public override MembershipUser GetUser(string userName, bool userIsOnline)
        {
            var userService = (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));
            BllUser user = userService.GetByLogin(userName);
            if (user == null) return null;

            return new MembershipUser("CustomMembershipProvider", user.Login, null, null, null, null,
                    false, false, user.RegistrationDateTime, DateTime.MinValue, DateTime.MinValue,
                    DateTime.MinValue, DateTime.MinValue);
        }

        #region Stabs
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer,
            bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }


        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }


        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override string ApplicationName { get; set; }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }
        #endregion
    }
}