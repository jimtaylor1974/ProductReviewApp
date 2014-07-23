using System.Linq;
using ProductReviewApp.Domain;

namespace ProductReviewApp.Queries
{
    public class FeatureRequestsQuery
    {
        public dynamic Execute(string orderBy)
        {
            using (var context = new RoundtuitDbContext())
            {
                var query = context.Features
                    .Where(f => f.IsRequest);

                switch (orderBy)
                {
                    case "comments":
                        query = query.OrderByDescending(f => f.Feedback.Count()).ThenBy(f => f.CreatedOn);
                        break;
                    case "votes":
                        query = query.OrderByDescending(f => f.Votes).ThenBy(f => f.CreatedOn);
                        break;
                    default: 
                        query = query.OrderByDescending(f => f.CreatedOn);
                        break;
                }

                return query
                    .Select(f => new
                    {
                        f.Id,
                        f.Name,
                        f.Description,
                        FeedbackCount = f.Feedback.Count(),
                        f.Votes,
                        f.CreatedBy,
                        f.CreatedOn
                    })
                    .ToArray();
            }
        }
    }
}