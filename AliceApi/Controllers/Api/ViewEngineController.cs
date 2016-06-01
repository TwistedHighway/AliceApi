using System.Collections.Generic;
using System.Web.Http;

namespace AliceApi.Controllers.Api
{
    public class ViewEngineController : ApiController
    {

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
