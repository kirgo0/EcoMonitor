using System.Net;

namespace EcoMonitor.Model.APIResponses
{
    public class MultipleAPIResponse
    {
        public HttpStatusCode StatusCode = HttpStatusCode.MultiStatus;
        public List<APIResponse> apiResponses { get; set; } = new List<APIResponse>();
    }
}
