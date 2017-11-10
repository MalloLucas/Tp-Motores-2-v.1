using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class NewDrawMode : EditorWindow
{

    //public PopupWindow miniWindow;
    public Nodes node;

    public List<PopupWindow> miniWindow; 

    Vector2 startPoint;
    bool isDrawing;
    public Rect windowSize;
    string theName = "1";

    [MenuItem("Editor Windows/NewDrawMode")]
    static void CreateWindow()
    {
        NewDrawMode nDM = ((NewDrawMode)GetWindow(typeof(NewDrawMode)));
        nDM.Show();
    }

    private void OnGUI()
    {
        if (Event.current.type == EventType.mouseDown && Event.current.button == 0)
        {
            isDrawing = true;
            startPoint = Event.current.mousePosition;
            startPoint.x = startPoint.x - startPoint.x % 10;
            startPoint.y = startPoint.y - startPoint.y % 10;
            Repaint();
        }
        miniWindow = new List<PopupWindow>();
        //node.name = "Window1";

        var mPos = Event.current.mousePosition;
        mPos.x = mPos.x - mPos.x % 10;
        mPos.y = mPos.y - mPos.y % 10;
        //v wPos = startPoint, mPos -startPoint;

        windowSize = new Rect(startPoint, mPos - startPoint);

        //node.window.position = startPoint;
        //.window.size = mPos - startPoint;


        if (isDrawing)
        {

            create(windowSize, theName);
            Debug.Log(windowSize);
        }

        if(Event.current.type == EventType.MouseUp && Event.current.button == 0)
        {

            BeginWindows();
            if (miniWindow != null)
                foreach (var item in miniWindow)
                {
                    Debug.Log(miniWindow.Count);

                    item.window = GUI.Window(0, item.window, instWindow, item.name);
                }
            //miniWindow.window = GUI.Window(0, miniWindow.window, instWindow,"name");
            //node.window = GUI.Window(1, node.window, instWindow, node.name);
            EndWindows();
            miniWindow.Clear();
            isDrawing = false;
        }
    }

    private void instWindow(int id)
    {
        if (GUI.Button(new Rect(10, 10, 20, 20), "holis"))
        {
            Debug.Log("holi");
        }
    }

    private void create(Rect size, string name)
    {
        miniWindow.Add(new PopupWindow(size ,name));
    }
}
