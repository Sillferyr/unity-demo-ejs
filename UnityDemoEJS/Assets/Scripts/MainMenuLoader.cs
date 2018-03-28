using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLoader : MonoBehaviour {

    public GameObject mail1;
    public GameObject mail2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {
            ExitGame();
        }

    }

    public void loadScene(string name){
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }
    
    public void enableMails()
    {
        mail1.SetActive(true);
        mail2.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
