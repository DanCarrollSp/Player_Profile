# Player Profile Guide

This guide provides step-by-step instructions for setting up and using the **Player Profile** package in Unity.

---

## Getting Started

1. **Import the Package**

   To import the package from a Git URL, add the following entry to your Unity project's `manifest.json` file under the `dependencies` section:

   ```json
   {
     "dependencies": {
       "ie.setu.playerprofile": "https://github.com/DanCarrollSp/Player_Profile.git?path=/Player profile/Packages/ie.setu.playerprofile",
       // ...other dependencies
     }
   }

2. **Attach the Component to a GameObject**

1. Locate the **PlayerProfile.cs** script in the Player Profile package.
2. Drag and drop the script onto a GameObject in your scene.
3. Once attached, you will see the component's properties and configuration options in the Inspector.

---

## Using the Player Profile Component

The Player Profile component comes preloaded with a variety of attributes and features that can be customized to suit your game. These features include:

### **Core Features**
- **Player Name**: A customizable field for identifying the player.
- **Time Statistics**:
  - **Total Time Played**
  - **Current Session Length**
  - **Previous Session Length**
- **Score Statistics**:
  - Tracks wins, losses, win streaks, and lose streaks.
  - Includes a modifiable list for creating custom score types (e.g., coins collected, levels completed).
- **Distance Statistics**:
  - Tracks total distance traveled.
  - Monitors travel distance for the current and previous sessions.
- **Combat Statistics**:
  - Records damage dealt and taken.
  - Tracks enemies defeated and player deaths.

### **Customizable Event Tracking**
The component also features an **Event Stats** system that allows you to:
- Create and monitor custom events specific to your game.
- Track unique occurrences, making the component universally adaptable to various game genres.

### **Data Backup and Management**
The Player Profile package includes a **built-in menu** for managing player data:
- **Backup and Save Data**: Uses `PlayerPrefs` to store player data securely.
- **Custom Save Interval**: Set an editable interval for automatic saving or disable saving altogether.
- **Erase Saved Data**: Clear saved data for debugging purposes with ease.

---


## Functions Guide

### **Score functions**
- **addWin(int addAmount)**: Calling addWin(x) will add x to the wins stat.
- **addLosses(int addAmount)**: Calling addLosses(x) will add x to the losses stat.
- Both winStreak() and loseStreak() will handle the winning and loosing streak statistics without needing to be called from outside the component, based on the win and losses stats.

### **Distance functions**
Distance statistics are calculated in the script by the movement of the GameObject the component is attached to.

### **Combat functions**
- **addDamageDealt(int addAmount)**: Calling addDamageDealt(x) will add x to the damagedDealt stat.
- **addDamageReceived(int addAmount)**: Calling addDamageReceived(x) will add x to the damageReceived stat.
- **addEnemiesSlain(int addAmount)**: Calling addEnemiesSlain(x) will add x to the enemiesSlain stat.
- **addPlayerDeaths(int addAmount)**: Calling addPlayerDeaths(x) will add x to the playerDeaths stat.

### **Event function**
- **addOccurrences(int listElement, int addOccurrence)**: Calling addOccurrences(x , y) will add y to the x event stat.
- EG. If you add an event type to the event list in the inspector by pressing + on the bottom right of the list in the inspector menu and call it "Trees Chopped".
- If it is the 0 element in the list, calling addOccurrence(0,1) will add 1 to the "Trees chopped" statistic you created.

### **Saving Data**
- **Erase profile data on play**: Enabling this bool will cause all saved data to be wiped on the next launch of play mode.
- **Save profile data**: Enabling this will cause all data to be saved between game sessions for debug purposes. It is enabled by default.
- **Save interval in seconds**: The value of this determines how often periodic saves occur, if saving is enabled saving will also occur on game shutdown, if saving is disabled the game will NOT save peridoically regardless of interval.





