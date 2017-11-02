using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    // Use this for initialization
    public Button yourButton;
    public int scena;

    void Start()
    {
        yourButton = this.GetComponent<Button>();
        yourButton.onClick.AddListener(ChangeSceneTo);
    }

    void ChangeSceneTo()
    {
        SceneManager.LoadScene(scena);
        Time.timeScale = 1;
    }
}
