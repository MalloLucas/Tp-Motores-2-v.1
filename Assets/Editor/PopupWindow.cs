using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class PopupWindow : EditorWindow
{
    public Rect window;
    public string name;

    GameObject _button;
    GameObject _text;
    Canvas myCanvas;

    public PopupWindow(Rect _window,string myName)
    {
        window = _window;
        name = myName;
    }

    public void inst(int id)
    {

        myCanvas = FindObjectOfType<Canvas>();

        if (GUI.Button(new Rect(5, 20, this.window.size.x - 20, this.window.size.y -20), "DrawButton"))
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

            _button.transform.parent = myCanvas.transform;
            _text.transform.parent = _button.transform;

            _button.transform.localScale = new Vector3(this.window.size.x / 100, this.window.size.y / 100, 0);
            _button.transform.position = new Vector3(this.position.x, this.position.y, 0);


            Debug.Log(this.position);

        }
    }

}
