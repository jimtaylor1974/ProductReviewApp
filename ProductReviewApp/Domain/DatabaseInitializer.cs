using System;
using System.Data.Entity;

namespace ProductReviewApp.Domain
{
    // http://www.entityframeworktutorial.net/code-first/database-initialization-strategy-in-code-first.aspx

    public class DatabaseInitializer : CreateDatabaseIfNotExists<RoundtuitDbContext>
    {
        public DatabaseInitializer(RoundtuitDbContext context)
        {
            if (context.Database.Exists() && !context.Database.CompatibleWithModel(false))
            {
                context.Database.Delete();
            }

            if (context.Database.Exists())
            {
                return;
            }

            context.Database.Create();

            SeedDatabase(context);
        }

        protected override void Seed(RoundtuitDbContext context)
        {
            SeedDatabase(context);
        }

        public static void SeedDatabase(RoundtuitDbContext context)
        {
            var features = new[]
            {
                new Feature
                {
                    Name = "Prioritised todo list",
                    Description = "Know what you should be working on and when to do it with our handy list screen.",
                    CreatedBy = "Roundtu.it",
                    CreatedOn = DateTime.UtcNow,
                    DisplayOrder = 1,
                    Image = "business-1.jpg",
                    IsRequest = false
                },
                new Feature
                {
                    Name = "Don't panic!",
                    Description = "With our handy messaging and alerting system you'll never miss a deadline. Message preferences can be configured in the app with SMS and email alerting.",
                    CreatedBy = "Roundtu.it",
                    CreatedOn = DateTime.UtcNow,
                    DisplayOrder = 2,
                    Image = "business-2.jpg",
                    IsRequest = false
                },
                new Feature
                {
                    Name = "Teamwork",
                    Description = "Join a team and share and manage each other todo lists. Manage priority conflicts with our scheduling and workload manager.",
                    CreatedBy = "Roundtu.it",
                    CreatedOn = DateTime.UtcNow,
                    DisplayOrder = 3,
                    Image = "business-3.jpg",
                    IsRequest = false
                },
                new Feature
                {
                    Name = "Conference",
                    Description = "Feature description - Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.",
                    CreatedBy = "Roundtu.it",
                    CreatedOn = DateTime.UtcNow,
                    DisplayOrder = 4,
                    Image = "business-4.jpg",
                    IsRequest = false
                },
                new Feature
                {
                    Name = "Super business mode",
                    Description = "Feature description - Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.",
                    CreatedBy = "Roundtu.it",
                    CreatedOn = DateTime.UtcNow,
                    DisplayOrder = 5,
                    Image = "business-5.jpg",
                    IsRequest = false
                },
                new Feature
                {
                    Name = "Another unmissable feature",
                    Description = "Feature description - Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.",
                    CreatedBy = "Roundtu.it",
                    CreatedOn = DateTime.UtcNow,
                    DisplayOrder = 6,
                    Image = "business-6.jpg",
                    IsRequest = false
                }
            };

            var feedbackItems = new[]
            {
                "Great feature, but it would be better if ...",
                "This so frustrating, whenever I click on x I lose the navigation to y",
                "A real timesaver, thank you.",
                "This is too slow when I'm on my mobile device.",
                "Can you add a copy link here?",
                "This would benefit from an export as feature like in xyx.",
                "Hi great site. I have learn't a lot from it. Please to check out my site at http://cheap-rolex-wathces.5765-xyz.com",
                "Awesome feature, you guys rock.",
                "Can you add support for my obscure usecase? ...",
                "Works perfectly, please don't change it!"
            };

            foreach (var feature in features)
            {
                context.Features.Add(feature);

                if (feature.DisplayOrder == 6)
                {
                    foreach (var feedbackItem in feedbackItems)
                    {
                        var feedback = new Feedback
                        {
                            CreatedBy = "Guest user",
                            CreatedOn = DateTime.UtcNow,
                            Feature = feature,
                            Text = feedbackItem
                        };

                        context.Feedback.Add(feedback);
                    }
                }
            }

            var featureRequests = new[]
            {
                new Feature
                {
                    CreatedBy = "Guest user",
                    CreatedOn = new DateTime(2014, 07, 16),
                    Votes = 0,
                    Name = @"Add SMS text notifications",
                    Description = @"Can you add SMS alerting for when I'm away from the office.",
                    IsRequest = true
                },
                new Feature
                {
                    CreatedBy = "Guest user",
                    CreatedOn = new DateTime(2014, 07, 18),
                    Votes = -1,
                    Name = @"Sync multiple calendars",
                    Description = @"I would like the ability to sync my todos across multiple calendars on my iPhone and my work outlook calendar.",
                    IsRequest = true
                },
                new Feature
                {
                    CreatedBy = "Guest user",
                    CreatedOn = new DateTime(2014, 07, 19),
                    Votes = 3,
                    Name = @"Export appointments to iCal",
                    Description = @"I would like the ability to click on the appointment and download the iCal file.",
                    IsRequest = true
                },
            };

            var comments = new[]
            {
                "I like this idea.",
                "I am indifferent to this idea.",
                "No thank you, it works fine as it is. Simple is best.",
                "Great idea, while you're at it you could ..."
            };

            foreach (var featureRequest in featureRequests)
            {
                context.Features.Add(featureRequest);

                if (featureRequest.Votes == 3)
                {
                    foreach (var comment in comments)
                    {
                        var feedback = new Feedback
                        {
                            CreatedBy = "Guest user",
                            CreatedOn = DateTime.UtcNow,
                            Feature = featureRequest,
                            Text = comment
                        };

                        context.Feedback.Add(feedback);
                    }
                }
            }

            context.SaveChanges();
        }
    }
}