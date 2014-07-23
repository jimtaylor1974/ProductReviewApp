using System;
using System.Linq;
using ProductReviewApp.Domain;

namespace ProductReviewApp.Queries
{
    public class FeedbackQuery
    {
        public dynamic Execute(Guid featureId)
        {
            using (var context = new RoundtuitDbContext())
            {
                return context.Feedback
                    .Where(f => f.FeatureId == featureId)
                    .OrderBy(f => f.CreatedOn)
                    .Select(f => new
                    {
                        f.Id,
                        f.Text,
                        f.CreatedBy,
                        f.CreatedOn
                    })
                    .ToArray();
            }
        }
    }
}