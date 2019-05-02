using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PostGameText : MonoBehaviour
{

    [SerializeField]
    private IntVariable playerGold = null;
    [SerializeField]
    private IntVariable playerHealth = null;
    [SerializeField]
    private string text1 = null;
    [SerializeField]
    private string text2 = null;



    private TextMeshProUGUI textMeshReference;
    void Start()
    {
        textMeshReference = GetComponent<TextMeshProUGUI>();
        textMeshReference.text = text1 + playerGold.variable + text2;
    }

}
