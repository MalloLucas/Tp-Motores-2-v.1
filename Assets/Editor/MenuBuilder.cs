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
	public string namebutton;
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

        if (_scripts == Scripts.ActivateCanvas) EditorGUILayout.LabelField("Canvas to Activate: " , EditorStyles.boldLabel);

        if (_scripts == Scripts.ActivateCanvas) canvasToAdd = (Canvas)EditorGUILayout.ObjectField("", canvasToAdd, typeof(Canvas), true);

        EditorGUILayout.LabelField("Canvas Parent: " , EditorStyles.boldLabel);

        canvasToStart = (Canvas)EditorGUILayout.ObjectField("", canvasToStart, typeof(Canvas), true);

		EditorGUILayout.LabelField("Button's name" , EditorStyles.boldLabel);

		namebutton = (string)EditorGUILayout.TextField ("", namebutton);				 

		if (_scripts == Scripts.ChangeScene) {
			EditorGUILayout.LabelField("Scene to Change: " , EditorStyles.boldLabel);
			scene = (int)EditorGUILayout.IntField ("", scene);
		}

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
            NewTextButton.GetComponent<Text>().color = Color.black;
			NewTextButton.transform.SetParent (NewButton.transform);

            if (_scripts == Scripts.ChangeScene) {

				if (namebutton == null){
					NewTextButton.GetComponent<Text> ().text = "Change Scene";
				}
				else{
					NewTextButton.GetComponent<Text>().text = namebutton;
				}
                NewButton.transform.SetParent(canvasToStart.transform);
                NewButton.AddComponent<ChangeScene>();

            }
            if (_scripts == Scripts.Pausa)
            {
				if (namebutton == null){
					NewTextButton.GetComponent<Text> ().text = "Pause";
				}
				else{
					NewTextButton.GetComponent<Text>().text = namebutton;
				}
                NewButton.transform.parent = canvasToStart.transform;

                NewButton.AddComponent<Pause>();
            }

            if (_scripts == Scripts.ActivateCanvas)
            {
				if (namebutton == null){
					NewTextButton.GetComponent<Text> ().text = "ActivateCanvas";
				}
				else{
					NewTextButton.GetComponent<Text>().text = namebutton;
				}
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

		EditorGUILayout.LabelField("Create a prefab menu" , EditorStyles.boldLabel);

        previewCanvas = (Canvas)EditorGUILayout.ObjectField("", previewCanvas, typeof(Canvas), true);


        if (previewCanvas == null)
        {
           
            EditorGUILayout.HelpBox("There's not a prefab", MessageType.Error);
            GUI.enabled = false;
        }


        if (GUILayout.Button("Build Prefab Menu"))
        {
            Instantiate(previewCanvas);
        }

        GUI.enabled = true;

    }

   
}