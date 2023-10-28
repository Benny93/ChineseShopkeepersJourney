# Chinese shopkeepers journey

This project was created for 2023 PICO Dev Jam

Memorize the mandarin names of countless items lining your shelves and race against the clock to fulfill each customer's order. The faster you serve, the higher your score climbs. Learn by doing.

[![Youtube video](./Documentation/Images/example1_small.jpg)](https://www.youtube.com/watch?v=TKyPnJ57tnI&ab_channel=Kenny32VR)

![Example Image1](./Documentation/Images/example4_small.jpg)

## How To Play

Select the level you want to play.
When a customer arrives at your desk it will state its order as a single word.
Find the matching item on you counter and simply throw it at the customer.
The customer will catch it an you receive points.
The faster you can serve the customers the more points you get.

## Requirements

- Unity 2023.3.0 or newer

## Getting started

The projects code and assets are located in `./Assets/CShopkeepersJourney`.

### How to add new vocabulary

Navigate into the  `./Assets/CShopkeepersJourney/Resouces/Items` directory.
Open the context menu by right clicking the assets browser and click "Create > LearningItem".
A new learning item appears. 
Give it an fitting name and fill in the properties of the learn item:
- The english name
- The translation in pinyin
- The translation in chinese characters
- Audio clip 

### Create new Levels

- Go to the level directory `./Assets/CShopkeepersJourney/Game`.
- Copy and existing level or right click to open the context menu and under create select "LevelSettings".
- Change the level parameters to your liking and add the required learnitems to the level.
- In the Game Scene open the XROrigin tree and find the main UI element. Find the level selection panel and add your level to the list of levels of the component.


## Credits:

This project is based on the microwar project:
- [Github link](https://github.com/picoxr/MicroWar)

Additional Assets used: 

- "Chinese Lamp" (https://skfb.ly/6SxPq) by Aki_Kato is licensed under Creative Commons Attribution (http://creativecommons.org/licenses/by/4.0/).
- Assets from [Kenny.nl](https://www.kenney.nl/)
- SFX from [OpenGameArt](https://opengameart.org/), [Sonniss - Game Audio GDC](https://sonniss.com/gameaudiogdc)
- Sounds from [ttsmp3.com](https://ttsmp3.com/text-to-speech/Chinese%20Mandarin/)
- Unity asset store [https://assetstore.unity.com/](https://assetstore.unity.com/)