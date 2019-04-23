using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    public float speedMultiplier = 10;
    public int health = 5;
    public int deckLength = 6;
    public int[] combatDeck;

    public GameObject healthUI;
    public Text goldUI;

    public int gold = 0;

    private Rigidbody2D rb;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        combatDeck = new int[deckLength];

        for (int i = 0; i < combatDeck.Length; i++)
        {
            combatDeck[i] = i - 1;
            //Debug.Log("Slot " + i + " has " + (i - 1) + " power");
        }
        combatDeck = Shuffle(combatDeck);

        healthUI = GameObject.Find("PlayerHealth");


    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speedMultiplier, Input.GetAxisRaw("Vertical") * speedMultiplier);
        //goldUI.text = "Player Gold :" + gold.ToString();
    }

    public void IdiotShuffle()
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

    public void UpdateHealth(int amount)
    {
        health = health + amount;
        healthUI.GetComponent<Text>().text = ("PlayerHP: " + health.ToString());

        if (health < 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}
