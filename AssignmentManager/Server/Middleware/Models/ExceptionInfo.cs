using Newtonsoft.Json;

namespace AssignmentManager.Server.Middleware
{
    public class ExceptionInfo
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
 
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}