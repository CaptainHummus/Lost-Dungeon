using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Portal
}

[CreateAssetMenu]
public class Event : ScriptableObject
{
    public string eventName;
    public Rarity rarity;
    public string eventDescription;
    public int numberOfOptions = 1;
    public int goldGain;


    public int healthCalculation;

}
