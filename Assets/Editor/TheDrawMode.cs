using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class TheDrawMode : EditorWindow
{

    List<PopupWindow> myRects = new List<PopupWindow>();

    Vector2 startPoint;
    bool isDrawing;

    bool drawMode;

    GameObject _button;
    GameObject _text;
    Canvas myCanvas;

    PopupWindow myWindow;
    string myName = "PopUpWindow";

    Sprite sp;


    [MenuItem("Editor Windows/Draw")]
    public static void DrawWindow()
    {
        GetWindow<TheDrawMode>().Show();
    }

    private void OnGUI()
    {

        myCanvas = FindObjectOfType<Canvas>();

        drawMode = EditorGUILayout.Toggle("Draw", drawMode);

        sp = (Sprite)Resources.Load("button") as Sprite;


        var canvasSize = myCanvas.GetComponent<RectTransform>().sizeDelta;
        maxSize = new Vector2(canvasSize.x, 484);
        minSize = new Vector2(canvasSize.x, 484);

        

        if (drawMode)
        {
            if (Event.current.type == EventType.mouseDown && Event.current.button == 0)
            {

                if (myRects.Count > 0) { myRects.Clear(); }
                if (myRects.Count == 0)
                {
                    isDrawing = true;
                    startPoint = Event.current.mousePosition;

                    startPoint.x = startPoint.x - startPoint.x % 10;
                    startPoint.y = startPoint.y - startPoint.y % 10;

                    Repaint();
                }
            }

            var mousePos = Event.current.mousePosition;

            mousePos.x = mousePos.x - mousePos.x % 10;
            mousePos.y = mousePos.y - mousePos.y % 10;

            var currentRect = new Rect(startPoint, mousePos - startPoint);

            if (isDrawing)
            {
                Handles.DrawSolidRectangleWithOutline(currentRect, Color.red, Color.black);
                Repaint();
            }

            if (Event.current.type == EventType.MouseUp && Event.current.button == 0)
            {

                myWindow = new PopupWindow(currentRect, myName);
                myRects.Add(myWindow);
                isDrawing = false;
                Debug.Log(currentRect);
            }
        }

        BeginWindows();
        foreach (var item in myRects)
        {
            if (myRects != null)
            {
                item.window = GUI.Window(0, item.window, inst, item.name);
                Repaint();

            }
        }
        EndWindows();
    }

    public void inst(int id)
    {
        if (GUI.Button(new Rect(5, 20, myWindow.window.size.x - 20, myWindow.window.size.y - 20), "DrawButton"))
        {
            _button = new GameObject("Button");
            _button.AddComponent<RectTransform>();
            _button.AddComponent<CanvasRenderer>();
            _button.AddComponent<Image>();
            _button.AddComponent<Button>();
            _button.GetComponent<Image>().preserveAspect = true;
            //_button.GetComponent<RectTransform>().position = new Vector3(0, 0, 0);

            _text = new GameObject("Text");
            _text.AddComponent<RectTransform>();
            _text.AddComponent<CanvasRenderer>();
            _text.AddComponent<Text>();
            _text.GetComponent<Text>().text = "New Text";
            _text.GetComponent<Text>().color = Color.black;
            _text.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;

            _button.transform.parent = myCanvas.transform;
            _text.transform.parent = _button.transform;

            //_button.transform.localScale = new Vector3(myWindow.window.size.x / 100, myWindow.window.size.y / 100, 0);
            var buttonSize = _button.GetComponent<RectTransform>().sizeDelta;
            buttonSize = new Vector2(myWindow.window.width, myWindow.window.height);
            _button.GetComponent<RectTransform>().sizeDelta = buttonSize;
            //_button.transform.position = new Vector3((startPoint.x + myWindow.window.size.x / 2) , (484 + startPoint.y), 0);
            _button.transform.position = new Vector3(startPoint.x + buttonSize.x / 2, -startPoint.y + 450 - buttonSize.y / 2, 0);

            _button.GetComponent<Image>().sprite = sp;

            Debug.Log(startPoint);

        }
    }
}
