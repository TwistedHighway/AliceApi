using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Optimization;
using AliceApi;
using AliceApi.Models;
using AliceApi.Repository;
using AliceApi.ViewModels.Membership;
using Microsoft.Owin.Security;

namespace AliceApi.Logic.Membership
{
    public class MembershipLogic
    {

        private readonly Repository.UnitOfWork _uow;
        private readonly ApplicationUser _user;
        public MembershipLogic(ApplicationUser user)
        {
            _user = user;
            var userContext = new LoggedInUserContext()
            {
                UserName = _user.UserName,
                UserId = Guid.Parse(_user.Id)
            };
            _uow = new UnitOfWork(userContext);
        }


        #region Public Properties
           
        public IList<MemberViewModel.Member> AllMembers
        {
            get { return new List<MemberViewModel.Member>(); }
        }
        

        public bool ProfileExists
        {
            get { return _uow.MemberProfileRepository.Get(id => id.AspNetUserId == _user.Id).Any(); }
        }

        #endregion




        public MemberViewModel.MemberProfile CreateProfile(MemberViewModel.MemberProfile model)
        {
            try
            {
                var lang = Thread.CurrentThread.CurrentCulture;
                var tiz = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);

                var profile = new Repository.Models.MemberProfile()
                {
                    MemberProfileId = Guid.NewGuid(),
                    AspNetUserId = model.AspNetUserId,
                    LocalUserName = model.LocalUserName,
                    PublicUserName = model.LocalUserName,
                    PrimaryEmail = model.PrimaryEmail,
                    ProfileDescription = "",
                    ProfileTimeZone = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).ToString(),
                    ProfileLanguage = Thread.CurrentThread.CurrentCulture.Name,
                    LastLoginDate = DateTime.Now,
                    //CreateDate = DateTime.Now,
                    //UpdatedDate = DateTime.Now,
                    //CreatedBy = "FOO"
                };

                _uow.MemberProfileRepository.Insert(profile);

                var member = new Repository.Models.Member()
                {
                    MemberId = Guid.NewGuid(),
                    MemberProfileId = profile.MemberProfileId,
                    FirstName = model.FirstName,
                    MiddleName = "",
                    LastName = model.LastName,
                    PreferredName = "",
                    MemberType = 1,
                    BirthDate = DateTime.Now.AddYears(-21),
                    FacebookHomeUrl = "",
                    MemberEmail = model.PrimaryEmail
                };

                _uow.MemberRepository.Insert(member);

                // Save Both Records
                _uow.Save();

                var memberProfile = new MemberViewModel.MemberProfile()
                {
                    MemberId = member.MemberId,
                    MemberProfileId = member.MemberProfileId,
                    LastName = member.LastName,
                    FirstName = member.FirstName,
                    PrimaryEmail = profile.PrimaryEmail,
                    LocalUserName = profile.LocalUserName,
                    
                };
                model.ResponseMessage = "OK";
                model.Members = new List<MemberViewModel.Member> { memberProfile };
            }
            catch (Exception ex)
            {
                model.ErrorCode = "ERROR";
                model.ResponseMessage = ex.Message;
            }
            return model;
        }


        public MemberViewModel.MemberProfile GetMemberProfileByUserId(string userId)
        {
            var profile = new MemberViewModel.MemberProfile();
            var member = new MemberViewModel.Member();
            try
            {
                using (_uow)
                {
                    var p = _uow.MemberProfileRepository.Get().First(w => w.AspNetUserId == userId);
                    profile.MemberProfileId = p.MemberProfileId;
                    profile.AspNetUserId = p.AspNetUserId;
                    profile.LocalUserName = p.LocalUserName;
                    profile.PrimaryEmail = p.PrimaryEmail;
                    profile.PublicUserName = p.PublicUserName;
                    profile.DateUpdated = p.UpdatedDate;

                    var m = p.Members.First(w => w.MemberType == 1);
                    member.MemberId = m.MemberId;
                    member.FirstName = m.FirstName;
                    member.LastName = m.LastName;
                    member.DateOfBirth = m.BirthDate;
                    member.MemberEmail = m.MemberEmail;
                    
                    profile.PrimaryMember = member;
                    profile.Members = new List<MemberViewModel.Member> { member }; 
                    profile.ErrorCode = "OK";
                    profile.ResponseMessage = "OK";
                }
            }
            catch (Exception ex)
            {
                profile.ErrorCode = "ERROR";
                profile.ResponseMessage = ex.Message;
            }
            return profile;
        }


        

        public MemberViewModel.MemberProfile UpdatePrimaryMemberProfile(MemberViewModel.MemberProfile model)
        {
            try
            {

                var profile = _uow.MemberProfileRepository.GetById(model.MemberProfileId);
                profile.LocalUserName = model.LocalUserName;
                profile.PrimaryEmail = model.PrimaryEmail;
                profile.PublicUserName = model.PublicUserName;
                profile.LastLoginDate = DateTime.Now;
                _uow.MemberProfileRepository.Update(profile);

                var primaryMember = model.PrimaryMember;

                var member = _uow.MemberRepository.GetById(primaryMember.MemberId);
                member.FirstName = primaryMember.FirstName;
                member.LastName = primaryMember.LastName;
                member.MemberEmail = primaryMember.MemberEmail;
                _uow.MemberRepository.Update(member);
                    
                _uow.Save();

                model.DateUpdated = profile.UpdatedDate;
                model.ErrorCode = "OK";
                model.ResponseMessage = "OK";
   
            }
            catch (Exception ex)
            {
                model.ErrorCode = "ERROR";
                model.ResponseMessage = ex.Message;
            }
            return model;
        }

        

        public MemberViewModel.Member CreateMember(MemberViewModel.Member model)
        {
            return new MemberViewModel.Member();
        }

   


        public MemberViewModel.Member DeleteMember(int id)
        {
            // we want to return an error message and any related values if any so we want the whole ViewModel here
            return new MemberViewModel.Member();
        }

    }
}
