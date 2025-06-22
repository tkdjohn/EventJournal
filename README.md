# Event Journal
Collect basic timestamped event data with the intent that the data can be analyzed/mined to see longer term patterns. 

Originally this was intended to track symptoms and intensity. But evolved into more of an event tracker system. 
The concept of Intensity is still a bit muddy, but the idea is that events of a particular even type can be rated on a user defined scale. For example a headache event can have a pain intensity rating. Where an exercise type event could have a distance or workout intensity.



## Features


Possible features include:
User definable symptom lists
User definable symptom intensity descriptions (tied to numeric values for graphing/data presentation purposes) 

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

journal entries should also have user defined tags 
not sure how to implement tags just yet, maybe a tag entity and table
and a tags list table (with an id) and have journal entry track taglist id



