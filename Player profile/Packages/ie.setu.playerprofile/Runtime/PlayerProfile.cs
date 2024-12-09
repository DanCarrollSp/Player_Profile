using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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


//For saving and reading lists to Playerprefs
[System.Serializable]
public class ScoreTypeListWrapper
{
    public List<ScoreType> list;
}

[System.Serializable]
public class EventTypeListWrapper
{
    public List<EventType> list;
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


    //Distance stats:
    [SerializeField] private bool showDistanceStats = false; // Foldout toggle
    public bool enableDistanceStatTracking = false;
    public float totalDistanceTravelled = 0;
    public float currentSessionTravelAmount = 0;
    public float previousSessionTravelAmount = 0;


    //Combat stats:
    [SerializeField] private bool showCombatStats = false; // Foldout toggle
    public bool enableCombatStatTracking = false;
    public float damageDealt = 0;
    public float damageTaken = 0;
    public int enemiesSlain = 0;
    public int playerDeaths = 0;


    //Event stats:
    [SerializeField] private bool showEventStats = false; // Foldout toggle
    public bool enableEventStatTracking = false;
    public int uniqueEventOccurrances = 0;
    public int totalEventOccurrences = 0;
    public string eventWithMostOccurances = "Not set";
    public string eventWithLeastOccurances = "Not set";
    public List<EventType> eventTypes = new List<EventType>();


    //Component configure options
    [SerializeField] private bool showConfigureOptions = false; // Foldout toggle
    public bool eraseProfileDataOnNextPlay = false;
    public bool saveProfileData = false;
    [Tooltip("Amount is in seconds.")]
    public int saveIntervalInSeconds = 5;

    //Component configure variables
    private int timer;




    // Functions //

    private void Start()
    {
        if (eraseProfileDataOnNextPlay)
        {
            eraseData();
        }
        else
        {
            //saveData();
            readData();
        }
    }
    
    private void OnApplicationQuit()
    {
        previousSessionLenght = currentSessionLenght;//Previous session Time
        previousSessionTravelAmount = currentSessionTravelAmount;//Previous session Distance travelled
        if (saveProfileData) saveData();
    }


    //Update component variables
    private void Update()
    {
        if (enableTimeStatTracking == true) updateTime();
        if (enableDistanceStatTracking == true) updateDistance();
        if (enableEventStatTracking == true) updateEvent();


        if (saveProfileData)
        {
            //Saves profile data based on set saveInterval
            timer += (int)Time.deltaTime;
            if (timer >= saveIntervalInSeconds)
            {
                saveData();
                timer = 0;
                Debug.Log("Player Profile Saved");
            }
        }
    }


    void updateTime()
    {
        totalTimePlayed += Time.deltaTime;
        currentSessionLenght += Time.deltaTime;
    }
    

    //Local vars
    private Vector3 lastPosition;
    void updateDistance()
    {
        if (lastPosition == default)
        {
            lastPosition = transform.position;
            return;
        }

        //Calculate how far the scripts parent has moved since the last update
        float distanceMoved = Vector3.Distance(transform.position, lastPosition);

        //Add that amount to both totalDistanceTravelled and currentSessionTravelAmount
        totalDistanceTravelled += distanceMoved;
        currentSessionTravelAmount += distanceMoved;

        //Update the stored position for the next frame
        lastPosition = transform.position;
    }


    void updateEvent()
    {
        // First, handle the case where there are no events
        if (eventTypes.Count == 0)
        {
            totalEventOccurrences = 0;
            uniqueEventOccurrances = 0;

            eventWithMostOccurances = "";
            eventWithLeastOccurances = "";
            return;
        }

        // Calculate total occurrences and unique occurrences
        int total = 0;
        int uniqueCount = 0;
        foreach (var evt in eventTypes)
        {
            total += evt.value;
            if (evt.value > 0)
                uniqueCount++;
        }

        totalEventOccurrences = total;
        uniqueEventOccurrances = uniqueCount;

        // Find the events with the most and least occurrences
        // Start by assuming the first event is both max and min
        EventType maxEvent = eventTypes[0];
        EventType minEvent = eventTypes[0];

        foreach (var evt in eventTypes)
        {
            if (evt.value > maxEvent.value)
                maxEvent = evt;

            if (evt.value < minEvent.value)
                minEvent = evt;
        }

        // Assign the names to the string fields
        eventWithMostOccurances = maxEvent.name;
        eventWithLeastOccurances = minEvent.name;
    }


    //Save data to playerprefs
    void saveData()
    {
        //Save Time stats
        PlayerPrefs.SetFloat("totalTimePlayed", totalTimePlayed);
        //PlayerPrefs.SetFloat("currentSessionLenght", currentSessionLenght); // Will never be saved as its only counting the current session
        PlayerPrefs.SetFloat("previousSessionLenght", previousSessionLenght);

        //Save Score stats
        PlayerPrefs.SetInt("wins", wins);
        PlayerPrefs.SetInt("losses", losses);
        PlayerPrefs.SetInt("winStreak", winStreak);
        PlayerPrefs.SetInt("loseStreak", loseStreak);
        //Serialize and Save ScoreTypes
        ScoreTypeListWrapper scoreWrapper = new ScoreTypeListWrapper { list = scoreTypes };
        string scoreJson = JsonUtility.ToJson(scoreWrapper);
        PlayerPrefs.SetString("ScoreTypes", scoreJson);

        //Save Distance stats
        PlayerPrefs.SetFloat("totalDistanceTravelled", totalDistanceTravelled);
        //PlayerPrefs.SetFloat("currentSessionTravelAmount", currentSessionTravelAmount); // Will never be saved as its only counting the current session
        PlayerPrefs.SetFloat("previousSessionTravelAmount", previousSessionTravelAmount);

        //Save Combat stats
        PlayerPrefs.SetFloat("damageDealt", damageDealt);
        PlayerPrefs.SetFloat("damageTaken", damageTaken);
        PlayerPrefs.SetInt("enemiesSlain", enemiesSlain);
        PlayerPrefs.SetInt("playerDeaths", playerDeaths);

        //Save Event stats
        PlayerPrefs.SetInt("totalEventOccurrences", totalEventOccurrences);
        PlayerPrefs.SetString("eventWithMostOccurances", eventWithMostOccurances);
        PlayerPrefs.SetString("eventWithLeastOccurances", eventWithLeastOccurances);
        PlayerPrefs.SetInt("uniqueEventOccurrances", uniqueEventOccurrances);
        //Serialize and Save EventTypes
        EventTypeListWrapper eventWrapper = new EventTypeListWrapper { list = eventTypes };
        string eventJson = JsonUtility.ToJson(eventWrapper);
        PlayerPrefs.SetString("EventTypes", eventJson);

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
        //Read ScoreTypes
        string scoreJson = PlayerPrefs.GetString("ScoreTypes", "");
        if (!string.IsNullOrEmpty(scoreJson))
        {
            ScoreTypeListWrapper scoreWrapper = JsonUtility.FromJson<ScoreTypeListWrapper>(scoreJson);
            scoreTypes = scoreWrapper.list;
        }
        else scoreTypes = new List<ScoreType>();

        //Read Distance stats
        totalDistanceTravelled = PlayerPrefs.GetFloat("totalDistanceTravelled");
        currentSessionTravelAmount = PlayerPrefs.GetFloat("currentSessionTravelAmount");
        previousSessionTravelAmount = PlayerPrefs.GetFloat("previousSessionTravelAmount");

        //Read Combat stats
        damageDealt = PlayerPrefs.GetFloat("damageDealt");
        damageTaken = PlayerPrefs.GetFloat("damageTaken");
        enemiesSlain = PlayerPrefs.GetInt("enemiesSlain");
        playerDeaths = PlayerPrefs.GetInt("playerDeaths");

        //Read Event stats
        totalEventOccurrences = PlayerPrefs.GetInt("totalEventOccurrences");
        eventWithMostOccurances = PlayerPrefs.GetString("eventWithMostOccurances");
        eventWithLeastOccurances = PlayerPrefs.GetString("eventWithLeastOccurances");
        uniqueEventOccurrances = PlayerPrefs.GetInt("uniqueEventOccurrances");
        //Read EventTypes
        string eventJson = PlayerPrefs.GetString("EventTypes", "");
        if (!string.IsNullOrEmpty(eventJson))
        {
            EventTypeListWrapper eventWrapper = JsonUtility.FromJson<EventTypeListWrapper>(eventJson);
            eventTypes = eventWrapper.list;
        }
        else eventTypes = new List<EventType>();
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
        eventWithMostOccurances = "";
        eventWithLeastOccurances = "";
        uniqueEventOccurrances = 0;
        //Put code in here to erase data in the list but not the list itself


        /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
        /// Now that data has been reset, clear player prefs of their values also
        saveData();
    }



    //Score vars
    private string lastAction = "";
    //Score functions
    public void addWin(int addAmount)
    {
        wins += addAmount;

        if (lastAction == "win")
        {
            winStreak += addAmount; // Increase win streak
        }
        else
        {
            winStreak = addAmount; // Reset win streak to the current addition
            loseStreak = 0;        // Reset lose streak
        }

        lastAction = "win"; // Update last action
    }

    public void addLosses(int addAmount)
    {
        losses += addAmount;

        if (lastAction == "loss")
        {
            loseStreak += addAmount; // Increase lose streak
        }
        else
        {
            loseStreak = addAmount; // Reset lose streak to the current addition
            winStreak = 0;          // Reset win streak
        }

        lastAction = "loss"; // Update last action
    }

    public void addScore(int listElement, int addValue)
    {
        //Checks if the provided index is within the bounds of the list
        if (listElement >= 0 && listElement < scoreTypes.Count)
        {
            //Add the specified value to the existing value of the selected element
            scoreTypes[listElement].value += addValue;
        }
        else
        {
            //Handle cases where the index is out of bounds
            Debug.LogWarning("Invalid list element index. Please provide a valid index.");
        }
    }



    //Combat functions
    public void addDamageDealt(int addAmount)
    {
        damageDealt += addAmount;
    }
    public void addDamageReceived(int addAmount)
    {
        damageTaken += addAmount;
    }
    public void addEnemiesSlain(int addAmount)
    {
        enemiesSlain += addAmount;
    }
    public void addPlayerDeaths(int addAmount)
    {
        playerDeaths += addAmount;
    }



    //Event functions
    //Allowing addtion and subtraction of event occurances
    public void addOccurrences(int listElement, int addOccurrence)
    {
        //Checks if the provided index is within the bounds of the list
        if (listElement >= 0 && listElement < eventTypes.Count)
        {
            //Add the specified value to the existing value of the selected element
            eventTypes[listElement].value += addOccurrence;
        }
        else
        {
            //Handle cases where the index is out of bounds
            Debug.LogWarning("Invalid list element index. Please provide a valid index.");
        }
    }
}