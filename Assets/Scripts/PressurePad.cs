using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    private bool pressed;
    public int pressurePadID;
    public VasePuzzle vases;
    private Collider2D boxCollider;

    public void OnTriggerEnter2D(Collider2D other)
    {
        //If the other collider is called vase + what number the pressure pad is (so basically, if vaseID and pressurePadID match)
        if (other.gameObject.tag == "vase"+pressurePadID)
        {
            if (vases.vaseID == pressurePadID)
            {
                //Call PressurePadPuzzle with the ID passed in
                LevelManager.instance.PressurePadPuzzle(pressurePadID);
                Deactivate();
            }
            //Else, ignore the entire collision so the player knows it's the wrong combination, also avoids wrong ID passing
            else
            {
                Physics2D.IgnoreCollision(boxCollider, other, true);
            }
        }
    }

    //If the player successfully places the vase, deactivate the vase and box collider on pressure pad
    private void Deactivate()
    {
        Debug.Log("deactivate");
        vases.Deactivate();
        boxCollider = this.GetComponentInChildren<Collider2D>();
        boxCollider.enabled = false;
    }
}
