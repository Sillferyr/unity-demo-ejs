using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToMainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            LoadMainMenu();
        }
    }

    public void LoadMainMenu()
    {
        Resources.UnloadUnusedAssets();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}
