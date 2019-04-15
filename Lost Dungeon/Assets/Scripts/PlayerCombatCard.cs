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
    public GameObject combatManager;


    void Start()
    {
        player = GameObject.Find("Player");
        //combatManager = GameObject.Find("CombatUI");
        damageValue = this.player.GetComponent<Player>().GetCardValue(cardIndex - 1);

        if (isHidden == false)
        {
            transform.Find("Text").GetComponent<Text>().text = damageValue.ToString();
        }
        else
        {
            transform.Find("Text").GetComponent<Text>().text = "?";
        }

    }


    public void SelectCard()
    {
        transform.Find("Text").GetComponent<Text>().text = damageValue.ToString();
        combatManager.GetComponent<CombatManager>().AddToPower(damageValue, true);

    }
}
