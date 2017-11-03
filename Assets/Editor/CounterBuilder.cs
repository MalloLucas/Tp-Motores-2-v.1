using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class CounterBuilder : EditorWindow {


    float timeToRestart = 0;
    public string demoText = "This is a Test Counter: 0, 1, 2, 3, 4, 5, 6, 7, 8, 9";
    Font fontStyle;

    [MenuItem("Editor Windows/Counter Builder")]
    static void CreateWindow()
    {
        CounterBuilder counterBuilder = ((CounterBuilder)GetWindow(typeof(CounterBuilder)));
        counterBuilder.Show();   

    }
	
	void OnGUI () {
		
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

        fontStyle = (Font)EditorGUILayout.ObjectField("Font Style for Timer", fontStyle, typeof(Font), true);

        EditorGUILayout.EndHorizontal();

        GUILayout.Space(15);
        
        EditorGUILayout.LabelField("This is a Test Counter: 0, 1, 2, 3, 4, 5, 6, 7, 8, 9", EditorStyles.largeLabel);


        if (GUILayout.Button("Add Counter to Scene"))
        {



        }



    }
}
