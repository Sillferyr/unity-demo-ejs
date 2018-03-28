using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class UseScriptableCards : MonoBehaviour
{
    public GameObject cardPrefab;
    //list containing the references to our Scriptable objects
    public List<ScriptableCards> myScriptableCardList = new List<ScriptableCards>();
    //list containing the spawn positions for our Scriptable objects when instantiated
    public List<Vector3> spawnPoints;
    

    //contains instantiated GameObjects for ingame modification
    private GameObject[] cardObjectArray;
    private bool areCardsInstantiated = false;
    private enum cardType { Allied, Talisman, Gold, Weapon, Totem, Reign };

    // Use this for initialization
    void Start()
    {
        cardObjectArray = new GameObject[myScriptableCardList.Count];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onInstantiateCardsButton() {
        //only instantiates if there are no cards or they all were destroyed.
        if (!areCardsInstantiated)
        {
            //click spam protection
            areCardsInstantiated = true;
            int i = 0;
            //sync instantiation of cards with default image as placeholder while asyncCardLoad works 
            //this is assuming it's convenient performance-wise to at least load the default prefab in this fashion 
            foreach (Vector3 spawn in spawnPoints)
            {                
                GameObject Card = Instantiate(cardPrefab);
                Card.transform.position = spawn;
                cardObjectArray[i++] = Card;
            }
            //DO ASYNC LOAD!!
            StartCoroutine( asyncCardLoad() );
        }
    }

    /* doesn't use prefab nor events, unconvenient.
    public void createCardsWithOnlyCode()
    {
        //only instantiates if there are no cards or they all were destroyed.
        if (!areCardsInstantiated)
        {
            int i = 0;
            //sync instantiation of cards with default image as placeholder while asyncCardLoad works
            foreach (Vector3 spawn in spawnPoints)
            {
                GameObject Card = GameObject.CreatePrimitive(PrimitiveType.Quad);
                Card.transform.position = spawn;
                Card.transform.localScale += new Vector3(12F, 20F, 0);

                //load defaults from Resources folder
                Card.GetComponent<Renderer>().material = (Material)Resources.Load("blankCardMat");
                Card.GetComponent<Renderer>().material.mainTexture = Resources.Load("defaultCardImg") as Texture2D;

                cardObjectArray[i++] = Card;
            }
            //DO ASYNC LOAD!!
            StartCoroutine(asyncCardLoad());
        }
    }
    */

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
        //load textures.
        foreach (ScriptableCards CardData in myScriptableCardList) {

            var assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<Texture>(CardData.imageName); 
            yield return assetLoadRequest;

            Texture img = assetLoadRequest.asset as Texture;
            if (img == null) { Debug.LogError("Null object (texture) extracted from assetbundle"); }

            cardObjectArray[i].GetComponent<Renderer>().material.mainTexture = img;
            //loading card values
            cardObjectArray[i].GetComponent<CardBehaviour>().strength = CardData.strength;
            cardObjectArray[i].GetComponent<CardBehaviour>().cost = CardData.cost;
            cardObjectArray[i].GetComponent<CardBehaviour>().type =(CardBehaviour.cardType)CardData.type;
            cardObjectArray[i++].GetComponent<CardBehaviour>().isfury = CardData.isFury;
            //etc...

        }
        myLoadedAssetBundle.Unload(false);      
    }

    public void destroyEventResponse()
    {
        areCardsInstantiated = false;
    }
}
