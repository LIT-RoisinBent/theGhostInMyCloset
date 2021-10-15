using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatDamage : MonoBehaviour
{
    public int whatRat;
    
    //If the player collides with the rat, make the sound, remove the rat from the list in the level manager, then destroy the rat
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            LevelManager.instance.SfxManager("rat");
            LevelManager.instance.RemoveRatsFromList(whatRat);
            Debug.Log("hit rat");
            Destroy(transform.parent.gameObject);
        }
    }
}
