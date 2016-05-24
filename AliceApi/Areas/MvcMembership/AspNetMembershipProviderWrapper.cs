using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using AliceApi;
using AliceApi.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Authorization;
//using Microsoft.AspNet.Mvc;
//using Microsoft.Extensions.Logging;
using MvcMembership.Settings;
using PagedList;
using WebMatrix.WebData;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Logging;
namespace MvcMembership
{
    public class AspNetMembershipProviderWrapper //: IUserService, IPasswordService
    {
        private readonly MembershipProvider _membershipProvider;
        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly IEmailSender _emailSender;
        //private readonly ISmsSender _smsSender;

        private ApplicationSignInManager _signInManager;
        //private ApplicationUserManager _userManager;

        //private readonly ILogger _logger;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get { return _roleManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>(); }
            private set { _roleManager = value; }
        }



        //public ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? HttpContext.GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}

        public AspNetMembershipProviderWrapper()
        {
            //_membershipProvider = Membership.Provider;
        }

        public AspNetMembershipProviderWrapper(MembershipProvider membershipProvider)
        {
            //_membershipProvider = membershipProvider;
        }

        //public AspNetMembershipProviderSettingsWrapper Settings
        //{
        //    get { return new AspNetMembershipProviderSettingsWrapper(_membershipProvider); }
        //}

        #region IPasswordService Members

        public void Unlock(MembershipUser user)
        {
            user.UnlockUser();
        }

        public string ResetPassword(MembershipUser user)
        {
            return user.ResetPassword();
        }

        public string ResetPassword(MembershipUser user, string passwordAnswer)
        {
            return user.ResetPassword(passwordAnswer);
        }

        public void ChangePassword(MembershipUser user, string newPassword)
        {
            var resetPassword = user.ResetPassword();
            if (!user.ChangePassword(resetPassword, newPassword))
                throw new MembershipPasswordException("Could not change password.");
        }

        public void ChangePassword(MembershipUser user, string oldPassword, string newPassword)
        {
            if (!user.ChangePassword(oldPassword, newPassword))
                throw new MembershipPasswordException("Could not change password.");
        }

        #endregion

        #region IUserService Members

        public IPagedList<ApplicationUser> FindAll(int pageNumber, int pageSize)
        {
            // get one page of users
            int totalUserCount = 0;
            var usersCollection = SignInManager.UserManager.Users;
            
            //var usersCollection = _membershipProvider.GetAllUsers(pageNumber - 1, pageSize, out totalUserCount);
            // convert from MembershipUserColletion to PagedList<MembershipUser> and return
            //var converter = new EnumerableToEnumerableTConverter<MembershipUserCollection, MembershipUser>();
            //var usersList = converter.ConvertTo<IEnumerable<MembershipUser>>(usersCollection);

            var usersPagedList = new StaticPagedList<ApplicationUser>(usersCollection, pageNumber, pageSize, totalUserCount);
            return usersPagedList;
        }

        public IPagedList<ApplicationUser> FindByEmail(string emailAddressToMatch, int pageNumber, int pageSize)
        {
            //var usersCollection = _membershipProvider.FindUsersByEmail(emailAddressToMatch, pageNumber - 1, pageSize, out totalUserCount);
            var usersCollection = SignInManager.UserManager.Users.Where(w => w.Email.Contains(emailAddressToMatch));
            int totalUserCount = usersCollection.Count();
            // convert from MembershipUserColletion to PagedList<MembershipUser> and return
            //var converter = new EnumerableToEnumerableTConverter<MembershipUserCollection, MembershipUser>();
            //var usersList = converter.ConvertTo<IEnumerable<MembershipUser>>(usersCollection);
            var usersPagedList = new StaticPagedList<ApplicationUser>(usersCollection, pageNumber, pageSize, totalUserCount);
            return usersPagedList;
        }

        public IPagedList<ApplicationUser> FindByUserName(string userNameToMatch, int pageNumber, int pageSize)
        {
            //// get one page of users
            //int totalUserCount;
            //var usersCollection = _membershipProvider.FindUsersByName(userNameToMatch, pageNumber - 1, pageSize, out totalUserCount);

            var usersCollection = SignInManager.UserManager.Users.Where(w => w.UserName.Contains(userNameToMatch));
            int totalUserCount = usersCollection.Count();

            // convert from MembershipUserColletion to PagedList<MembershipUser> and return
            //var converter = new EnumerableToEnumerableTConverter<MembershipUserCollection, MembershipUser>();
            //var usersList = converter.ConvertTo<IEnumerable<MembershipUser>>(usersCollection);
            var usersPagedList = new StaticPagedList<ApplicationUser>(usersCollection, pageNumber, pageSize, totalUserCount);
            return usersPagedList;
        }

        public IList<ApplicationUser> FindUsersByRoleId(string id)
        {
            //var userId = User.Identity.GetUserId();
            //var user = UserManager.FindById(User.Identity.GetUserId());

            //// get one page of users
            //int totalUserCount;
            //var usersCollection = _membershipProvider.FindUsersByName(userNameToMatch, pageNumber - 1, pageSize, out totalUserCount);

            var myList = new List<ApplicationUser>();
            var role = RoleManager.FindByIdAsync(id).Result;
            
            foreach (var roleUser in role.Users)
            {
                myList.Add(SignInManager.UserManager.FindByIdAsync(roleUser.UserId).Result);
            }
            return myList;
        }

        //public MembershipUser Get(string userName)
        //{
        //    return _membershipProvider.GetUser(userName, false);
        //}

        public ApplicationUser Get(string userName)
        {
            return SignInManager.UserManager.Users.FirstOrDefault(w => w.UserName == userName);
        }

        public MembershipUser Get(object providerUserKey)
        {
            return _membershipProvider.GetUser(providerUserKey, false);
        }

        public MembershipUser Create(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved)
        {
            return Create(username, password, email, passwordQuestion, passwordAnswer, isApproved, Guid.NewGuid());
        }

        public MembershipUser Create(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey)
        {
            MembershipCreateStatus status;
            var user = _membershipProvider.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, out status);
            if (status != MembershipCreateStatus.Success)
                throw new MembershipCreateUserException(status);
            return user;
        }

        public void Update(MembershipUser user)
        {
            _membershipProvider.UpdateUser(user);
        }

        public void Delete(MembershipUser user)
        {
            _membershipProvider.DeleteUser(user.UserName, false);
        }

        public void Delete(MembershipUser user, bool deleteAllRelatedData)
        {
            _membershipProvider.DeleteUser(user.UserName, deleteAllRelatedData);
        }

        public MembershipUser Touch(MembershipUser user)
        {
            return _membershipProvider.GetUser(user.UserName, true);
        }

        public MembershipUser Touch(string userName)
        {
            return _membershipProvider.GetUser(userName, true);
        }

        public MembershipUser Touch(object providerUserKey)
        {
            return _membershipProvider.GetUser(providerUserKey, true);
        }

        public int TotalUsers
        {
            get
            {
                int totalUsers;
                _membershipProvider.GetAllUsers(1, 1, out totalUsers);
                return totalUsers;
            }
        }

        public int UsersOnline
        {
            get
            {
                return _membershipProvider.GetNumberOfUsersOnline();
            }
        }

        #endregion
    }
}