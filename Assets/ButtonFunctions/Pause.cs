using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    public Button yourButton;
    

    void Start()
    {
        yourButton = this.GetComponent<Button>();
        yourButton.onClick.AddListener(PauseNow);
    }

    void PauseNow()
    {
 
        Time.timeScale = 0;
    }

}

