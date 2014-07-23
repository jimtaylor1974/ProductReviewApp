using System;
using System.ComponentModel.DataAnnotations;
using ProductReviewApp.Domain;

namespace ProductReviewApp.Commands
{
    public class RequestFeatureCommand
    {
        [Required]
        [StringLength(64)]
        public string CreatedBy { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }

    public class RequestFeatureCommandHandler : ICommandHandler<RequestFeatureCommand, Guid>
    {
        public Guid Dispatch(RequestFeatureCommand command)
        {
            Feature feature;

            using (var context = new RoundtuitDbContext())
            {
                feature = new Feature
                {
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = command.CreatedBy,
                    Name = command.Name,
                    Description = command.Description,
                    DisplayOrder = 0,
                    IsRequest = true,
                    Votes = 0
                };

                context.Features.Add(feature);

                context.SaveChanges();
            }

            return feature.Id;
        }
    }
}