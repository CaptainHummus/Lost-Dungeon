﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Enemy : ScriptableObject
{
    public int maxHealth;
    public int minHealth;
    public float fleeChance;
    public List<int> combatDeck;



}
