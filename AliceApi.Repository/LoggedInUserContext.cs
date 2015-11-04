using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliceApi.Repository
{
    public class LoggedInUserContext
    {
           public Guid UserId { get; set; }
        public string UserName { get; set; }        
    
    }
}
