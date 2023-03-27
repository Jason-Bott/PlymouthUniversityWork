# 2022-COMP1000-CW

## Video Link

https://youtu.be/aOJ4n_DU6PU

The link above is to a youtube video which describes how to run and play the game

It has quick explainations of my code and the way in which it executes

It also has some descriptions of the software engineering paradigms I used,

what I found was my biggest achievements and what I would have liked to have 

made better in the project

## Project Features

The basic features that have been developed are:
* Loading all maps in the maps folder by using the command "load filename"
* The player will always start on P
* Using the W,A,S,D keys the player can move and complete the map
* Using Z the player can pick up a coin
* Players do not need to press ENTER to make a move
* The players steps are shown under the map

The advanced features that have been developed are:
* These features are only enabled with the use of the command "advanced"
* Using Q the player can attack a nearby monster dealing 1 damage
* Monsters have 3 health points and Players have 2
* Monsters can attack the player
* Monsters can pick up coins
* When a player or monster picks up a coin they gain 1 health point
* When a monster is killed it drops its coins
* The player can replay the game when they exit the dungeon

### Loading Maps

![LoadingMaps](/gifs/LoadMap.gif)

### Player Starting On P

![StartOnP](/gifs/StartOnP.gif)

### Movement with W,A,S,D (Shows Step Counter)

![Movement](/gifs/Movement.gif)

### Pickup Coin with Z

![CoinBasic](/gifs/CoinBasic.gif)

### Advanced Toggle

![AdvancedToggle](/gifs/AdvancedToggle.gif)

### Attacking a Monster and Monster Attacking Back

![Attacking](/gifs/Attacking.gif)

### Pickup Coin in Advanced to Increase Health

![CoinAdvanced](/gifs/CoinAdvanced.gif)

### Dungeon Completion

![Completion](/gifs/Completion.gif)

## Project Progress

Here I will show the progress I have made showing:
* the date of the push
* images of progress
* links to resources used

### 03/11/2022 - Loading The Maps

Added to the methods ProcessUserInput and LoadMapFromFile

If the users input starts with load the second method is called

This method then loads the map into an array and outputs the map to the console

At this point the map can only be seen

* [how to read a text file](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-read-from-a-text-file)
* [how to create directory paths](https://stackoverflow.com/questions/17405180/how-to-read-existing-text-files-without-defining-path)

![Checking for input to load map](/images/LoadMapInput.png)

![Loading map from file into a string array](/images/LoadMapStringArray.png)

### 17/11/2022 - Load Map Into Global Variable

Updated the method LoadMapFromFile

Previously the map would be loaded into a local string array

The map is now loaded into the private originalMap instead

At this point the map can now be used in other methods

* [array resize](https://learn.microsoft.com/en-us/dotnet/api/system.array.resize?view=net-7.0)

![LoadMapFromFile Part 1](/images/LoadMapCharacterArray1.png)
![LoadMapFromFile Part 2](/images/LoadMapCharacterArray2.png)

### 18/11/2022 - Player Movement

Updated ReadUserInput, GetCurrentMapState, ProcessUserInput and PrintMapToConsole

The player can now move around the map

The players move will not be accepted if they try to walk into a wall

At this point the exit doesnt work, coins cannot be picked up and monsters have no effect

* [console.readkey](https://learn.microsoft.com/en-us/dotnet/api/system.console.readkey?view=net-7.0)

![ReadUserInput](/images/ReadUserInput.png)
![GetCurrentMapState1](/images/GetCurrentMapState1.png)
![GetCurrentMapState2](/images/GetCurrentMapState2.png)
![ProcessUserInput](/images/ProcessUserInput.png)
![PrintMapToConsole](/images/PrintMapToConsole.png)

### 05/12/2022 - Step Counter

Added a step counter displayed after the map is displayed

The step counter is only incremented if the action is movement

And only if the movement isn't direceted at a wall or monster

Step counter resets to 0 when new map is loaded

### 08/12/2022

#### Coin Pickup

Added the ability to pick up coins "C" with action "z"

While attempting to add this feature I had to alter the method LoadMapFromFile

This is due to originalMap chaning when workingMap changed so now I load both seperately

![LoadMapFromFile](/images/LoadMapFromFile.PNG)

Checks map to see if there should be a coin in position the player is leaving

If the coin has been picked up the coin will not reappear when leaving its position

Otherwise walking over the coin will not make the coin disappear

![CoinPickup](/images/CoinPickup.PNG)

#### Advanced Toggle

Added a way to switch between the advanced mode and basic mode

At the moment switching to advanced mode has no effect on gameplay

![AdvancedToggle](/images/AdvancedToggle.PNG)

### 15/12/2022 - Start of Advanced Features

Added a health bar. Added the ability to attack monsters and kill them (advanced mode only)

Picking up coins now increases players health by 1

Map can be replayed by typing "replay"

Advanced Map can be completed

![PlayerAttack1](/images/PlayerAttack1.PNG)
![PlayerAttack2](/images/PlayerAttack2.PNG)
![PlayerHealth](/images/PlayerHealth.PNG)
![Replay](/images/Replay.PNG)

### 01/01/2023 - Bug fixes

While playing the game in advanced mode I found that monsters would respawn

This would happen after killing them then moving one space

This was due to my code not checking if a monster was dead before it moved

This was easily fixed with by checking if its health was greater than 0 before moving

### 04/01/2023 - Monster CoinPickup and Attacking

Monsters now pickup coins when they run over them increase their health by 1

Monsters with coins picked up will drop them when killed

Monsters will now attack the player if stood next to them

![MonsterCoinPickup](/images/MonsterCoinPickup.png)
![MonsterAttackCondition](/images/MonsterAttackCondition.png)
![MonsterAttack](/images/MonsterAttack.png)
