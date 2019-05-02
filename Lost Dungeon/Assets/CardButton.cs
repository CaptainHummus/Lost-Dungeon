using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        if (!played && combatManager.playerCardCounter < 2)
        {
            revealed = true;
            played = true;
            combatManager.AddToPower(cardValue, player);
            valueText.text = cardValue.ToString();
            GetComponent<Image>().color = Color.red;
        }

    }

     public void Reveal()
    {
        valueText.text = cardValue.ToString();
        revealed = true;
    }

     public void ResetValues()
    {
        revealed = false;
        played = false;
        valueText.text = "?";
        GetComponent<Image>().color = new Color(219, 219, 219);
    }

}
