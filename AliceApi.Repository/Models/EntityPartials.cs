using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AliceApi.Repository.Interfaces;

namespace AliceApi.Repository.Models
{
    
    class EntityPartials
    {
        //public partial class Project : IAuditable, IsDeleted
        //{
        //    public string GetCapacityTypeNameFromId(UnitOfWork uow)
        //    {
        //        var capacityType =
        //            uow.CapacityTypeRepository.Get(c => c.CapacityTypeId == this.CapacityTypeId).FirstOrDefault();
        //        return capacityType != null ? capacityType.CapacityTypeName : String.Empty;
        //    }
        //}
    }

    /// <summary>
    /// This will automatially add the logged in user context and datestamps for any changes
    /// </summary>
    public partial class MemberProfile : IAuditable
    {
    }


}
