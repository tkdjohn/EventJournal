using EventJournal.Common.Enumerations;
using EventJournal.DomainDto;
using EventJournal.DomainDto.UserTypes;

namespace EventJournal.DomainService {
    public class DefaultDataProvider(
        IEventService eventService,
        IUserTypesService userTypesService)
    : IDefaultDataProvider {
        private static readonly Guid DefaultEventResourceId = Guid.Parse("00000000-0000-0000-0000-000000000001");
        private static readonly Guid DefaultDetailResourceId = Guid.Parse("00000000-0000-0000-0000-000000000001");
        private static readonly Guid[] DefaultEventTypeResourceIds = [
            Guid.Parse("00000000-0000-0000-0000-000000000001"),
            Guid.Parse("00000000-0000-0000-0000-000000000002"),
            Guid.Parse("00000000-0000-0000-0000-000000000003"),
            Guid.Parse("00000000-0000-0000-0000-000000000004"),
            Guid.Parse("00000000-0000-0000-0000-000000000005")
         ];
        private static readonly Guid DefaultDetailTypeResourceId = Guid.Parse("00000000-0000-0000-0000-000000000001");

        private static readonly Guid[] DefaultIntensityResourceIds = [
            Guid.Parse("00000000-0000-0000-0000-000000000001"),
            Guid.Parse("00000000-0000-0000-0000-000000000002"),
            Guid.Parse("00000000-0000-0000-0000-000000000003"),
            Guid.Parse("00000000-0000-0000-0000-000000000004"),
            Guid.Parse("00000000-0000-0000-0000-000000000005"),
         ];

        public async Task AddResetDefaultDataAsync() {
            var defaultEventTypes = await userTypesService.AddUpdateEventTypesAsync([
                new EventTypeDto{ ResourceId = DefaultEventTypeResourceIds[0], Name = "Random Event", Description="Use for tracking random things like onset of pain, headache, or whatever that isn't directly associated with a specific event type." },
                new EventTypeDto{ ResourceId = DefaultEventTypeResourceIds[1], Name = "Bowel Movement", Description=""},
                new EventTypeDto{ ResourceId = DefaultEventTypeResourceIds[2], Name = "Ate Something", Description=""},
                new EventTypeDto{ ResourceId = DefaultEventTypeResourceIds[3], Name = "Weigh In", Description=""},
                new EventTypeDto{ ResourceId = DefaultEventTypeResourceIds[4], Name = "Exercise", Description=""}
            ]).ConfigureAwait(false);

            var defaultDetailTypeDto = await userTypesService.AddUpdateDetailTypeAsync(new DetailTypeDto {
                ResourceId = DefaultDetailTypeResourceId,
                Description = "This is a generic detail type",
                Name = "Generic Detail Type",
                IntensitySortType = SortType.Descending,
                AllowedIntensities = [
                    new() {
                        ResourceId = DefaultIntensityResourceIds[0],
                        Level = 0,
                        Name = "Zero Intensity",
                        Description = "Not intense at all. Normal."
                    },
                    new() {
                        ResourceId = DefaultIntensityResourceIds[1],
                        Level = 1,
                        Name = "Mild Intensity",
                        Description = "Almost broke a sweat."
                    },
                    new() {
                        ResourceId = DefaultIntensityResourceIds[2],
                        Level = 2,
                        Name = "Moderate Intensity",
                        Description = "Got sweaty, did some breathing. Might feel this tomorrow."
                    },
                    new() {
                        ResourceId = DefaultIntensityResourceIds[3],
                        Level = 3,
                        Name = "High Intensity",
                        Description = "Breathing hard. Will definitely feel this for a few days."
                    },
                    new() {
                        ResourceId = DefaultIntensityResourceIds[4],
                        Level = 4,
                        Name = "Insane Intensity",
                        Description = "Unbearable. Insane. No one needs to experience this. Why did I do this to myself?"
                    }
                ]
            }).ConfigureAwait(false);


            var eventDto = await eventService.AddUpdateEventAsync(new EventDto {
                ResourceId = DefaultEventResourceId,
                StartTime = DateTime.Now,
                Description = "Event History Started",
                EventType = defaultEventTypes.First()
            });

            await eventService.AddUpdateDetailAsync(eventDto.ResourceId, new DetailDto {
                ResourceId = DefaultDetailResourceId,
                DetailType = defaultDetailTypeDto,
                Intensity = defaultDetailTypeDto.AllowedIntensities.First(),
                Notes = "Congratulations on starting your Event History!\nTake the next step and add another event!"
            });
        }

    }
}
