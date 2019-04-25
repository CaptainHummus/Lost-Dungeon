using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCombatCard : MonoBehaviour
{
    public bool isHidden;
    public int damageValue;
    public int cardIndex;
    public GameObject enemy;
    public GameObject combatManager;


    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        Refresh();

    }


    public void SelectCard()
    {
        transform.Find("Text").GetComponent<Text>().text = damageValue.ToString();
        combatManager.GetComponent<CombatManager>().AddToPower(damageValue, false);

    }

    public void Refresh()
    {
        damageValue = this.enemy.GetComponent<OldEnemy>().GetCardValue(cardIndex - 1);
        if (isHidden == false)
        {
            transform.Find("Text").GetComponent<Text>().text = damageValue.ToString();
        }
        else
        {
            transform.Find("Text").GetComponent<Text>().text = "?";
        }
    }
}
