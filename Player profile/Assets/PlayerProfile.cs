using System;
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
    public float currentSessionLenght = 0;
    public float previousSessionLenght = 0;


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
    public float totalDistanceTravelled = 0;
    public float currentSessionTravelAmount = 0;
    public float previousSessionTravelAmount = 0;


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
    public bool saveProfileData = false;
    public float saveInterval = 5;

    //Component configure variables
    private float timer;




    // Functions //

    private void Start()
    {
        if (eraseProfileDataOnNextPlay)
        {
            eraseData();
        }
        else
        {
            readData();
        }
    }
    
    private void OnApplicationQuit()
    {
        previousSessionLenght = currentSessionLenght;
        if (saveProfileData) saveData();
    }


    //Update component variables
    private void Update()
    {
        if (enableTimeStatTracking == true) updateTime();
        if (enableScoreStatTracking == true) updateScore();
        if (enableCombatStatTracking == true) updateCombat();
        if (enableDistanceStatTracking == true) updateDistance();
        if (enableEventStatTracking == true) updateEvent();

        updateConfigureOptions();


        if (saveProfileData)
        {
            //Saves profile data based on set saveInterval
            timer += Time.deltaTime;
            if (timer >= saveInterval)
            {
                saveData();
                timer = 0f;
                Debug.Log("Player Profile Saved");
            }
        }
    }



    void updateTime()
    {
        totalTimePlayed += Time.deltaTime;
        currentSessionLenght += Time.deltaTime;

        //previousSessionLenght =

    }

    void updateScore()
    {

    }

    void updateCombat()
    {

    }

    void updateDistance()
    {

    }

    void updateEvent()
    {

    }

    void updateConfigureOptions()
    {

    }


    //Save data to playerprefs
    void saveData()
    {
        //Save Time stats
        PlayerPrefs.SetFloat("totalTimePlayed", totalTimePlayed);
        //PlayerPrefs.SetFloat("currentSessionLenght", currentSessionLenght); // Will never be saved as its only counting the current session
        PlayerPrefs.SetFloat("previousSessionLenght", previousSessionLenght);

        //Save Score stats
        PlayerPrefs.SetFloat("wins", wins);
        PlayerPrefs.SetFloat("losses", losses);
        PlayerPrefs.SetFloat("winStreak", winStreak);
        PlayerPrefs.SetFloat("loseStreak", loseStreak);
        //Put code in here to save data in the list

        //Save Distance stats
        PlayerPrefs.SetFloat("totalDistanceTravelled", totalDistanceTravelled);
        PlayerPrefs.SetFloat("currentSessionTravelAmount", currentSessionTravelAmount);
        PlayerPrefs.SetFloat("previousSessionTravelAmount", previousSessionTravelAmount);

        //Save Combat stats
        PlayerPrefs.SetFloat("damageDealt", damageDealt);
        PlayerPrefs.SetFloat("damageTaken", damageTaken);
        PlayerPrefs.SetFloat("enemiesSlain", enemiesSlain);
        PlayerPrefs.SetFloat("playerDeaths", playerDeaths);

        //Save Event stats
        PlayerPrefs.SetFloat("totalEventOccurrences", totalEventOccurrences);
        PlayerPrefs.SetFloat("eventWithMostOccurances", eventWithMostOccurances);
        PlayerPrefs.SetFloat("eventWithLeastOccurances", eventWithLeastOccurances);
        PlayerPrefs.SetFloat("uniqueEventOcurrances", uniqueEventOcurrances);
        //Put code in here to save data in the list

        PlayerPrefs.Save();
    }


    //Read data from playerprefs
    void readData()
    {
        //Read Time stats
        totalTimePlayed = PlayerPrefs.GetFloat("totalTimePlayed");
        currentSessionLenght = PlayerPrefs.GetFloat("currentSessionLenght");
        previousSessionLenght = PlayerPrefs.GetFloat("previousSessionLenght");

        //Read Score stats
        wins = PlayerPrefs.GetInt("wins");
        losses = PlayerPrefs.GetInt("losses");
        winStreak = PlayerPrefs.GetInt("winStreak");
        loseStreak = PlayerPrefs.GetInt("loseStreak");
        //Put code in here to Read data in the list

        //Read Distance stats
        totalDistanceTravelled = PlayerPrefs.GetFloat("totalDistanceTravelled");
        currentSessionTravelAmount = PlayerPrefs.GetFloat("currentSessionTravelAmount");
        previousSessionTravelAmount = PlayerPrefs.GetFloat("previousSessionTravelAmount");

        //Read Combat stats
        damageDealt = PlayerPrefs.GetInt("damageDealt");
        damageTaken = PlayerPrefs.GetInt("damageTaken");
        enemiesSlain = PlayerPrefs.GetInt("enemiesSlain");
        playerDeaths = PlayerPrefs.GetInt("playerDeaths");

        //Read Event stats
        totalEventOccurrences = PlayerPrefs.GetInt("totalEventOccurrences");
        eventWithMostOccurances = PlayerPrefs.GetInt("eventWithMostOccurances");
        eventWithLeastOccurances = PlayerPrefs.GetInt("eventWithLeastOccurances");
        uniqueEventOcurrances = PlayerPrefs.GetInt("uniqueEventOcurrances");
        //Put code in here to Read data in the list
    }


    //Function to erase all data from fields
    void eraseData()
    {
        //Erase Time stats
        totalTimePlayed = 0;
        currentSessionLenght = 0;
        previousSessionLenght = 0;

        //Erase Score stats
        wins = 0;
        losses = 0;
        winStreak = 0;
        loseStreak = 0;
        //Put code in here to erase data in the list but not the list itself

        //Erase Distance stats
        totalDistanceTravelled = 0;
        currentSessionTravelAmount = 0;
        previousSessionTravelAmount = 0;

        //Erase Combat stats
        damageDealt = 0;
        damageTaken = 0;
        enemiesSlain = 0;
        playerDeaths = 0;

        //Erase Event stats
        totalEventOccurrences = 0;
        eventWithMostOccurances = 0;
        eventWithLeastOccurances = 0;
        uniqueEventOcurrances = 0;
        //Put code in here to erase data in the list but not the list itself


        /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
        /// Now that data has been reset, clear player prefs of their values also
        saveData();
    }
}