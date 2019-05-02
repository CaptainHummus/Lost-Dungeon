using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    [SerializeField]
    private IntVariable playerPower = null;
    [SerializeField]
    private IntVariable playerHealth = null;
    [SerializeField]
    private IntVariable enemyPower = null;
    [SerializeField]
    private IntVariable enemyHealth = null;

    [SerializeField]
    private Event enemyDeath = null;
    [SerializeField]
    private Event playerDeath = null;



    private int playerCardCounter = 0;
    private int enemyCardCounter = 0;

    [SerializeField]
    private Player player;
    [SerializeField]
    private Enemy enemy;
    [SerializeField]
    private GameObject continueButton;

    [SerializeField]
    private Button[] playerCardsUI = new Button[6];
    [SerializeField]
    private Button[] enemyCardsUI = new Button[6];



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();

    }

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        updateCardValues();

    }

    void updateCardValues()
    {
        player.ShufflePlayerDeck();
        enemy.EnemyShuffle();

        for (int i = 0; i < playerCardsUI.Length; i++)
        {
            playerCardsUI[i].GetComponent<CardButton>().cardValue = player.combatDeck[i];
            if (player.knownSlots -1 >= i)
            {
                playerCardsUI[i].GetComponent<CardButton>().Reveal();
            }
        }
        for (int i = 0; i < enemy.combatDeck.Length; i++)
        {
            enemyCardsUI[i].GetComponent<CardButton>().cardValue = enemy.combatDeck[i];
        }
    }

    public void CalculateDamage()
    {
        if (playerPower.variable > enemyPower.variable)
        {
            enemyHealth.variable--;
            if (enemyHealth.variable < 1)
            {
                EventHandler.instance.RunEvent(enemyDeath);
                gameObject.SetActive(false);
            }
        }
        else if (playerPower.variable < enemyPower.variable)
        {
            playerHealth.variable--;
            if (playerHealth.variable < 1)
            {
                EventHandler.instance.RunEvent(playerDeath);
                gameObject.SetActive(false);
            }
        }
        else
        {
            enemyHealth.variable--;
            if (enemyHealth.variable < 1)
            {
                EventHandler.instance.RunEvent(enemyDeath);
                gameObject.SetActive(false);
            }
            playerHealth.variable--;
            if (playerHealth.variable < 1)
            {
                EventHandler.instance.RunEvent(playerDeath);
                gameObject.SetActive(false);
            }
        }
        playerPower.variable = 0;
        enemyPower.variable = 0;
        playerCardCounter = 0;
        enemyCardCounter = 0;
        player.ShufflePlayerDeck();
        enemy.EnemyShuffle();
        continueButton.SetActive(false);
    }

    public void AddToPower(int power, bool player)
    {
        if (player == true && playerCardCounter < 2)
        {
            playerPower.variable += power;
            playerCardCounter++;
            enemyCardsUI[playerCardCounter].GetComponent<CardButton>().OnClicked();
        }
        else if (player == false && enemyCardCounter < 2)
        {
            enemyPower.variable += power;
            enemyCardCounter++;
        }

        if (playerCardCounter == 2)
        {
            continueButton.SetActive(true);
            //CalculateDamage(playerPower.variable, enemyPower.variable);
        }
    }


}
