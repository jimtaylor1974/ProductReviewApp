using System.Web.Http;
using ProductReviewApp.Queries;

namespace ProductReviewApp.Controllers
{
    public class FeatureController : ApiController
    {
        public IHttpActionResult Get()
        {
            var result = new FeaturesQuery().Execute();

            return Ok(result);
        }
    }
}
