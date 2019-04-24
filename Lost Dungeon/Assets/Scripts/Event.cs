using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "WorldEvents/New Event", order = 1)]
public class Event : ScriptableObject
{
    [System.Flags]
    public enum Rarity
    {
        Common,
        Rare,
        Epic,      
    }
    public int goldGain;
}
