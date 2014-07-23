using System;
using ProductReviewApp.Domain;

namespace ProductReviewApp.Commands
{
    public class VoteCommand
    {
        public Guid FeatureId { get; set; }
        public int Value { get; set; }
    }

    public class VoteCommandHandler : ICommandHandler<VoteCommand, bool>
    {
        public bool Dispatch(VoteCommand command)
        {
            using (var context = new RoundtuitDbContext())
            {
                var feature = context.Features.Find(command.FeatureId);

                feature.Votes += command.Value;

                context.SaveChanges();
            }

            return true;
        }
    }
}