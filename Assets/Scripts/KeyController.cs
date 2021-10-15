using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public string whatKey;

    public void OnTriggerStay2D(Collider2D other)
    {
        //If the key collides with the player, assign to UI and adds to list
        if (other.tag == "Player")
        {
            LevelManager.instance.SfxManager("key");
            LevelManager.instance.ShowKeysUI();
            LevelManager.instance.keys.Add(whatKey); //Add key to list in LvlMgr
            Destroy(gameObject); //Destroy key
        }
    }
}
