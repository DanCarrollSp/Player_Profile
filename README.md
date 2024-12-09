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