using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AliceApi.Repository.Models;

namespace AliceApi.Repository.Repositories.Membership
{
    public class MemberRepository : GenericRepository<Models.Member>, Interfaces.IMemberRepository
    {
        public MemberRepository(BaseEntities baseContext, LoggedInUserContext userContext)
            : base(baseContext, userContext)
        {
            
        }
    }

}
