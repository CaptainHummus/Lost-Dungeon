using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarity
{
    None,
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
    public bool pauseMovement = true;
    public string eventDescription;
    public int goldGain;
    public ScriptableEnemy enemy;
    public float chance;
    public int healthCalculation;

    public bool exitPortal;
    public bool deathEvent;

    public int numberOfOptions = 1;
    public Event option1;
    public Event option2;


}
