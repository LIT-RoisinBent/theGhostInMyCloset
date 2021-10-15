using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverControl : MonoBehaviour
{
    public GhoulController ghoulController;
    
    public GameObject theVents;
    public List<GameObject> vents = new List<GameObject>();
    private GasForEnd theGas;
    private BoxCollider2D boxCollider;
    private Animator leverAnim;
    

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        leverAnim = GetComponent<Animator>();
        boxCollider.enabled = false;
    }

    //Lever gets enabled when player walks into the 9th dialogue trigger
    public void LeverEnabled()
    {
        boxCollider.enabled = true;
    }
    
    //If the player is in the trigger box, and they press E, it turns on every vent in the room, and turns on the
    //final dialogue
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                leverAnim.SetBool("lever", true);
                foreach (var vent in vents)
                {
                    var gasParticle = vent.GetComponent<GasForEnd>();
                    gasParticle.GasOn();
                }
                
                LevelManager.instance.MusicManager("stop");
                LevelManager.instance.End();
                ghoulController.Dead();
            }
        }
    }
}
