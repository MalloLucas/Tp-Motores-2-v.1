using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;


public class MenuBuilder : EditorWindow
{
   GameObject NewButton;
   GameObject NewTextButton;
   Canvas ActualCanvas;
   Sprite PrefabSprite;
    bool CustomSize;
    
   

    [MenuItem("Editor Windows/MenuBuilder")]
    static void CreateWindow()
    {
        ((MenuBuilder)GetWindow(typeof(MenuBuilder))).Show();
    }

    void OnGUI()
    {
        PrefabSprite = (Sprite)EditorGUILayout.ObjectField("Prefab Button Image", PrefabSprite, typeof(Sprite), true);
        ActualCanvas = FindObjectOfType<Canvas>();


        if (GUILayout.Button("Build Button"))
        {

            NewButton = new GameObject("Button");
            NewButton.AddComponent<RectTransform>();
            NewButton.AddComponent<CanvasRenderer>();
            NewButton.AddComponent<Image>();
            NewButton.AddComponent<Button>();
            NewButton.GetComponent<Image>().preserveAspect = true;
            NewButton.GetComponent<RectTransform>().position = new Vector3(0, 0, 0);

            NewTextButton = new GameObject("Text");
            NewTextButton.AddComponent<RectTransform>();
            NewTextButton.AddComponent<CanvasRenderer>();
            NewTextButton.AddComponent<Text>();
            NewTextButton.GetComponent<Text>().text = "New Text";
            NewTextButton.GetComponent<Text>().color = Color.black;



            NewButton.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
            NewTextButton.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
            NewTextButton.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;

            NewTextButton.transform.parent = NewButton.transform;
            NewButton.transform.parent = ActualCanvas.transform;

            NewButton.GetComponent<Image>().sprite = PrefabSprite;
            
        }
    }
}