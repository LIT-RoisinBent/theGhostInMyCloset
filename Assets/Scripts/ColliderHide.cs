using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))] 
public class ColliderHide : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private ParticleSystem gas;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        gas = GetComponent<ParticleSystem>();
        
        StartCoroutine(VentHide());
    }

    private IEnumerator VentHide()
    {
        //For forever, enable the collider, turn on the gas, wait, turn off the collider/gas, wait
        for (;;)
        {
            boxCollider.enabled = true;
            gas.Clear();
            gas.Play();
            yield return new WaitForSeconds(3f);
            boxCollider.enabled = false;
            gas.Stop();
            yield return new WaitForSeconds(3f);
        }
    }
}
