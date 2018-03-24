using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MapAPI : MonoBehaviour
{

    public RawImage img;

    string url;

    public float lat; 
    public float lng; 

    LocationInfo li;

    public int zoom = 14;
    public int mapWidth = 400;
    public int mapHeight = 200;

    public enum mapType { roadmap, satellite, hybrid, terrain }
    public mapType mapSelected;
    public int scale;


    //overload
    public IEnumerator DrawMap(string lat, string lng,RawImage rimg)
    {
        url = "https://maps.googleapis.com/maps/api/staticmap?center=" + lat + "," + lng +
            "&zoom=" + zoom + "&size=" + mapWidth + "x" + mapHeight + "&scale=" + scale
            + "&maptype=" + mapSelected +
            "&key=AIzaSyAI3yet1WE_WJytKeLVB7YWrsLJU5PwNRk";
        WWW www = new WWW(url);
        yield return www;
        rimg.texture = www.texture;
        rimg.SetNativeSize();

    }

    public IEnumerator DrawMap()
    {
        url = "https://maps.googleapis.com/maps/api/staticmap?center=" + lat + "," + lng +
            "&zoom=" + zoom + "&size=" + mapWidth + "x" + mapHeight + "&scale=" + scale
            + "&maptype=" + mapSelected +
            "&key=AIzaSyAI3yet1WE_WJytKeLVB7YWrsLJU5PwNRk";
        WWW www = new WWW(url);
        yield return www;
        img.texture = www.texture;
        img.SetNativeSize();

    }
    // Use this for initialization
    void Start()
    {
        img = gameObject.GetComponent<RawImage>();
        //StartCoroutine(DrawMap());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
    
