https://github.com/troygoode/MembershipStarterKit

2 3 4 5 6 7 8 9 10 11 12 13 14 	

namespace MvcMembership
{
public class AspNetMembershipProviderUserServiceFactory : IUserServiceFactory
{
#region IUserServiceFactory Members

public IUserService Make()
{
return new AspNetMembershipProviderWrapper();
}

#endregion
}
}


using System;
using System.Collections.Generic;
using System.Web.Security;
using MvcMembership.Settings;
using PagedList;

namespace MvcMembership
{
public class AspNetMembershipProviderWrapper : IUserService, IPasswordService
{
private readonly MembershipProvider _membershipProvider;

public AspNetMembershipProviderWrapper()
{
_membershipProvider = Membership.Provider;
}

public AspNetMembershipProviderWrapper(MembershipProvider membershipProvider)
{
_membershipProvider = membershipProvider;
}

public AspNetMembershipProviderSettingsWrapper Settings
{
get{ return new AspNetMembershipProviderSettingsWrapper(_membershipProvider); }
}

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
if(!user.ChangePassword(resetPassword, newPassword))
throw new MembershipPasswordException("Could not change password.");
}

public void ChangePassword(MembershipUser user, string oldPassword, string newPassword)
{
if (!user.ChangePassword(oldPassword, newPassword))
throw new MembershipPasswordException("Could not change password.");
}

#endregion

#region IUserService Members

public IPagedList<MembershipUser> FindAll(int pageNumber, int pageSize)
{
// get one page of users
int totalUserCount;
            var usersCollection = _membershipProvider.GetAllUsers(pageNumber - 1, pageSize, out totalUserCount);

// convert from MembershipUserColletion to PagedList<MembershipUser> and return
var converter = new EnumerableToEnumerableTConverter<MembershipUserCollection, MembershipUser>();
var usersList = converter.ConvertTo<IEnumerable<MembershipUser>>(usersCollection);
            var usersPagedList = new StaticPagedList<MembershipUser>(usersList, pageNumber, pageSize, totalUserCount);
return usersPagedList;
}

        public IPagedList<MembershipUser> FindByEmail(string emailAddressToMatch, int pageNumber, int pageSize)
{
// get one page of users
int totalUserCount;
            var usersCollection = _membershipProvider.FindUsersByEmail(emailAddressToMatch, pageNumber - 1, pageSize, out totalUserCount);

// convert from MembershipUserColletion to PagedList<MembershipUser> and return
var converter = new EnumerableToEnumerableTConverter<MembershipUserCollection, MembershipUser>();
var usersList = converter.ConvertTo<IEnumerable<MembershipUser>>(usersCollection);
            var usersPagedList = new StaticPagedList<MembershipUser>(usersList, pageNumber, pageSize, totalUserCount);
return usersPagedList;
}

        public IPagedList<MembershipUser> FindByUserName(string userNameToMatch, int pageNumber, int pageSize)
{
// get one page of users
int totalUserCount;
            var usersCollection = _membershipProvider.FindUsersByName(userNameToMatch, pageNumber - 1, pageSize, out totalUserCount);

// convert from MembershipUserColletion to PagedList<MembershipUser> and return
var converter = new EnumerableToEnumerableTConverter<MembershipUserCollection, MembershipUser>();
var usersList = converter.ConvertTo<IEnumerable<MembershipUser>>(usersCollection);
            var usersPagedList = new StaticPagedList<MembershipUser>(usersList, pageNumber, pageSize, totalUserCount);
return usersPagedList;
}

public MembershipUser Get(string userName)
{
return _membershipProvider.GetUser(userName, false);
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
if(status != MembershipCreateStatus.Success)
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


using System;
using System.Collections.Generic;
using System.Web.Security;

namespace MvcMembership
{
public class AspNetRoleProviderWrapper : IRolesService
{
private readonly RoleProvider _roleProvider;

public AspNetRoleProviderWrapper()
{
if (Roles.Enabled)
_roleProvider = Roles.Provider;
}

public AspNetRoleProviderWrapper(RoleProvider roleProvider)
{
_roleProvider = roleProvider;
}

#region IRolesService Members

public bool Enabled
{
get { return _roleProvider != null; }
}

public IEnumerable<string> FindAll()
{
return _roleProvider.GetAllRoles();
}

public IEnumerable<string> FindByUser(MembershipUser user)
{
return _roleProvider.GetRolesForUser(user.UserName);
}

public IEnumerable<string> FindByUserName(string userName)
{
return _roleProvider.GetRolesForUser(userName);
}

public IEnumerable<string> FindUserNamesByRole(string roleName)
{
return _roleProvider.GetUsersInRole(roleName);
}

public void AddToRole(MembershipUser user, string roleName)
{
_roleProvider.AddUsersToRoles(new[] {user.UserName}, new[] {roleName});
}

public void RemoveFromRole(MembershipUser user, string roleName)
{
_roleProvider.RemoveUsersFromRoles(new[] {user.UserName}, new[] {roleName});
}

public void RemoveFromAllRoles(MembershipUser user)
{
var roles = FindByUser(user);
foreach(var role in roles)
RemoveFromRole(user, role);
}

public bool IsInRole(MembershipUser user, string roleName)
{
return _roleProvider.IsUserInRole(user.UserName, roleName);
}

public void Create(string roleName)
{
_roleProvider.CreateRole(roleName);
}

public void Delete(string roleName)
{
_roleProvider.DeleteRole(roleName, false);
}

#endregion
}
}

using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMembership
{
public class AuthorizeUnlessOnlyUserAttribute : AuthorizeAttribute
{
private static bool _hasAtLeastTwoUsers;
private static bool _hasAtLeastOneRole;
private readonly IRolesService _rolesService;
private readonly IUserService _userService;
private string[] _rolesSplit;
private string[] _usersSplit;

public AuthorizeUnlessOnlyUserAttribute() : this(new AspNetMembershipProviderWrapper(), new AspNetRoleProviderWrapper())
{
}

public AuthorizeUnlessOnlyUserAttribute(IUserService userService, IRolesService rolesService)
{
_userService = userService;
_rolesService = rolesService;
}

protected override bool AuthorizeCore(HttpContextBase httpContext)
{
if (httpContext == null)
throw new ArgumentNullException("httpContext");

//never authorize someone who isn't logged in
var user = httpContext.User;
if (!user.Identity.IsAuthenticated)
return false;

//allow anyone access if there are less than two users, otherwise
// - use normal logic (and cache this finding in a static variable)
if (_hasAtLeastTwoUsers || _userService.TotalUsers > 1)
_hasAtLeastTwoUsers = true;
else
return true;

//MSFT wasn't kind enough to make these protected, so on the first request go ahead and recacl these
if (_usersSplit == null)
_usersSplit = SplitString(Users);
if (_rolesSplit == null)
_rolesSplit = SplitString(Roles);

//same user check as MSFT - not sure that anyone actually uses this feature though...
if (_usersSplit.Any() && !_usersSplit.Contains(user.Identity.Name, StringComparer.OrdinalIgnoreCase))
return false;

//added the check for whether the role service is enabled or not. if it isn't, don't validate on that
if (_hasAtLeastOneRole || _rolesService.FindAll().Any())
_hasAtLeastOneRole = true;
if (!_rolesService.Enabled || !_rolesSplit.Any() || !_hasAtLeastOneRole)
return true;

//is this user in one of the necessary roles?
return _rolesSplit.Any(user.IsInRole);
}

private static string[] SplitString(string original)
{
if (String.IsNullOrEmpty(original))
return new string[0];

var split = from piece in original.Split(',')
let trimmed = piece.Trim()
where !String.IsNullOrEmpty(trimmed)
select trimmed;
return split.ToArray();
}
}
}



using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace MvcMembership
{
public class EnumerableToEnumerableTConverter<TCollectionType, TItemType> : TypeConverter where TCollectionType : IEnumerable
{
public override bool CanConvertTo( ITypeDescriptorContext context, Type destinationType )
{
return destinationType.IsAssignableFrom(typeof(IEnumerable<TItemType>))
? true
: base.CanConvertTo( context, destinationType );
}

public T ConvertTo<T>( object value )
{
return (T)ConvertTo( value, typeof(T) );
}

public override object ConvertTo( ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType )
{
var items = (TCollectionType)value;
var destination = new List<TItemType>();
foreach( var item in items )
destination.Add((TItemType)item);
return destination;
}
}
}

using System.Web.Security;

namespace MvcMembership
{
public interface IPasswordService
{
void Unlock(MembershipUser user);
string ResetPassword(MembershipUser user);
string ResetPassword(MembershipUser user, string passwordAnswer);
void ChangePassword(MembershipUser user, string newPassword);
void ChangePassword(MembershipUser user, string oldPassword, string newPassword);
}
}

using System.Collections.Generic;
using System.Web.Security;

namespace MvcMembership
{
public interface IRolesService
{
bool Enabled { get; }
IEnumerable<string> FindAll();
IEnumerable<string> FindByUser(MembershipUser user);
IEnumerable<string> FindByUserName(string userName);
IEnumerable<string> FindUserNamesByRole(string roleName);
void AddToRole(MembershipUser user, string roleName);
void RemoveFromRole(MembershipUser user, string roleName);
void RemoveFromAllRoles(MembershipUser user);
bool IsInRole(MembershipUser user, string roleName);
void Create(string roleName);
void Delete(string roleName);
}
}



using System.Net.Mail;

namespace MvcMembership
{
public interface ISmtpClient
{
void Send(MailMessage mailMessage);
}
}

using System.Web.Security;
using PagedList;

namespace MvcMembership
{
public interface IUserService
{
int TotalUsers { get; }
int UsersOnline{ get; }
        IPagedList<MembershipUser> FindAll(int pageNumber, int pageSize);
        IPagedList<MembershipUser> FindByEmail(string emailAddressToMatch, int pageNumber, int pageSize);
        IPagedList<MembershipUser> FindByUserName(string userNameToMatch, int pageNumber, int pageSize);
MembershipUser Get(string userName);
MembershipUser Get(object providerUserKey);
MembershipUser Create(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved);
MembershipUser Create(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey);
void Update(MembershipUser user);
void Delete(MembershipUser user);
void Delete(MembershipUser user, bool deleteAllRelatedData);
MembershipUser Touch(MembershipUser user);
MembershipUser Touch(string userName);
MembershipUser Touch(object providerUserKey);
}
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcMembership
{
public interface IUserServiceFactory
{
IUserService Make();
}
}



using System.Net.Mail;

namespace MvcMembership
{
public class SmtpClientProxy : ISmtpClient
{
private readonly SmtpClient _smtpClient;

public SmtpClientProxy()
{
_smtpClient = new SmtpClient();
}

public SmtpClientProxy(SmtpClient smtpClient)
{
_smtpClient = smtpClient;
}

#region ISmtpClient Members

public void Send(MailMessage mailMessage)
{
_smtpClient.Send(mailMessage);
}

#endregion
}
}

using System.Web.Mvc;

namespace MvcMembership
{
public class TouchUserOnEachVisitFilter : ActionFilterAttribute
{
private readonly IUserServiceFactory _userServiceFactory;
private IUserService _userService;

private IUserService UserService
{
get { return _userService ?? (_userService = _userServiceFactory.Make()); }
}

public TouchUserOnEachVisitFilter() : this(new AspNetMembershipProviderUserServiceFactory())
{
}

public TouchUserOnEachVisitFilter(IUserServiceFactory userServiceFactory)
{
_userServiceFactory = userServiceFactory;
}

public override void OnActionExecuting(ActionExecutingContext filterContext)
{
var user = filterContext.RequestContext.HttpContext.User;
if (user.Identity.IsAuthenticated)
UserService.Touch(user.Identity.Name);
}
}
}




