using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliceApi.Repository.Interfaces
{
    /// <summary>
    ///  This is a 'soft delete' and useful when you want to hide records vs. actually deleting them. This will automatially omit 'deleted' records. 
    /// </summary>
    interface IsDeleted
    {
        bool IsDeleted { get; set; }
    }
}


