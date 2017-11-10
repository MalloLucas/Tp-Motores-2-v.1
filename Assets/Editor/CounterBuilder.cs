using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class CounterBuilder : EditorWindow {


    float timeToRestart = 0;
    int fontSize = 0;
    string scene;


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

        EditorGUILayout.BeginHorizontal();

        timeToRestart = EditorGUILayout.FloatField("Time to restart", timeToRestart);
        fontSize = EditorGUILayout.IntField("Font Size int Screen", fontSize);

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        fontStyle = (Font)EditorGUILayout.ObjectField("Font Style for Timer", fontStyle, typeof(Font), true);
        fontColor.a = 255;
        fontColor = EditorGUILayout.ColorField("Font Color", fontColor);

        EditorGUILayout.EndHorizontal();

        GUILayout.Space(15);

        GUILayout.Label("This is a Test Counter: 0, 1, 2, 3, 4, 5, 6, 7, 8, 9", newFont);
        GUI.skin.font = default(Font);

        GUILayout.Space(50);


        scene = EditorGUILayout.TextField("Scene to restart", scene);

        GUILayout.Space(15);

        if (GUILayout.Button("Add Counter to Scene"))
        {
            displayedText = new GameObject("Text");
            displayedText.AddComponent<Text>();
            displayedText.GetComponent<Text>().text = timeToRestart.ToString();
            displayedText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            displayedText.GetComponent<RectTransform>().sizeDelta = new Vector2(350, 200);
            displayedText.GetComponent<Text>().font = fontStyle;
            displayedText.GetComponent<Text>().fontSize = fontSize;
            displayedText.GetComponent<Text>().color = fontColor;

            displayedText.AddComponent<Counter>();
            displayedText.GetComponent<Counter>().sceneToChange = scene;

            Vector3 textPosition = new Vector3 (FindObjectOfType<Canvas>().GetComponent<Transform>().position.x, 400, 
                FindObjectOfType<Canvas>().GetComponent<Transform>().position.z);

            displayedText.transform.position = textPosition; 

            displayedText.transform.parent = FindObjectOfType<Canvas>().GetComponent<Transform>();

        }



    }
}
