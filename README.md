# Event Journal

Collect basic timestamped event data with the intent that the data can be analyzed/mined to see longer term patterns.

Originally this was intended to track symptoms and intensity. But evolved into more of an event tracker system.
The concept of Intensity is still a bit muddy, but the idea is that events of a particular even type can be rated on a user defined scale. For example a headache event can have a pain intensity rating. Where an exercise type event could have a distance or workout intensity.
<!--TOC-->
  - [Features](#features)
  - [NOTES](#notes)
    - [Entities](#entities)
    - [Intensity Examples](#intensity-examples)
      - [Pain level could be something like](#pain-level-could-be-something-like)
      - [Bleeding could be](#bleeding-could-be)
      - [Exercise Intensity could be](#exercise-intensity-could-be)
      - [Entity Framework help](#entity-framework-help)
    - [NEXT STEPS](#next-steps)
    - [TODO](#todo)
    - [Future Considerations](#future-considerations)
<!--/TOC-->
## Features

- User definable event types (ex. exercise, meal, headache )
- USer definable detail types (ex. muscle pain, bleeding, restaurant or food eaten, pain location, pain level, nausea )
- User definable detail intensity descriptions (tied to numeric values for graphing/data presentation purposes)

## NOTES

### Entities

Events are the central entity. An `Event` has an `EventType` and one or more `Detail` records. Each `Detail` record has a `DetailType` and an `Intensity`.
`EventType`, `DetailType`, and `Intensity` are user defined types.

### Intensity Examples

#### Pain level could be something like

- None (0)
- Slight to mild Discomfort (1)
- I need an OTC painkiller (2)
- I'm going to limit my activity (3)
- I can't function normally (4)
- Take me to the ER (5)

#### Bleeding could be

- None (0)
- Trace (on TP only) (1)
- Minor (drops visible in water) (2)
- Major (water mostly red) (3)
- Intense (water completely red) (4)

#### Exercise Intensity could be
- Took it easy (0)
- Pushed a bit (1)
- Heart Pumping and light sweating (2)
- Breathing Hard and sweating good (3)
- Hardcore Must take it easy tomorrow (4)

#### Entity Framework help
EF https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli


### NEXT STEPS
- Add details, detail types, and intensities to the initializer
- update detail, detail type repositories to return related entities
    - detail should include detail type and intensity and detailtypes.AllowedIntensities
    - detail type should include valid intensities
- test full get event with details
- test full get detail type with allowed intensities
- add multiple default detail types to initializer (a la event types)
- add multiple default intensities to initializer (a la event types)
### TODO
- unit tests for repositories and services. Also base entity code?
- Web API to call service methods
- Add common BootStrap code to be consumed by cli and web api projects
    - remove microsoft.extension.hosting pkg where not needed
- move initializer code to an appropriate place 
    - want to let user get some defaults to start with 
    - but also wan to use these defaults for testing
- Entity validators
### Future Considerations
- basic CRUD web UI 
    - want to add the ability to add new types on the fly as a new event is added
- Integration tests for  web api.
- `Event` entries could also have user defined tags. (Maybe `Details` could have tags too?)
not sure how to implement tags just yet, maybe a tag entity and table
and a tags list table (with an id) and have journal entry track tag-list id
