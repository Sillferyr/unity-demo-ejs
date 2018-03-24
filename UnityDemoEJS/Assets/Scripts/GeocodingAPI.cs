using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class GeocodingAPI : MonoBehaviour {

    public Text input;
    public Text outputlat;
    public Text outputlng;
    public RawImage img;
    private string latitude;
    private string longitude;

    // Use this for initialization
    void Start(){

    }
	
	// Update is called once per frame
	void Update() {
		
	}


    public void onButton1Click(){

        string address = input.text.Replace(" ", "+");
        string url = "https://maps.googleapis.com/maps/api/geocode/json?address=" + address + "&key=AIzaSyAI3yet1WE_WJytKeLVB7YWrsLJU5PwNRk";

        StartCoroutine( callGeoApi(url) );
    }

    //IEnumerators are not real time, so they shouldn't be called synchronously on a button.onclick() event in the inspector
    public IEnumerator callGeoApi(string url) {

        using (WWW www = new WWW(url))
            {
            yield return www;
            string result = www.text;

            var N = JSON.Parse(result);
            latitude = N["results"][0]["geometry"]["location"][0]; 
            longitude = N["results"][0]["geometry"]["location"][1];
            updateOutput(latitude, longitude);
        } 
     }


    public void updateOutput(string lat,string lng)
    {
        outputlat.text = "Latitude: " + lat;
        outputlng.text = "Longitude: " + lng;
        //TODO: draw map
        //StartCoroutine(MapAPI.DrawMap(latitude, longitude, img));
    }

}

