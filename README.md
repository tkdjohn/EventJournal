# Hippocrates Journal
Collect basic timestamped symptom data with the intent that the data can be analyzed/mined to see longer term patterns. 

To start, this will focus on symptoms and intensity and intentionally be a single user app. Longer term, the idea is to be able to couple this data with other similar journal data (such as food and medicine tracking). With this in mind the journal entries will be fairly generic in nature.
Maybe rather than tracking symptoms, food, exercise separately, we track EVENTS
with user defined event types such as


## Features


Possible features include:
User definable symptom lists
User definable symptom intensity descriptions (tied to numeric values for graphing/data presentation purposes) 

####	Pain level could be something like 
		
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
- Intense (water completly red) (4)

journal entries should also have user defined tags 
not sure how to implement tags jsut yet, maybe a tag entity and table
and a tags list table (with an id) and have journal entry track taglist id



