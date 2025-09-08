using EventJournal.Common.Enumerations;
using EventJournal.DomainDto;
using EventJournal.DomainDto.UserTypes;

namespace EventJournal.DomainService {
    public class DefaultDataProvider(
        IEventService eventService,
        IUserTypesService userTypesService)
    : IDefaultDataProvider {
        private static readonly Guid DefaultEventResourceId = Guid.Parse("00000000-0000-0000-0000-000000000001");

        private static readonly Guid[] DefaultEventTypeResourceIds = [
            Guid.Parse("00000000-0000-0000-0000-000000000001"),
            Guid.Parse("00000000-0000-0000-0000-000000000002"),
            Guid.Parse("00000000-0000-0000-0000-000000000003"),
            Guid.Parse("00000000-0000-0000-0000-000000000004"),
            Guid.Parse("00000000-0000-0000-0000-000000000005")
         ];
        private static readonly Guid[] DefaultDetailTypeResourceIds = [
            Guid.Parse("00000000-0000-0000-0000-000000000001"),
            Guid.Parse("00000000-0000-0000-0000-000000000002"),
            Guid.Parse("00000000-0000-0000-0000-000000000003")
        ];

        public async Task AddResetDefaultDataAsync() {
            var defaultEventTypes = await userTypesService.AddUpdateEventTypesAsync([
                new EventTypeDto{ ResourceId = DefaultEventTypeResourceIds[0], Name = "Random Event", Description="Use for tracking random things like onset of pain, headache, or whatever that isn't directly associated with a specific event type." },
                new EventTypeDto{ ResourceId = DefaultEventTypeResourceIds[1], Name = "Bowel Movement", Description=""},
                new EventTypeDto{ ResourceId = DefaultEventTypeResourceIds[2], Name = "Ate Something", Description=""},
                new EventTypeDto{ ResourceId = DefaultEventTypeResourceIds[3], Name = "Weigh In", Description=""},
                new EventTypeDto{ ResourceId = DefaultEventTypeResourceIds[4], Name = "Exercise", Description=""}
            ]).ConfigureAwait(false);

            IEnumerable<DetailTypeDto> defaultDetailTypeDtos = await userTypesService.AddUpdateDetailTypesAsync([
                new DetailTypeDto {
                    ResourceId = DefaultDetailTypeResourceIds[1],
                    Description = "This is a generic detail type",
                    Name = "Generic",
                    IntensitySortType = SortType.Descending,
                    AllowedIntensities =  [
                        new IntensityDto {
                            ResourceId = Guid.NewGuid(),
                            Level = 0,
                            Name = "Zero Intensity",
                            Description = "Not intense at all. Normal."
                        },
                        new IntensityDto {
                            ResourceId = Guid.NewGuid(),
                            Level = 1,
                            Name = "Mild Intensity",
                            Description = "Almost broke a sweat."
                        },
                        new IntensityDto {
                            ResourceId = Guid.NewGuid(),
                            Level = 2,
                            Name = "Moderate Intensity",
                            Description = "Got sweaty, did some breathing. Might feel this tomorrow."
                        },
                        new IntensityDto {
                            ResourceId = Guid.NewGuid(),
                            Level = 3,
                            Name = "High Intensity",
                            Description = "Breathing hard. Will definitely feel this for a few days."
                        },
                        new IntensityDto {
                            ResourceId = Guid.NewGuid(),
                            Level = 4,
                            Name = "Insane Intensity",
                            Description = "Unbearable. Insane. No one needs to experience this. Why did I do this to myself?"
                        }
                    ]
                },
                //TODO: breakfast seems a bit too specific for a default detail type but it's an example of how to use detail types and intensities
                // Consider changing to something more generic like "Meal" or "Food Intake" and leaving the specifics to the Detail Notes
                new DetailTypeDto {
                    ResourceId = DefaultDetailTypeResourceIds[2],
                    Description = "Use this detail type to track your breakfast.",
                    Name = "Meal",
                    IntensitySortType = SortType.Descending,
                    AllowedIntensities =  [
                        new IntensityDto {
                            ResourceId = Guid.NewGuid(),
                            Level = 0,
                            Name = "Skipped",
                            Description = "Skipped altogether or just had coffee, tea, or a soda."
                        },
                        new IntensityDto {
                            ResourceId = Guid.NewGuid(),
                            Level = 1,
                            Name = "Unhealthy",
                            Description = "Ate something but it wasn't really a healthy choice."
                        },
                        new IntensityDto {
                            ResourceId = Guid.NewGuid(),
                            Level = 2,
                            Name = "Moderately Healthy",
                            Description = "Made a moderately healthy choice."
                        },
                        new IntensityDto {
                            ResourceId = Guid.NewGuid(),
                            Level = 3,
                            Name = "Power Meal",
                            Description = "Made a very healthy choice."
                        }
                    ]
                }
            ]).ConfigureAwait(false);

            var eventDtos = await eventService.AddUpdateEventsAsync([
                new EventDto {
                    ResourceId = DefaultEventResourceId,
                    StartTime = DateTime.Now,
                    Description = "Event History Started",
                    EventType = defaultEventTypes.First(d => d.Name == "Random Event")
                },
                new EventDto {
                    ResourceId = Guid.NewGuid(),
                    StartTime = DateTime.Now.AddHours(-1),
                    EndTime = DateTime.Now.AddHours(-1).AddMinutes(30),
                    Description = "Ate breakfast",
                    EventType = defaultEventTypes.First(et => et.Name == "Ate Something")
                }
            ]);

            var genericDetailType = defaultDetailTypeDtos.First(dt => dt.Name == "Generic");
            var mealDetailType = defaultDetailTypeDtos.First(dt => dt.Name == "Meal");
            await eventService.AddUpdateDetailsAsync(eventDtos.First().ResourceId, [
                new DetailDto {
                    ResourceId = Guid.NewGuid(),
                    DetailType = genericDetailType,
                    Intensity = genericDetailType.AllowedIntensities.First(i => i.Level == 2),
                    Notes = "Congratulations on starting your Event History!\nTake the next step and add another event!"
                },
                new DetailDto {
                    ResourceId = Guid.NewGuid(),
                    DetailType = mealDetailType,
                    Intensity = mealDetailType.AllowedIntensities.First(i => i.Level == 1),
                    Notes = "This is an example of how you can use details to track more information about your events.\nYou can add multiple details to an event."
                }
            ]);

            await eventService.AddUpdateDetailsAsync(eventDtos.ElementAt(1).ResourceId, [
                new DetailDto {
                    ResourceId = Guid.NewGuid(),
                    DetailType = mealDetailType,
                    Intensity = mealDetailType.AllowedIntensities.First(i => i.Level == 0),
                    Notes = "Was running late - Just had coffee."
                },
                new DetailDto {
                    ResourceId = Guid.NewGuid(),
                    DetailType = mealDetailType,
                    Intensity = mealDetailType.AllowedIntensities.First(i => i.Level == 3),
                    Notes = "Realized I had time to stop at the cafe and grab Eggs and Fruit"
                }
            ]);
        }

    }
}
