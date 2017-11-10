using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class DrawMode : EditorWindow
{
    bool drawModeIsActive;
    GameObject _button;
    GameObject _text;
    Canvas myCanvas;
    Vector2 nodePos;
    Rect rect;

    private List<Nodes> myNodes;
    private string currentName;
    

    [MenuItem("Editor Windows/DrawMode")]
    static void CreateWindow()
    {      
        DrawMode dM = ((DrawMode)GetWindow(typeof(DrawMode)));
        dM.Show();
        dM.Initialization();
        
    }

    void OnGUI()
    {

        myCanvas = FindObjectOfType<Canvas>();
        maxSize = new Vector2(600, 400);
        //minSize = new Vector2(400, 300);
        rect = new Rect(50, 50, 50, 50);

        /*if (GUILayout.Button("Build Button"))
        {

           
        }*/

        drawModeIsActive = EditorGUILayout.Toggle("Draw Mode", drawModeIsActive);

        


        if(myNodes == null)
            myNodes = new List<Nodes>();

        EditorGUILayout.BeginHorizontal();
        currentName = EditorGUILayout.TextField("New Window", currentName);
        if (GUILayout.Button("Create"))
            CreateNodeWindow(currentName);
        EditorGUILayout.EndHorizontal();

        BeginWindows();
        for (int i = 0; i < myNodes.Count - 1; i++)
        {
            myNodes[i].window = GUI.Window(i, myNodes[i].window, InstNodeWindow, myNodes[i].name + i);
            nodePos = myNodes[i].window.position;

            //fhsarefhrtfgjfharjsdrthryj
            myNodes[i].window.width = 100;
            //dasgsrtjhsfaefhdfjhnfdghasdfgdrfg

            for (int j = myNodes[i].connections.Count - 1; j >= 0; j--)
            {
                CreateLine(myNodes[i].window, myNodes[i].connections[j].window);
            }
        }
        EndWindows();


        EditorGUILayout.LabelField("Begin Draw Area");
        rect = GUILayoutUtility.GetRect(541, 326);       
        EditorGUILayout.LabelField("End Draw Area");

        
        
    }

    private void Initialization()
    {
        myNodes = new List<Nodes>();
        
    }

    private void CreateNodeWindow(string name)
    {
        myNodes.Add(new Nodes(name));
    }

    private void InstNodeWindow(int id)
    {
        if (drawModeIsActive)
            GUI.DragWindow();

        //int i = EditorGUILayout.IntField("Add", -1);
        /*if (i >= 0 && i != id && i < myNodes.Count && !myNodes[id].connections.Contains(myNodes[i]))
        {
            myNodes[id].connections.Add(myNodes[i]);
        }*/

        if(GUILayout.Button("Create New Button"))
        {
            _button = new GameObject("Button");
            _button.AddComponent<RectTransform>();
            _button.AddComponent<CanvasRenderer>();
            _button.AddComponent<Image>();
            _button.AddComponent<Button>();
            _button.GetComponent<Image>().preserveAspect = true;
            _button.GetComponent<RectTransform>().position = new Vector3(0, 0, 0);

            _text = new GameObject("Text");
            _text.AddComponent<RectTransform>();
            _text.AddComponent<CanvasRenderer>();
            _text.AddComponent<Text>();
            _text.GetComponent<Text>().text = "New Text";
            _text.GetComponent<Text>().color = Color.black;

            _button.transform.parent = myCanvas.transform;
            _text.transform.parent = _button.transform;

            _button.transform.position = nodePos;
        }
    }

    private void CreateLine(Rect a, Rect b)
    {
        Handles.DrawLine(a.position, b.position);
    }

}
