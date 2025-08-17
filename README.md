# Event Journal

Collect basic timestamped event data with the intent that the data can be analyzed/mined to see longer term patterns.

Originally this was intended to track symptoms and intensity. But evolved into more of an event tracker system.
The concept of Intensity is still a bit muddy, but the idea is that events of a particular even type can be rated on a user defined scale. For example a headache event can have a pain intensity rating. Where an exercise type event could have a distance or workout intensity.

## Features

Features include:
User definable event types (ex. exercise, meal, headache )
USer definable detail types (ex. muscle pain, bleeding, restaurant or food eaten, pain location, pain level, nausea )
User definable detail intensity descriptions (tied to numeric values for graphing/data presentation purposes)

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
- I can't function (4)
- Take me to the ER (5)

#### Bleeding could be

- None (0)
- Trace (on TP only) (1)
- Minor (drops visible in water) (2)
- Major (water mostly red) (3)
- Intense (water completely red) (4)

### TODO

populate Events domain service - manages Events and Details
populate UserType service - manages all the User configurable types.

Event entries should also have user defined tags.
not sure how to implement tags just yet, maybe a tag entity and table
and a tags list table (with an id) and have journal entry track tag-list id
