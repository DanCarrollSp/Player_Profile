using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(PlayerProfile))]
public class PlayerProfileEditor : Editor
{
    // Basic information
    private SerializedProperty playerNameProp;


    //Time stats
    private SerializedProperty showTimeStats;// Foldout toggle
    private SerializedProperty enableTimeStatTracking;//Enable checkbox
    private SerializedProperty totalTimePlayed;
    private SerializedProperty currentSessionLenght;
    private SerializedProperty previousSessionLenght;

    //Score stats
    private SerializedProperty showScoreStats;// Foldout toggle
    private SerializedProperty enableScoreStatTracking;//Enable checkbox
    private SerializedProperty wins;
    private SerializedProperty losses;
    private SerializedProperty winStreak;
    private SerializedProperty loseStreak;
    private ReorderableList scoreList;

    //Combat stats
    private SerializedProperty showCombatStats; // Foldout toggle
    private SerializedProperty enableCombatStatTracking;//Enable checkbox
    private SerializedProperty damageDealt;
    private SerializedProperty damageTaken;
    private SerializedProperty enemiesSlain;
    private SerializedProperty playerDeaths;

    //Distance stats:
    private SerializedProperty showDistanceStats; // Foldout toggle
    private SerializedProperty enableDistanceStatTracking;//Enable checkbox
    private SerializedProperty totalDistanceTravelled;
    private SerializedProperty currentSessionTravelAmount;
    private SerializedProperty previousSessionTravelAmount;

    //Event stats:
    private SerializedProperty showEventStats; // Foldout toggle
    private SerializedProperty enableEventStatTracking;//Enable checkbox
    private SerializedProperty uniqueEventOcurrances;
    private SerializedProperty totalEventOccurrences;
    private SerializedProperty eventWithMostOccurances;
    private SerializedProperty eventWithLeastOccurances;
    private ReorderableList eventList;


    //Component configure options
    private SerializedProperty showConfigureOptions; // Foldout toggle
    private SerializedProperty eraseProfileDataOnNextPlay;
    private SerializedProperty saveProfileData;
    private SerializedProperty saveInterval;



    //Cache serialized properties
    private void OnEnable()
    {
        //Basic information
        playerNameProp = serializedObject.FindProperty("playerName");



        //Time stats:
        enableTimeStatTracking = serializedObject.FindProperty("enableTimeStatTracking");
        showTimeStats = serializedObject.FindProperty("showTimeStats");
        totalTimePlayed = serializedObject.FindProperty("totalTimePlayed");
        currentSessionLenght = serializedObject.FindProperty("currentSessionLenght");
        previousSessionLenght = serializedObject.FindProperty("previousSessionLenght");



        //Score stats:
        enableScoreStatTracking = serializedObject.FindProperty("enableScoreStatTracking");
        showScoreStats = serializedObject.FindProperty("showScoreStats");
        wins = serializedObject.FindProperty("wins");
        losses = serializedObject.FindProperty("losses");
        winStreak = serializedObject.FindProperty("winStreak");
        loseStreak = serializedObject.FindProperty("loseStreak");
        //
        scoreList = new ReorderableList(serializedObject, serializedObject.FindProperty("scoreTypes"), true, true, true, true);
        //
        scoreList.drawHeaderCallback = (Rect rect) => EditorGUI.LabelField(rect, "Score type:                                                           Value:");
        scoreList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            SerializedProperty element = scoreList.serializedProperty.GetArrayElementAtIndex(index);
            SerializedProperty name = element.FindPropertyRelative("name");
            SerializedProperty value = element.FindPropertyRelative("value");

            float fieldWidth = rect.width / 2 - 10;
            //Draw the fields
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, fieldWidth, EditorGUIUtility.singleLineHeight), name, GUIContent.none);
            EditorGUI.PropertyField(new Rect(rect.x + fieldWidth + 5, rect.y, fieldWidth, EditorGUIUtility.singleLineHeight), value, GUIContent.none);
        };



        //Combat stats:
        showCombatStats = serializedObject.FindProperty("showCombatStats"); ; // Foldout toggle
        enableCombatStatTracking = serializedObject.FindProperty("enableCombatStatTracking");
        damageDealt = serializedObject.FindProperty("damageDealt");
        damageTaken = serializedObject.FindProperty("damageTaken");
        enemiesSlain = serializedObject.FindProperty("enemiesSlain");
        playerDeaths = serializedObject.FindProperty("playerDeaths");



        //Distance stats:
        enableDistanceStatTracking = serializedObject.FindProperty("enableDistanceStatTracking");//Enable checkbox
        showDistanceStats = serializedObject.FindProperty("showDistanceStats"); // Foldout toggle
        totalDistanceTravelled = serializedObject.FindProperty("totalDistanceTravelled");
        currentSessionTravelAmount = serializedObject.FindProperty("currentSessionTravelAmount");
        previousSessionTravelAmount = serializedObject.FindProperty("previousSessionTravelAmount");



        //Event stats:
        enableEventStatTracking = serializedObject.FindProperty("enableEventStatTracking");
        showEventStats = serializedObject.FindProperty("showEventStats"); // Foldout toggle
        totalEventOccurrences = serializedObject.FindProperty("totalEventOccurrences");
        uniqueEventOcurrances = serializedObject.FindProperty("uniqueEventOcurrances");
        eventWithMostOccurances = serializedObject.FindProperty("eventWithMostOccurances");
        eventWithLeastOccurances = serializedObject.FindProperty("eventWithLeastOccurances");
        //
        eventList = new ReorderableList(serializedObject, serializedObject.FindProperty("eventTypes"), true, true, true, true);
        //
        eventList.drawHeaderCallback = (Rect rect) => EditorGUI.LabelField(rect, "Event type:                                                           Occurances:");
        eventList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            SerializedProperty element = eventList.serializedProperty.GetArrayElementAtIndex(index);
            SerializedProperty name = element.FindPropertyRelative("name");
            SerializedProperty value = element.FindPropertyRelative("value");

            float fieldWidth = rect.width / 2 - 10;
            //Draw the fields
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, fieldWidth, EditorGUIUtility.singleLineHeight), name, GUIContent.none);
            EditorGUI.PropertyField(new Rect(rect.x + fieldWidth + 5, rect.y, fieldWidth, EditorGUIUtility.singleLineHeight), value, GUIContent.none);


        };



        //Component configure options
        showConfigureOptions = serializedObject.FindProperty("showConfigureOptions");
        eraseProfileDataOnNextPlay = serializedObject.FindProperty("eraseProfileDataOnNextPlay");
        saveProfileData = serializedObject.FindProperty("saveProfileData");
        saveInterval = serializedObject.FindProperty("saveInterval");

    }






    //Draw and update
    public override void OnInspectorGUI()
    {
        //Update the serialized object
        serializedObject.Update();



        //Basic information (player name)
        EditorGUILayout.PropertyField(playerNameProp);
        EditorGUILayout.Space(5);


        //Draw time stats
        showTimeStats.boolValue = EditorGUILayout.Foldout(showTimeStats.boolValue, "Time Stats", true);
        if (showTimeStats.boolValue)
        {
            EditorGUILayout.PropertyField(enableTimeStatTracking);
            if (enableTimeStatTracking.boolValue == true)
            {
                EditorGUI.indentLevel++;

                EditorGUILayout.Space(5);
                EditorGUILayout.PropertyField(totalTimePlayed);
                EditorGUILayout.PropertyField(currentSessionLenght);
                EditorGUILayout.PropertyField(previousSessionLenght);
                EditorGUILayout.Space(5);

                EditorGUI.indentLevel--;
            }
        }


        //Draw Score stats
        showScoreStats.boolValue = EditorGUILayout.Foldout(showScoreStats.boolValue, "Score Stats", true);
        if (showScoreStats.boolValue)
        {
            EditorGUILayout.PropertyField(enableScoreStatTracking);
            if (enableScoreStatTracking.boolValue == true)
            {
                EditorGUI.indentLevel++;

                EditorGUILayout.PropertyField(wins);
                EditorGUILayout.PropertyField(losses);
                EditorGUILayout.PropertyField(winStreak);
                EditorGUILayout.PropertyField(loseStreak);
                EditorGUILayout.Space(5);

                scoreList.DoLayoutList();


                EditorGUI.indentLevel--;
            }
        }


        //Draw Distance stats
        showDistanceStats.boolValue = EditorGUILayout.Foldout(showDistanceStats.boolValue, "Distance Stats", true);
        if (showDistanceStats.boolValue)
        {
            EditorGUILayout.PropertyField(enableDistanceStatTracking);
            if (enableDistanceStatTracking.boolValue == true)
            {
                EditorGUI.indentLevel++;

                EditorGUILayout.PropertyField(totalDistanceTravelled);
                EditorGUILayout.PropertyField(currentSessionTravelAmount);
                EditorGUILayout.PropertyField(previousSessionTravelAmount);
                EditorGUILayout.Space(5);


                EditorGUI.indentLevel--;
            }
        }


        //Draw combat stats
        showCombatStats.boolValue = EditorGUILayout.Foldout(showCombatStats.boolValue, "Combat Stats", true);
        if (showCombatStats.boolValue)
        {
            EditorGUILayout.PropertyField(enableCombatStatTracking);
            if (enableCombatStatTracking.boolValue == true)
            {
                EditorGUI.indentLevel++;


                EditorGUILayout.PropertyField(damageDealt);
                EditorGUILayout.PropertyField(damageTaken);


                EditorGUILayout.Space(5);

                EditorGUILayout.PropertyField(enemiesSlain);
                EditorGUILayout.PropertyField(playerDeaths);


                EditorGUI.indentLevel--;
            }
        }


        //Draw Event stats
        showEventStats.boolValue = EditorGUILayout.Foldout(showEventStats.boolValue, "Event Stats", true);
        if (showEventStats.boolValue)
        {
            EditorGUILayout.PropertyField(enableEventStatTracking);
            if (enableEventStatTracking.boolValue == true)
            {
                EditorGUI.indentLevel++;

                EditorGUILayout.PropertyField(totalEventOccurrences);
                EditorGUILayout.PropertyField(eventWithMostOccurances);
                EditorGUILayout.PropertyField(eventWithLeastOccurances);
                EditorGUILayout.PropertyField(uniqueEventOcurrances);
                EditorGUILayout.Space(5);

                eventList.DoLayoutList();


                EditorGUI.indentLevel--;
            }
        }


        //Component configure options
        EditorGUILayout.Space(10);
        showConfigureOptions.boolValue = EditorGUILayout.Foldout(showConfigureOptions.boolValue, "Configure Profile Data", true);
        if (showConfigureOptions.boolValue)
        {
            EditorGUILayout.PropertyField(eraseProfileDataOnNextPlay);
            EditorGUILayout.PropertyField(saveProfileData);
            EditorGUILayout.PropertyField(saveInterval);
        }


        //Apply modified properties
        serializedObject.ApplyModifiedProperties();
    }
}