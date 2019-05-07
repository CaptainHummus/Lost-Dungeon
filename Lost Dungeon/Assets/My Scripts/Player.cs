using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

enum animationMovement
{
    idle,
    up,
    down,
    left,
    right
}

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

    public int powerUp = 0;
    [SerializeField]
    private IntVariable playerThrowingKnives;


    public int knownSlots = 1;




    private Rigidbody2D rb;

    private animationMovement playerAnimation;
    private Animator animator;
    private SpriteRenderer spriteRenderer;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        health.variable = maxHP;
        gold.variable = 0;
        playerThrowingKnives.variable = 0;

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
            if (rb.velocity == Vector2.zero)
            {
                playerAnimation = animationMovement.idle;
                
            }
            if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.y))
            {
                if (rb.velocity.x < 0)
                {
                    playerAnimation = animationMovement.left;
                    if (!spriteRenderer.flipX)
                    {
                        spriteRenderer.flipX = true;
                    }
                }
                else
                {
                    playerAnimation = animationMovement.right;
                    if (spriteRenderer.flipX)
                    {
                        spriteRenderer.flipX = false;
                    }
                }
            }
            else if (Mathf.Abs(rb.velocity.x) < Mathf.Abs(rb.velocity.y))
            {
                if (rb.velocity.y < 0)
                {
                    playerAnimation = animationMovement.down;
                }
                else
                {
                    playerAnimation = animationMovement.up;
                }
            }
            animator.SetInteger("Enum Direction", (int)playerAnimation);
            Debug.Log(playerAnimation);


        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
        //goldUI.text = "Player Gold :" + gold.ToString();
    }

    public void PowerUP()
    {
        powerUp++;
        for (int i = 0; i < combatDeck.Length; i++)
        {
            combatDeck[i] = i - 1 + powerUp;
            Debug.Log("Plus One");
        }
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
        EventHandler.instance.OverrideEvent(startEvent);
    }

}
