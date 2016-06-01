using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliceApi.Repository.Enums
{
    /// <summary>
    ///  The type of member this for User Meta Data
    ///  </summary>
    public enum MemberType : int
    {
        [Description("Primary")]
         Primary = 1,
        [Description("Family")]
         Family = 2,
        [Description("Friend")]
         Friend = 3,
        [Description("CoWorker")]
         CoWorker = 4,
        [Description("Other")]
         Other = 5
    }
}
