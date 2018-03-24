using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class JsonPHAPI : MonoBehaviour {

    public Text input;
    public Text output;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void toggleEnabled()
    {
        gameObject.active = !gameObject.active;
    }

    public void onApiButtonClick()
    {

        int id;
        string idstr = input.text;
        int.TryParse(idstr, out id);
        if(id == 0)
            Debug.LogError("Post number must be integer ");

        string url = "https://jsonplaceholder.typicode.com/comments?postId=" + id ;

        StartCoroutine(callPHApi(url));
    }
    
    public IEnumerator callPHApi(string url)
    {

        using (WWW www = new WWW(url))
        {
            yield return www;
            string result = www.text;
            //check if api request failed
            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.Log(www.error);
            } else {
                //added "results : " for parsing with SimpleJSON
                var N = JSON.Parse(" \"results\" : " + result);
                //Debug.Log(N.ToString());

                string commentData = "";
                //N[i][j] where i stands for different comments and j stands for data in the comment i (0 = "postid", 1="id", 2="name", 3="email", 4="body")
                for (int i = 0; !string.IsNullOrEmpty(N[i][0]); i++ ) {
                    commentData = "\nId: " + N[i][1] + "\nName: " + N[i][2] + "\nEmail: " + N[i][3] + " \nBody: " + N[i][4] +"\n";                  
                    updateOutput(commentData);
                }
                
            }
        }
    }

    public void updateOutput(string str)
    {
        output.text += str;
    }
}
