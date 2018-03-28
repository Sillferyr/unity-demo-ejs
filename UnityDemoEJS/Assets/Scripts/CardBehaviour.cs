using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CardBehaviour : MonoBehaviour {

    public enum cardType { Allied, Talisman, Gold, Weapon, Totem, Reign };
    public cardType type;
    public int strength;
    public int cost;
    public bool isfury; 

    //not needed because event invocation is done on a button.
    //public UnityEvent destroyEvent; 

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void destroyEventResponse()
    {
        Destroy(gameObject);
    }

    public void damageUpEventResponse()
    {
        strength += 1;
    }

    public void damageDownEventResponse()
    {
        if(strength >= 1)
        strength -= 1;
    }

}

