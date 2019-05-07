using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ValueUpdaterUI : MonoBehaviour
{
    [SerializeField]
    private IntVariable value = null;

    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (text.text == value.variable.ToString())
        {
            
        }
        else
        {
            iTween.PunchScale(gameObject, new Vector3(1, 1, 1), 1f);
            text.text = value.variable.ToString();

        }
    }

}
