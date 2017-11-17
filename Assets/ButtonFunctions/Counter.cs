using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Counter : MonoBehaviour {

    Text textCounter;
    int counter;
    float secondsCounter;
    

    public string sceneToChange;


	void Start () {

        textCounter = GetComponent<Text>();
        counter = int.Parse(GetComponent<Text>().text);
    }
	
	void Update () {
		
       if (counter != 0)
        {
            
            textCounter.text = counter.ToString();
        }

        if (counter == 0)
        {
            SceneManager.LoadScene(sceneToChange);
        }

        secondsCounter++;

        if (secondsCounter >= 60 && counter > 0)
        {
            counter--;
            secondsCounter = 0;
        }


    }
}
