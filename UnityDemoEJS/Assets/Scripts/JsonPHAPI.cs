using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class JsonPHAPI : MonoBehaviour {

    public Text input;
    public Text output;
    public int apiNumber = 3; // 3 for JsonPlaceHolder, 4 for self-made

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
        string inStr = input.text;
        int.TryParse(inStr, out id);
        if(id == 0)
            Debug.LogError("Post number must be integer ");

        string url;

        switch (apiNumber)
        {
            case 3:
                Debug.Log("Call Api3: JsonPlaceHolder ");
                url = "https://jsonplaceholder.typicode.com/comments?postId=" + id;
                break;

            case 4:
                Debug.Log("Call Api4: last posts");
                url = "https://my-json-server.typicode.com/Sillferyr/unity-demo-api/posts" ;
                break;

            default:
                Debug.Log("Unknown Api number");
                url = "";
                break;
        }
        

        StartCoroutine(callPHApi(url, apiNumber, id ));
    }


    public IEnumerator callPHApi(string url, int apiNumber, int inputParams)
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
                Debug.Log("Api" + apiNumber + "return Parsed");
                Debug.Log(N.ToString());

                switch (apiNumber)
                {
                    case 3:
                        string commentData = "";
                        //N[i][j] where i stands for different comments and j stands for data in the comment i (J = 0 : "postid", 1:"id", 2:"name", 3:"email", 4 :"body")
                        for (int i = 0; !string.IsNullOrEmpty(N[i][0]); i++)
                        {
                            commentData = "\nId: " + N[i][1] + "\nName: " + N[i][2] + "\nEmail: " + N[i][3] + " \nBody: " + N[i][4] + "\n";
                            updateOutput(commentData);
                        }
                        
                        break;

                    case 4:
                        //TODO:  parse lasts posts by inputParams(how many last posts), get their data and call their thumb images by postId
                        int length = N.Count;
                        int posts_req = inputParams;
                        int posts_rdy = 0;
                        //N[i][j] where i stands for different posts and j stands for data in the post i (J= 0 : "userid", 1 : "id", 2 : "title", 3 : "body")
                        for (int i = length -1 ;  posts_rdy++ >= posts_req ; i--)
                        {
                            //debug
                            commentData = "\nUserid: " + N[i][0] +"\nId: " + N[i][1] + "\nTitle: " + N[i][2] + "\nBody: " + N[i][3] + "\n";
                            Debug.Log(commentData);
                            updateOutput(commentData);
                            //TODO: updateOutputPhoto();
                        }


                        //call for photos
                        //callPHApi("https://my-json-server.typicode.com/Sillferyr/unity-demo-api/photos?postId="+postId , 41)
                        break;

                    case 41:
                        //TODO: another api call for photos...
                        Debug.Log("Call Api4: last posts-> get thumb images");

                        break;

                    default:
                        Debug.Log("Unknown Api number");
                        break;
                }            
            }
        }
    }


    public void updateOutput(string str)
    {
        output.text += str;
    }

    public void updateOutputPhoto(Texture2D image) {
        //TODO
    }
}
