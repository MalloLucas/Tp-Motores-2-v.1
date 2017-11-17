using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;


public class MenuBuilder : EditorWindow
{
    GameObject NewButton;
//asdasdasd
    GameObject NewTextButton;
    Canvas ActualCanvas;
    public GameObject newCanvas;
    Sprite PrefabSprite;
    public MonoScript functionButton;
    public Scripts _scripts;
    public string actualFuntion;
    public GameObject previewCanvas;
    public Canvas canvasToAdd;
    public Canvas canvasToStart;
    public Canvas canvasForImage;
    public Canvas canvasForText;
    public SceneAsset scene;
	public string namebutton;
    public string inText;
    public Font newFont;
    public int textSize;
    public Font newFont2;
    public int textSize2;
    public Sprite prefabImage;
    public Color color;
    Vector2 scrollPosition = Vector2.zero;
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



        GUILayout.BeginArea(new Rect(0, 0, 800, 600));
        GUILayout.BeginVertical();

        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true, GUILayout.Width(800), GUILayout.MinHeight(600), GUILayout.MaxHeight(500), GUILayout.ExpandHeight(true));


        maxSize = new Vector2(800, 800);
        minSize = new Vector2(200,200);

        GUIStyle TitleStyle = new GUIStyle();
        TitleStyle.fontSize = 20;
        TitleStyle.alignment = TextAnchor.MiddleCenter;
        TitleStyle.fontStyle = FontStyle.Bold;

        GUIStyle TitleStyle2 = new GUIStyle();
        TitleStyle2.fontSize = 30;
        TitleStyle2.alignment = TextAnchor.MiddleCenter;
        TitleStyle2.fontStyle = FontStyle.Bold;


        GUIStyle Arial = new GUIStyle();
        Arial.fontSize = 10;
        Arial.alignment = TextAnchor.MiddleCenter;
        Arial.fontStyle = FontStyle.Normal;

        EditorGUILayout.LabelField("Menu Builder", TitleStyle2);

        EditorGUILayout.LabelField("Create a New Button", TitleStyle);

        EditorGUILayout.LabelField("Actual Function in Button: " + actualFuntion, EditorStyles.boldLabel);

        _scripts = (Scripts)EditorGUILayout.EnumPopup("Function to Add :", _scripts);

        if (_scripts == Scripts.ChangeScene)
        {
            EditorGUILayout.LabelField("Scene to Change: ", EditorStyles.boldLabel);
            scene = (SceneAsset)EditorGUILayout.ObjectField("", scene, typeof(SceneAsset), true );
        }


        if (_scripts == Scripts.ActivateCanvas) EditorGUILayout.LabelField("Canvas to Activate: ", EditorStyles.boldLabel);

        if (_scripts == Scripts.ActivateCanvas) canvasToAdd = (Canvas)EditorGUILayout.ObjectField("", canvasToAdd, typeof(Canvas), true);

        EditorGUILayout.LabelField("Button's Sprite", EditorStyles.boldLabel);
        GUILayout.BeginVertical();
        PrefabSprite = (Sprite)EditorGUILayout.ObjectField("", PrefabSprite, typeof(Sprite), true);
        ActualCanvas = FindObjectOfType<Canvas>();
        GUILayout.EndVertical();
        if (_scripts == Scripts.ChangeScene) actualFuntion = "ChangeScene";
        if (_scripts == Scripts.Pausa) actualFuntion = "Pause";
        if (_scripts == Scripts.ActivateCanvas) actualFuntion = "Activate Canvas";              

        EditorGUILayout.LabelField("Canvas Parent: " , EditorStyles.boldLabel);
        
        canvasToStart = (Canvas)EditorGUILayout.ObjectField("", canvasToStart, typeof(Canvas), true);

		EditorGUILayout.LabelField("Button's name" , EditorStyles.boldLabel);

		namebutton = (string)EditorGUILayout.TextField ("", namebutton);

        EditorGUILayout.LabelField("Button's Font", EditorStyles.boldLabel);
        textSize = EditorGUILayout.IntField("Text Size: ", textSize);
        newFont = (Font)EditorGUILayout.ObjectField("", newFont, typeof(Font), true);

         if (newFont == null) newFont = EditorStyles.standardFont ;
        
        if (textSize <= 0) textSize = 10;

            if (canvasToStart == null)
        {
            EditorGUILayout.HelpBox("To create a Button you must have a Canvas Parent", MessageType.Error);
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
            NewTextButton.GetComponent<Text>().fontSize = textSize;
            NewTextButton.GetComponent<Text>().font = newFont;
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
                NewButton.AddComponent<ChangeScene>().scena = scene.name;

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

		EditorGUILayout.LabelField("Create a Prefab Menu" , TitleStyle);

        previewCanvas = (GameObject)EditorGUILayout.ObjectField("", previewCanvas, typeof(GameObject), true);


        if (previewCanvas == null || previewCanvas.GetComponent<Canvas>() == null)
        {
           
            EditorGUILayout.HelpBox("There's not a prefab or the prefab has not contain a CanvasObject", MessageType.Error);
            GUI.enabled = false;
        }

        if (GUILayout.Button("Build Prefab Menu"))
        {
            Instantiate(previewCanvas);
        }

        GUI.enabled = true;

        EditorGUILayout.LabelField("Create a New Canvas", TitleStyle);

        if (GUILayout.Button("Create New Canvas"))
        {
            newCanvas = new GameObject("Canvas");
            newCanvas.AddComponent<Canvas>();
            newCanvas.AddComponent<GraphicRaycaster>();
            newCanvas.AddComponent<CanvasScaler>();
        }

        EditorGUILayout.LabelField("Create a New Image", TitleStyle);

        EditorGUILayout.LabelField("Canvas for Image: ", EditorStyles.boldLabel);

        canvasForImage = (Canvas)EditorGUILayout.ObjectField("", canvasForImage, typeof(Canvas), true);


        EditorGUILayout.LabelField("Prefab Image: ", EditorStyles.boldLabel);

        prefabImage = (Sprite)EditorGUILayout.ObjectField("", prefabImage, typeof(Sprite), true);

        if (canvasForImage == null)
        {

            EditorGUILayout.HelpBox("There´s not a Canvas for Image", MessageType.Error);
            GUI.enabled = false;
        }

        if (GUILayout.Button("Create New Image"))
        {
            GameObject image = new GameObject("Image");
            image.AddComponent<RectTransform>();
            image.AddComponent<Image>();
            image.AddComponent<CanvasRenderer>();
            if (prefabImage != null) image.GetComponent<Image>().sprite = prefabImage;
            image.transform.SetParent(canvasForImage.transform);
            image.transform.position = canvasForImage.transform.position;

        }

        GUI.enabled = true;

        EditorGUILayout.LabelField("Create a New Text", TitleStyle);

        EditorGUILayout.LabelField("Canvas Parent: ", EditorStyles.boldLabel);

        canvasForText = (Canvas)EditorGUILayout.ObjectField("", canvasForText, typeof(Canvas), true);

        EditorGUILayout.LabelField("Text Font", EditorStyles.boldLabel);
        textSize2 = EditorGUILayout.IntField("Text Size: ", textSize2);
        newFont2 = (Font)EditorGUILayout.ObjectField("", newFont2, typeof(Font), true);

        EditorGUILayout.LabelField("Text's Content", EditorStyles.boldLabel);

        inText = (string)EditorGUILayout.TextField("", inText);

        EditorGUILayout.LabelField("Text's Colour", EditorStyles.boldLabel);

        color = (Color)EditorGUILayout.ColorField("", color);

        if (newFont2 == null) newFont2 = EditorStyles.standardFont;

        if (textSize2 <= 0) textSize2 = 10;

        if (inText == null) inText = "New Text";

        if (canvasForText == null)
        {

            EditorGUILayout.HelpBox("There´s not a Canvas for Text", MessageType.Error);
            GUI.enabled = false;
        }

        if (GUILayout.Button("Create New Text"))
        {
            GameObject text = new GameObject("Text");
            text.AddComponent<RectTransform>();
            text.AddComponent<Text>();
            text.AddComponent<CanvasRenderer>();
            text.GetComponent<Text>().font = newFont2;
            text.GetComponent<Text>().fontSize = textSize2;
            text.GetComponent<Text>().text = inText;
            text.GetComponent<Text>().color = color;
            text.transform.SetParent(canvasForText.transform);
            text.transform.position = canvasForText.transform.position;

        }
        GUI.enabled = true;

        GUILayout.EndScrollView();
        GUILayout.EndVertical();
        GUILayout.EndArea();


    }

   
}