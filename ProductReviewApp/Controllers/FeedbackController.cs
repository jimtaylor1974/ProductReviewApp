using System;
using System.Web.Http;
using ProductReviewApp.Commands;
using ProductReviewApp.Queries;

namespace ProductReviewApp.Controllers
{
    public class FeedbackController : ApiController
    {
        public IHttpActionResult Get(Guid featureId)
        {
            var result = new FeedbackQuery().Execute(featureId);

            return Ok(result);
        }

        public IHttpActionResult Post(LeaveFeedbackCommand command)
        {
            var id = new LeaveFeedbackCommandHandler().Dispatch(command);

            return Ok(new { id });
        }
    }
}
