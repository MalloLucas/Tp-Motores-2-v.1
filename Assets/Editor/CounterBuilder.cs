using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class CounterBuilder : EditorWindow {


    float timeToRestart = 0;
    int fontSize = 0;
    string scene;
    SceneAsset sceneToChange;
    bool canCreateCounter = true;

    Font fontStyle;
    GameObject displayedText;
    Color fontColor;

    [MenuItem("Editor Windows/Counter Builder")]
    static void CreateWindow()
    {
        CounterBuilder counterBuilder = ((CounterBuilder)GetWindow(typeof(CounterBuilder)));
        counterBuilder.Show();   

    }
	
	void OnGUI () {

        var newFont = EditorStyles.largeLabel;
        newFont.font = fontStyle;

        Canvas canvas = FindObjectOfType<Canvas>();

        minSize = new Vector2(800, 400);
        maxSize = new Vector2(800, 400);

        EditorGUILayout.BeginVertical();

        EditorGUILayout.HelpBox("In this window you can define a counter to restart to main menu, for example, if player had lose the game", 
            MessageType.Info);

        EditorGUILayout.EndVertical();

        GUILayout.Space(20);

        if (timeToRestart <= 0)
        {

            EditorGUILayout.HelpBox("Time to restart cannot be 0 or less",
                MessageType.Warning);

        }

        if (fontSize <= 0)
        {

            EditorGUILayout.HelpBox("Font size cannot be 0 or less",
                MessageType.Warning);

        }

        EditorGUILayout.BeginHorizontal();
        
        timeToRestart = EditorGUILayout.FloatField("Time to restart", timeToRestart);

        fontSize = EditorGUILayout.IntField("Font Size int Screen", fontSize);

        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10);

        if (fontStyle == null)
        {

            EditorGUILayout.HelpBox("Font size cannot be null",
                MessageType.Warning);

        }

        EditorGUILayout.BeginHorizontal();

        fontStyle = (Font)EditorGUILayout.ObjectField("Font Style for Timer", fontStyle, typeof(Font), true);
        fontColor.a = 255;
        fontColor = EditorGUILayout.ColorField("Font Color", fontColor);

        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10);

        if (sceneToChange == null)
        {

            EditorGUILayout.HelpBox("Scene cannot be null",
                MessageType.Warning);

        }

        sceneToChange = (SceneAsset)EditorGUILayout.ObjectField("Scene to Load", sceneToChange, typeof(SceneAsset), true);

        GUILayout.Space(15);

        GUILayout.Label("This is a Test Counter: 0, 1, 2, 3, 4, 5, 6, 7, 8, 9", newFont);
        GUI.skin.font = default(Font);

        GUILayout.Space(15);

        if (GUILayout.Button("Add Counter to Scene"))
        {
            if (fontSize <= 0 || timeToRestart <= 0 || sceneToChange == null || fontStyle == null)
            {
                canCreateCounter = false;
            }
            else
            {
                canCreateCounter = true;

                displayedText = new GameObject("Text");
                displayedText.AddComponent<Text>();
                displayedText.GetComponent<Text>().text = timeToRestart.ToString();
                displayedText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
                displayedText.GetComponent<RectTransform>().sizeDelta = new Vector2(350, 200);
                displayedText.GetComponent<Text>().font = fontStyle;
                displayedText.GetComponent<Text>().fontSize = fontSize;
                displayedText.GetComponent<Text>().color = fontColor;

                displayedText.AddComponent<Counter>();
                displayedText.GetComponent<Counter>().sceneToChange = sceneToChange.name;

                displayedText.GetComponent<RectTransform>().position = FindObjectOfType<Canvas>().GetComponent<Transform>().position;

                displayedText.transform.parent = FindObjectOfType<Canvas>().GetComponent<Transform>();
            }

        }


        if (!canCreateCounter)
        {
            EditorGUILayout.HelpBox("You must complete all fields to create the counter",
                MessageType.Error);
        }

    }
}
