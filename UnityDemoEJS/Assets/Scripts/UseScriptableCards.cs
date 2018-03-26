using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class UseScriptableCards : MonoBehaviour
{
    //list containing the references to our Scriptable objects
    public List<ScriptableCards> myScriptableCardList = new List<ScriptableCards>();
    //list containing the spawn positions for our Scriptable objects when instantiated
    public List<Vector3> spawnPoints;

    //contains instantiated GameObjects for ingame modification
    private GameObject[] cardObjectArray;
    private bool areCardsInstantiated = false;


    // Use this for initialization
    void Start()
    {
        cardObjectArray = new GameObject[myScriptableCardList.Count];

        // avoid to overwrite Scriptable Objects. 
        //List<ScriptableCards> copy = Instantiate(myScriptableCardList); //uno por uno 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            onEscapeButton();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            changeDamage(2);
        }
    }

    public void onInstantiateCardsButton() {
        //only instantiates once, could alternatively delete all cards and reinstantiate
        if (!areCardsInstantiated)
        {
            int i = 0;
            //sync instantiation of cards with default image as placeholder while asyncCardLoad works
            foreach (Vector3 spawn in spawnPoints)
            {                
                //GameObject Card = new GameObject("Card");
                GameObject Card = GameObject.CreatePrimitive(PrimitiveType.Quad);

                Card.transform.position = spawn;
                Card.transform.localScale += new Vector3(12F, 20F, 0);

                //load defaults from Resources folder
                Card.GetComponent<Renderer>().material = (Material)Resources.Load("defaultCardMat");
                Card.GetComponent<Renderer>().material.mainTexture = Resources.Load("defaultCardImg") as Texture2D;

                //TODO: assign scriptableobject data to Card

                cardObjectArray[i++] = Card;
            }
            //TODO: async load
            StartCoroutine( asyncCardLoad() );
        }
    }

    //async load from assetbundle
    IEnumerator asyncCardLoad() {
        
        var bundleLoadRequest = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, "cardresourses"));
        yield return bundleLoadRequest;

        var myLoadedAssetBundle = bundleLoadRequest.assetBundle;
        if (myLoadedAssetBundle == null)
        {
            Debug.LogError("Failed to load AssetBundle!");
            yield break;
        }

        int i = 0;
        //TODO: foreach (ScriptableCards CardData in myScriptableCardList) , using i and assigning the loaded texture on cardObjectArray[i]

        var assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync("defaultCardImg"); //load number i card image
        yield return assetLoadRequest;

        //GameObject prefab = assetLoadRequest.asset as GameObject;
        //Instantiate(prefab);

        myLoadedAssetBundle.Unload(false);
            
        

    }

    //TODO:
    public void changeDamage(int change)
    {
        //change gameObject
        //foreach (var Card in cardObjectArray){
        //    Card.strength += change;       }

        //change scriptableObject
        //foreach (ScriptableCards CardData in myScriptableCardList){
        //    CardData.Strength += change;                          }  /
    }

    public void onEscapeButton() {
        Resources.UnloadUnusedAssets();
        //TODO: load main scene
    }
}
