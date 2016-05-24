using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliceApi.Repository.Interfaces
{
    public interface ICreateAuditable
    {
        System.DateTime DateCreated { get; set; }
        System.Guid CreatedBy { get; set; }
        DateTime CreateDate { get; set; }
    }
}
