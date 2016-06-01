using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AliceApi.Repository.Models;

namespace AliceApi.Repository.Repositories.Membership
{
    /// <summary>
    /// Member Profile Repository manages the profile metadata for a given application user
    /// </summary>
   public class MemberProfileRepository : GenericRepository<Models.MemberProfile>, Interfaces.IMemberProfileRepository
    {
        public MemberProfileRepository(BaseEntities baseContext, LoggedInUserContext userContext) : base(baseContext, userContext)
        {
            // Do Stuff... 
        }
    }

}
