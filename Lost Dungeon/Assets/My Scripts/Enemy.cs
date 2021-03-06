﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public ScriptableEnemy currentEnemy;

    [SerializeField]
    private IntVariable enemyHealth = null;

    public int deckLength = 6;
    public int[] combatDeck = new int[6] ;

    [SerializeField]
    private GameObject combatUI;

    private void Awake()
    {
        combatUI = CombatManager.instance.gameObject;
    }


    void Start()
    {
        //combatDeck = new int[deckLength];
        GetComponent<SpriteRenderer>().sprite = currentEnemy.enemySprite;
        enemyHealth.variable = Random.Range(currentEnemy.minHealth, currentEnemy.maxHealth + 1);
        combatDeck = currentEnemy.combatDeck.ToArray();
        combatDeck = Shuffle(combatDeck);

        for (int i = 0; i < combatDeck.Length; i++)
        {
        }



        //for (int i = 0; i < combatDeck.Length; i++)
        //{
        //    combatDeck[i] = i - 1;
        //    //Debug.Log("Slot " + i + " has " + (i - 1) + " power");
        //}

        combatUI.SetActive(true);
    }

    public void ReloadEnemy()
    {

    }


    public void EnemyShuffle()
    {
        combatDeck = Shuffle(combatDeck);
    }

    int[] Shuffle(int[] deck)
    {
        for (int i = 0; i < deck.Length; i++)
        {
            int temp = deck[i];
            int randomIndex = Random.Range(i, deck.Length);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
        return deck;
    }

    public int GetCardValue(int index)
    {
        return combatDeck[index];
    }

}
