using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombatCard : MonoBehaviour
{
    public bool isHidden;
    public int damageValue;
    public int cardIndex;
    public GameObject player;


    void Start()
    {
        player = GameObject.Find("Player");
        damageValue = this.player.GetComponent<Player>().GetCardValue(cardIndex - 1);

        if (isHidden == false)
        {
            transform.Find("Text").GetComponent<Text>().text = damageValue.ToString();
        }

        Time.timeScale = 0;
    }


    public void SelectCard()
    {
        isHidden = false;
        transform.Find("Text").GetComponent<Text>().text = damageValue.ToString();

    }
}
