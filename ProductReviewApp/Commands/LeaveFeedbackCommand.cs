using System;
using System.ComponentModel.DataAnnotations;
using ProductReviewApp.Domain;

namespace ProductReviewApp.Commands
{
    public class LeaveFeedbackCommand
    {
        public Guid FeatureId { get; set; }

        [Required]
        [StringLength(64)]
        public string CreatedBy { get; set; }

        [Required]
        [StringLength(140)]
        public string Text { get; set; }
    }

    public class LeaveFeedbackCommandHandler : ICommandHandler<LeaveFeedbackCommand, Guid>
    {
        public Guid Dispatch(LeaveFeedbackCommand command)
        {
            Feedback feedback;

            using (var context = new RoundtuitDbContext())
            {
                // var feature = context.Features.Find(command.FeatureId);

                feedback = new Feedback
                {
                    CreatedBy = command.CreatedBy,
                    CreatedOn = DateTime.UtcNow,
                    FeatureId = command.FeatureId,
                    Text = command.Text
                };

                context.Feedback.Add(feedback);

                context.SaveChanges();
            }

            return feedback.Id;
        }
    }
}