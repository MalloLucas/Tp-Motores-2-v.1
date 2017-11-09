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
    public MonoScript functionButton;
    public Scripts _scripts;
    public string actualFuntion;
    public Canvas previewCanvas;
    public Canvas canvasToAdd;
    public Canvas canvasToStart;
    public int scene;
    public enum Scripts
    {
        ChangeScene,
        Pausa,
        ActivateCanvas
    }
   

    [MenuItem("Editor Windows/MenuBuilder")]
    static void CreateWindow()
    {
        ((MenuBuilder)GetWindow(typeof(MenuBuilder))).Show();
    }


    void OnGUI()
    {

        PrefabSprite = (Sprite)EditorGUILayout.ObjectField("Prefab Button Image", PrefabSprite, typeof(Sprite), true);
        ActualCanvas = FindObjectOfType<Canvas>();

        if (_scripts == Scripts.ChangeScene) actualFuntion = "ChangeScene";
        if (_scripts == Scripts.Pausa) actualFuntion = "Pause";
        if (_scripts == Scripts.ActivateCanvas) actualFuntion = "Activate Canvas";
            
        
        EditorGUILayout.LabelField("Actual Function in Button: " + actualFuntion, EditorStyles.boldLabel);

        _scripts = (Scripts)EditorGUILayout.EnumPopup("Function to Add :", _scripts);

        if (_scripts == Scripts.ActivateCanvas) EditorGUILayout.LabelField("Canvas to Activate: " + actualFuntion, EditorStyles.boldLabel);

        if (_scripts == Scripts.ActivateCanvas) canvasToAdd = (Canvas)EditorGUILayout.ObjectField("", canvasToAdd, typeof(Canvas), true);

        EditorGUILayout.LabelField("Canvas Parent: " + actualFuntion, EditorStyles.boldLabel);

         canvasToStart = (Canvas)EditorGUILayout.ObjectField("", canvasToStart, typeof(Canvas), true);

        EditorGUILayout.LabelField("Scene to Change: " + actualFuntion, EditorStyles.boldLabel);

        if (_scripts == Scripts.ChangeScene) scene = (int)EditorGUILayout.IntField("" , scene);

        if (canvasToStart == null)
        {
            EditorGUILayout.HelpBox("To create a Button you must habe a Canvas Parent", MessageType.Error);
            GUI.enabled = false;
        }

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

            if (_scripts == Scripts.ChangeScene) {

                NewTextButton.GetComponent<Text>().text = "Change Scene To: ";
                NewButton.transform.SetParent(canvasToStart.transform);
                NewButton.AddComponent<ChangeScene>();

            }
            if (_scripts == Scripts.Pausa)
            {
                NewTextButton.GetComponent<Text>().text = "Pause";
                NewButton.transform.parent = canvasToStart.transform;
                NewButton.AddComponent<Pause>();
            }

            if (_scripts == Scripts.ActivateCanvas)
            {
                NewTextButton.GetComponent<Text>().text = "Activate Canvas: " + ActualCanvas;
                NewButton.transform.parent = canvasToStart.transform;
                NewButton.AddComponent<ActivateCanvas>();
                NewButton.GetComponent<ActivateCanvas>().ActualCanvas = canvasToAdd;
            }
           

            NewButton.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
            NewTextButton.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
            NewTextButton.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;

            NewButton.transform.parent = canvasToStart.transform;
            NewButton.transform.position = ActualCanvas.transform.position;
            NewButton.GetComponent<Image>().sprite = PrefabSprite;

        }

        GUI.enabled = true;

        previewCanvas = (Canvas)EditorGUILayout.ObjectField("", previewCanvas, typeof(Canvas), true);


        if (previewCanvas == null)
        {
           
                EditorGUILayout.HelpBox("You can create your Actual Canvas", MessageType.Error);
            GUI.enabled = false;
        }

        if (GUILayout.Button("Build Prefab Menu"))
        {
            Instantiate(previewCanvas);
        }

        GUI.enabled = true;

    }

   
}