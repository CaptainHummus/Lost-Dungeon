using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardButton : MonoBehaviour
{
    public bool player;
    [SerializeField]
    public int cardValue;
    [SerializeField]
    private bool revealed;
    [SerializeField]
    private bool played;
    [SerializeField]
    private TextMeshProUGUI valueText;
    [SerializeField]
    private CombatManager combatManager;


    public void OnClicked()
    {
        if (!played)
        {
            revealed = true;
            played = true;
            combatManager.AddToPower(cardValue, player);
            valueText.text = cardValue.ToString();
        }

    }

     public void Reveal()
    {
        valueText.text = cardValue.ToString();
        revealed = true;
    }

     void ResetValues()
    {
        revealed = false;
        played = false;
        valueText.text = "?";
    }

}
