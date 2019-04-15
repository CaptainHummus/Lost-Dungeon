using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    public float speedMultiplier = 10;
    public int health = 10;
    public int deckLength = 6;
    public int[] combatDeck;

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

    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speedMultiplier, Input.GetAxisRaw("Vertical") * speedMultiplier);
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
