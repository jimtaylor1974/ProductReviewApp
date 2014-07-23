using System.Linq;
using ProductReviewApp.Domain;

namespace ProductReviewApp.Queries
{
    public class FeaturesQuery
    {
        public dynamic Execute()
        {
            using (var context = new RoundtuitDbContext())
            {
                return context.Features
                    .Where(f => !f.IsRequest)
                    .OrderBy(f => f.DisplayOrder)
                    .ThenBy(f => f.CreatedOn)
                    .Select(f => new
                    {
                        f.Id,
                        f.Image,
                        f.Name,
                        f.Description,
                        FeedbackCount = f.Feedback.Count()
                    })
                    .ToArray();
            }
        }
    }
}