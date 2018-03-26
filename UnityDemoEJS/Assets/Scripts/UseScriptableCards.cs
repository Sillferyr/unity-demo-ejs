using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UseScriptableCards : MonoBehaviour
{
    //list containing the references to our Scriptable objects
    public List<ScriptableCards> myScriptableCardList = new List<ScriptableCards>();
    //list containing the spawn positions for our Scriptable objects when instantiated
    public List<Vector3> spawnPoints;

    //list containing instantiated GameObjects for ingame modification
    private List<GameObject> myCardObjects = null;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            changeDamage(2);
        }
    }

    //TODO:
    public void changeDamage(int change)
    {

        //change gameObject
        //foreach (var Card in myCardObjects){
        //    Card.strength += change;       }

        //change scriptableObject
        //foreach (ScriptableCards CardData in myScriptableCardList){
        //    CardData.Strength += change;                          }  /
    }

    public void onInstantiateCardsButton() {
        //only instantiates once, could alternatively delete all cards and reinstantiate
        if (myCardObjects == null)
        {
            myCardObjects = new List<GameObject>(); //tipo carta "instanciada" seria mejor para cambios
            
            //foreach (ScriptableCards CardData in myScriptableCardList)
            foreach (Vector3 spawn in spawnPoints)
            {
                //GameObject Card = new GameObject("Card");
                GameObject Card = GameObject.CreatePrimitive(PrimitiveType.Quad);

                Card.transform.position = spawn;
                Card.transform.localScale += new Vector3(12F, 20F, 0);

                Card.GetComponent<Renderer>().material = (Material)Resources.Load("defaultCardMat");
                Card.GetComponent<Renderer>().material.mainTexture = Resources.Load("defaultCardImg") as Texture2D;

                //TODO

            }
        }
    }

}