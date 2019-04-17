using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    public int playerPower;
    public int playerHealth;
    public int enemyPower;
    public int enemyHealth;

    public int playerCardCounter = 0;
    public int enemyCardCounter = 0;

    public GameObject playerPowerText;
    public GameObject enemyPowerText;

    public GameObject player;
    public GameObject enemy;
    //GameObject[] EnemyButtons;


    void Start()
    {
        playerPowerText = GameObject.Find("PlayerPower");
        enemyPowerText = GameObject.Find("EnemyPower");

        //playerHealthUI = GameObject.Find("PlayerHealth");
        //enemyHealthUI = GameObject.Find("EnemyHealth");

        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        playerHealth = player.GetComponent<Player>().health;
        enemyHealth = enemy.GetComponent<Enemy>().health;

    }

    void CalculateDamage(int _playerPower, int _enemyPower)
    {
        if (_playerPower > _enemyPower)
        {
            enemy.GetComponent<Enemy>().UpdateHealth(-1);
            if (enemyHealth < 1)
            {

            }
        }
        else if (_playerPower < _enemyPower)
        {
            player.GetComponent<Player>().UpdateHealth(-1);
            if (playerHealth < 1)
            {

            }
        }
        else
        {

        }
        playerPower = 0;
        enemyPower = 0;
        playerCardCounter = 0;
        enemyCardCounter = 0;
        playerPowerText.GetComponent<Text>().text = playerPower.ToString();
        enemyPowerText.GetComponent<Text>().text = enemyPower.ToString();
        player.GetComponent<Player>().IdiotShuffle();
        enemy.GetComponent<Enemy>().IdiotShuffle();
    }

    public void AddToPower(int power, bool player)
    {
        if (player == true && playerCardCounter < 2)
        {
            playerPower += power;
            playerPowerText.GetComponent<Text>().text = playerPower.ToString();
            playerCardCounter++;
        }
        else if (player == false && enemyCardCounter < 2)
        {
            enemyPower += power;
            enemyPowerText.GetComponent<Text>().text = enemyPower.ToString();
            enemyCardCounter++;
        }

        if (playerCardCounter == 2 && enemyCardCounter == 2)
        {
            CalculateDamage(playerPower, enemyPower);
        }
    }


}
