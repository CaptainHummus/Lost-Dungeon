using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int health;
    public int deckLength = 6;
    public int[] combatDeck;
    public GameObject combatUIPrefab;
    private GameObject combatUI;

    public GameObject healthUI;
    // Start is called before the first frame update
    void Start()
    {
        health = Random.Range(1, 2);
        combatDeck = new int[deckLength];


        for (int i = 0; i < combatDeck.Length; i++)
        {
            combatDeck[i] = i - 1;
            //Debug.Log("Slot " + i + " has " + (i - 1) + " power");
        }
        combatDeck = Shuffle(combatDeck);
        combatUI = Instantiate(combatUIPrefab, GameObject.Find("Canvas").transform);

        Debug.Log("Enemy Health is: " + health);
        healthUI = GameObject.Find("EnemyHealth");
        healthUI.GetComponent<Text>().text = ("EnemyHP: " + health.ToString());
        Time.timeScale = 0;
    }


    public void IdiotShuffle()
    {
        combatDeck = Shuffle(combatDeck);
        if (health == 0)
        {
            Destroy(combatUI);
            Destroy(gameObject);
            Time.timeScale = 1;
        }
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

    public void UpdateHealth(int amount)
    {
        health = health + amount;
        healthUI.GetComponent<Text>().text = ("EnemyHP: " + health.ToString());

    }
}
