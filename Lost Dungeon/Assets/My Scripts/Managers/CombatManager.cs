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

    private int playerCardCounter = 0;
    private int enemyCardCounter = 0;

    [SerializeField]
    private Player player;
    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    private Button[] playerCardsUI = new Button[6];
    [SerializeField]
    private Button[] enemyCardsUI = new Button[6];



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enemy = GameObject.FindGameObjectWithTag("Enemy");

    }

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        updateCardValues();

    }

    void updateCardValues()
    {
        for (int i = 0; i < playerCardsUI.Length; i++)
        {
            playerCardsUI[i].GetComponent<CardButton>().cardValue = player.combatDeck[i];
            playerCardsUI[i].GetComponent<CardButton>().Reveal();
        }
    }

    void CalculateDamage(int _playerPower, int _enemyPower)
    {
        if (_playerPower > _enemyPower)
        {
            enemyHealth.variable--;
            if (enemyHealth.variable < 1)
            {

            }
        }
        else if (_playerPower < _enemyPower)
        {
            player.GetComponent<Player>().UpdateHealth(-1);
            if (playerHealth.variable < 1)
            {

            }
        }
        else
        {

        }
        playerPower.variable = 0;
        enemyPower.variable = 0;
        playerCardCounter = 0;
        enemyCardCounter = 0;
        player.IdiotShuffle();
        enemy.GetComponent<OldEnemy>().IdiotShuffle();
    }

    public void AddToPower(int power, bool player)
    {
        if (player == true && playerCardCounter < 2)
        {
            playerPower.variable += power;
            playerCardCounter++;
        }
        else if (player == false && enemyCardCounter < 2)
        {
            enemyPower.variable += power;
            enemyCardCounter++;
        }

        if (playerCardCounter == 2 && enemyCardCounter == 2)
        {
            CalculateDamage(playerPower.variable, enemyPower.variable);
        }
    }


}
