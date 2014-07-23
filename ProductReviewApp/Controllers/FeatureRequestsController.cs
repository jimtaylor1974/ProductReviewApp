using System.Web.Http;
using ProductReviewApp.Commands;
using ProductReviewApp.Queries;

namespace ProductReviewApp.Controllers
{
    public class FeatureRequestsController : ApiController
    {
        public IHttpActionResult Get(string orderBy)
        {
            var result = new FeatureRequestsQuery().Execute(orderBy);

            return Ok(result);
        }

        public IHttpActionResult Post(RequestFeatureCommand command)
        {
            var id = new RequestFeatureCommandHandler().Dispatch(command);

            return Ok(new { id });
        }
    }
}