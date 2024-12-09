using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ScoreType
{
    public string name; // The name of the score type (e.g., Correct Answers)
    public int value;   // The value of the score (e.g., How many correct answers have been given)
}

[System.Serializable]
public class EventType
{
    public string name; // The name of the event that may occur (e.g., Going to the principals office or falling down a well)
    public int value;   // The amount of times this event has occured
}



[System.Serializable]
public class PlayerProfile : MonoBehaviour
{
    //Player info:
    public string playerName = "Default Name";


    //Time stats:
    [SerializeField] private bool showTimeStats = false; // Foldout toggle
    public bool enableTimeStatTracking = false;
    public float totalTimePlayed = 0;
    public int currentSessionLenght = 0;
    public int previousSessionLenght = 0;


    //Score stats:
    [SerializeField] private bool showScoreStats = false; // Foldout toggle
    public bool enableScoreStatTracking = false;
    public int wins = 0;
    public int losses = 0;
    public int winStreak = 0;
    public int loseStreak = 0;
    public List<ScoreType> scoreTypes = new List<ScoreType>();


    //Combat stats:
    [SerializeField] private bool showCombatStats = false; // Foldout toggle
    public bool enableCombatStatTracking = false;
    public int damageDealt = 0;
    public int damageTaken = 0;
    public int enemiesSlain = 0;
    public int playerDeaths = 0;


    //Distance stats:
    [SerializeField] private bool showDistanceStats = false; // Foldout toggle
    public bool enableDistanceStatTracking = false;
    public int totalDistanceTravelled = 0;
    public int currentSessionTravelledAmount = 0;
    public int previousSessionTravelledAmount = 0;


    //Event stats:
    [SerializeField] private bool showEventStats = false; // Foldout toggle
    public bool enableEventStatTracking = false;
    public int uniqueEventOcurrances = 0;
    public int totalEventOccurrences = 0;
    public int eventWithMostOccurances = 0;
    public int eventWithLeastOccurances = 0;
    public List<EventType> eventTypes = new List<EventType>();


    //Component configure options
    [SerializeField] private bool showConfigureOptions = false; // Foldout toggle
    public bool eraseProfileDataOnNextPlay = false;
    public bool stopSavingProfileData = false;



    //Update component variables
    private void Update()
    {
        totalTimePlayed += Time.deltaTime;
    }
}