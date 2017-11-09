using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateCanvas : MonoBehaviour {

    public Button yourButton;
    public Canvas ActualCanvas;
    public List<Canvas> listCanvas = new List<Canvas>();
    public List<Canvas> auxList = new List<Canvas>();
    // Use this for initialization
    void Start () {

        yourButton = this.GetComponent<Button>();
        yourButton.onClick.AddListener(ActivateTheCanvas);
        auxList.AddRange(FindObjectsOfType<Canvas>());
        foreach (var item in auxList)
        {
            if (item.GetComponent<Canvas>().renderMode == RenderMode.ScreenSpaceOverlay && item != ActualCanvas)
                listCanvas.Add(item);
        }
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void ActivateTheCanvas()
    {
        ActualCanvas.enabled = true;
        
        foreach (var item in listCanvas)
        {
            item.enabled = false;
        }
    }
}
