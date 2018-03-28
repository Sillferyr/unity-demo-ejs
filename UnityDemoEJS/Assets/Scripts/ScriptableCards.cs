using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Card", order = 1)]
public class ScriptableCards : ScriptableObject {
    public string cardName = "New ScriptableCard";
    public enum cardType { Allied, Talisman, Gold, Weapon, Totem, Reign };
    public cardType type;
    public int cost = 0;
    public int strength = 0;
    //extra: bitwise attributes as a single int binary chain, ¿worth it?
    public bool isFury = false;
    public bool isUnBlockable = false;
    public bool isUnExileable = false;
    public bool isUnique = false;
    public bool isFed = false;
    public bool isPurified = false;
    public string flavourText = "";
    public string description = "Abilities";
    public string imageName = "defaultCardImg";
}