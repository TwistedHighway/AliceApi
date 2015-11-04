using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliceApi.Repository.Interfaces
{
    public interface IAuditable
    {
        System.DateTime CreatedDate { get; set; }
        Nullable<System.DateTime> UpdatedDate { get; set; }

        //System.Guid CreatedBy { get; set; }
        //Nullable<System.Guid> UpdatedBy { get; set; }
        System.String CreatedBy { get; set; }
        System.String UpdatedBy { get; set; }
    }
}
