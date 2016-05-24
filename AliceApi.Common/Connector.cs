namespace AliceApi.Common
{
    public class Connector
    {
        public string TheConnector = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
    }

    

}
