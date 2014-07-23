using System.Web.Http;
using ProductReviewApp.Commands;

namespace ProductReviewApp.Controllers
{
    public class VoteController : ApiController
    {
        public IHttpActionResult Post(VoteCommand command)
        {
            new VoteCommandHandler().Dispatch(command);

            return Ok();
        }
    }
}