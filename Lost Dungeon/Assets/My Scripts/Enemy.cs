using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private ScriptableEnemy currentEnemy;

    [SerializeField]
    private IntVariable enemyHealth;

    public int deckLength = 6;
    public int[] combatDeck;



    void Start()
    {
        combatDeck = new int[deckLength];
        GetComponent<SpriteRenderer>().sprite = currentEnemy.enemySprite;
        enemyHealth.variable = Random.Range(currentEnemy.minHealth, currentEnemy.maxHealth + 1);
        combatDeck = currentEnemy.combatDeck.ToArray();
        combatDeck = Shuffle(combatDeck);

        for (int i = 0; i < combatDeck.Length; i++)
        {
            Debug.Log("Slot " + i + " has " + (combatDeck[i]) + " power");
        }



        //for (int i = 0; i < combatDeck.Length; i++)
        //{
        //    combatDeck[i] = i - 1;
        //    //Debug.Log("Slot " + i + " has " + (i - 1) + " power");
        //}
 

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
