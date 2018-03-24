using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class GeocodingAPI : MonoBehaviour {

    public Text input;
    public Text outputlat;
    public Text outputlng;
    public GameObject rawimg;
    LocationInfo li;
    public int zoom = 14;
    public int mapWidth = 400;
    public int mapHeight = 200;
    public enum mapType { roadmap, satellite, hybrid, terrain }
    public mapType mapSelected;
    public int scale;
    private string address;
    private string latitude;
    private string longitude;

    // Use this for initialization
    void Start(){

    }
	
	// Update is called once per frame
	void Update() {
		
	}

    public void toggleEnabled()
    {
        gameObject.active = !gameObject.active;
    }


    public void onApiButtonClick(){

        address = input.text.Replace(" ", "+");
        string url = "https://maps.googleapis.com/maps/api/geocode/json?address=" + address + "&key=AIzaSyAI3yet1WE_WJytKeLVB7YWrsLJU5PwNRk";

        StartCoroutine( callGeoApi(url) );
    }

    //IEnumerators are not real time, so they shouldn't be called synchronously on a button.onclick() event in the inspector
    public IEnumerator callGeoApi(string url) {

        using (WWW www = new WWW(url))
            {
            yield return www;
            string result = www.text;
            //check if api request failed
            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.Log(www.error);
            } else {
                var N = JSON.Parse(result);
                Debug.Log("Simple json: " + N);
                latitude = N["results"][0]["geometry"]["location"][0];
                longitude = N["results"][0]["geometry"]["location"][1];
                updateOutput(latitude, longitude);
            }
        } 
     }


    public void updateOutput(string lat,string lng)
    {
        outputlat.text = "Latitude: " + lat;
        outputlng.text = "Longitude: " + lng;
        
        StartCoroutine( DrawMap());
    }

    public IEnumerator DrawMap()
    {
        string url = "https://maps.googleapis.com/maps/api/staticmap?center=" + latitude + "," + longitude +
            "&zoom=" + zoom + "&size=" + mapWidth + "x" + mapHeight + "&scale=" + scale
            + "&maptype=" + mapSelected + "&markers=size:mid%7Ccolor:0xff0000%7Clabel:%7C" + address +
            "&key=AIzaSyAI3yet1WE_WJytKeLVB7YWrsLJU5PwNRk";
        WWW www = new WWW(url);
        //yield return www;

        // Create a texture to use
        Texture2D mapTexture = new Texture2D(mapWidth, mapHeight, TextureFormat.DXT1, false);

        while (!www.isDone)
            yield return null;
        if (www.error == null)
        {
            www.LoadImageIntoTexture(mapTexture);
            rawimg.GetComponent<CanvasRenderer>().SetTexture(mapTexture);
        }

    }
}

