using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private IntVariable health = null;
    [SerializeField]
    private IntVariable gold = null;

    [SerializeField]
    private int maxHP = 10;

    [SerializeField]
    private float speedMultiplier = 10;
    [SerializeField]
    private Event startEvent = null;
    public int deckLength = 6;
    public int[] combatDeck;
    public bool canMove = true;

    public int knownSlots = 1;




    private Rigidbody2D rb;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        health.variable = maxHP;
        gold.variable = 0;

        combatDeck = new int[deckLength];

        for (int i = 0; i < combatDeck.Length; i++)
        {
            combatDeck[i] = i - 1;
            //Debug.Log("Slot " + i + " has " + (i - 1) + " power");
        }
        combatDeck = Shuffle(combatDeck);
        StartCoroutine(RunStartEvent());
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speedMultiplier, Input.GetAxisRaw("Vertical") * speedMultiplier);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
        //goldUI.text = "Player Gold :" + gold.ToString();
    }

    public void ShufflePlayerDeck()
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
        health.variable = health.variable + amount;
        //healthUI.GetComponent<Text>().text = ("PlayerHP: " + health.ToString());

        if (health.variable < 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    IEnumerator RunStartEvent()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        EventHandler.instance.RunEvent(startEvent);
    }

}
