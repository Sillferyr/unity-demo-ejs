using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class JsonPHAPI : MonoBehaviour {

    public Text input;
    public Text output;
    public GameObject outputRawImage;
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
            Debug.LogError("Post number must be an integer  ");

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
                Debug.Log("Api" + apiNumber + " Parsed");
                //Debug.Log(N.ToString());

                string dataOut = "";
                switch (apiNumber)
                {
                    case 3:
                        //N[i][j] where i stands for different comments and j stands for data in the comment i (J = 0 : "postid", 1:"id", 2:"name", 3:"email", 4 :"body")
                        for (int i = 0; !string.IsNullOrEmpty(N[i][0]); i++)
                        {
                            dataOut = "\nId: " + N[i][1] + "\nName: " + N[i][2] + "\nEmail: " + N[i][3] + " \nBody: " + N[i][4] + "\n";
                            updateOutput(dataOut);
                        }
                        
                        break;

                    //parse lasts posts by inputParams(how many last posts), get their data and call their thumb images by postId
                    case 4:                    
                        int length = N.Count;
                        int posts_req = inputParams;
                        int posts_rdy = 0;
                        //N[i][j] where i stands for different posts and j stands for data in the post i (J= 0 : "userid", 1 : "id", 2 : "title", 3 : "body")
                        for (int i = length -1 ;  posts_rdy++ < posts_req ; i--)
                        {
                            //debug
                            dataOut = "\nUserid: " + N[i][0] +"\nId: " + N[i][1] + "\nTitle: " + N[i][2] + "\nBody: " + N[i][3] + "\n";
                            updateOutput(dataOut);

                            //call for photos
                            StartCoroutine( callPHApi("https://my-json-server.typicode.com/Sillferyr/unity-demo-api/photos?postId=" + N[i][1], 41, 0) );

                        }

                        break;

                    case 41:
                        Debug.Log("Call Api4: last posts-> get thumb images");
                        //N[0][j] where 0 stands for the first photo and j stands for data in the photo i (J = 0 : "postid", 1:"id", 2:"title", 3:"url", 4 :"thumbnailurl")                     
                        string thumbnailUrl = N[0][4];
                        //TODO: load picture from url
                        Texture2D tex;
                        tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
                        using ( WWW thumbWWW = new WWW(thumbnailUrl) )
                        {
                            yield return thumbWWW;
                            thumbWWW.LoadImageIntoTexture(tex);
                            updateOutputPhoto(tex);
                            //GetComponent<Renderer>().material.mainTexture = tex;

                        }

                        break;

                    default:
                        Debug.Log("Unknown Api number");
                        break;
                }            
            }
        }
    }

    //extra: instantiate new gameobject instead
    public void updateOutput(string str)
    {
        output.text += str;
    }

    //extra: instantiate new gameobject instead
    public void updateOutputPhoto(Texture2D image) {
        outputRawImage.GetComponent<RawImage>().texture = image;
    }
}
