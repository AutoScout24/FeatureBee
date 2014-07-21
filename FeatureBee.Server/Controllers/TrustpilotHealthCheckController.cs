namespace FeatureBee.Server.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class TrustpilotHealthCheckController : ApiController
    {
        private static bool isHealthy = true;

        [HttpGet]
        [Route("health-check")]
        public HttpResponseMessage HealthCheck()
        {
            if (isHealthy)
            {
                return new HttpResponseMessage()
                {
                    Content = new StringContent("OK")
                };
            }

            return new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
            {
                Content = new StringContent("Deployment in process")
            };
        }

        [HttpPost]
        [Route("fail-health-check")]
        public HttpResponseMessage FailHealthCheck()
        {
            if (!Request.IsLocal())
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            isHealthy = false;

            return new HttpResponseMessage()
            {
                Content = new StringContent("OK")
            };
        }
    }
}