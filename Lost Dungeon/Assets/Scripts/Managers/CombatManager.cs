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

    public GameObject playerPowerText;
    public GameObject enemyPowerText;

    void Start()
    {
        playerPowerText = GameObject.Find("PlayerPower");
        enemyPowerText = GameObject.Find("EnemyPower");
        
    }

    void CalculateDamage(int _playerPower, int _enemyPower)
    {
        if (_playerPower > _enemyPower)
        {
            enemyHealth--;
        }
        else if (_playerPower < _enemyPower)
        {
            playerHealth--;
        }
        else
        {

        }
    }
}
