﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour
{
    [SerializeField]
    private Event chestEvent;
    [SerializeField]
    private GameObject openChest;
    private bool opened = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (opened == false && collision.tag == "Player")
        {
            EventHandler.instance.OverrideEvent(chestEvent);
            openChest.SetActive(true);
            opened = true;
        }
    }

}
