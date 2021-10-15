using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasForEnd : MonoBehaviour
{
    //Separate gas controller for the final battle
    
    private ParticleSystem gas;
    private BoxCollider2D boxCollider;
    public GhoulController ghoulController;
    private void Awake()
    {
        gas = GetComponent<ParticleSystem>();
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
        gas.Stop();
    }

    //Play the particle system, enable the box colliders
    public void GasOn()
    {
        gas.Play();
        boxCollider.enabled = true;
    }
}
