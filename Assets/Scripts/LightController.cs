using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightController : MonoBehaviour
{
    //All this is is a lighting effect that triggers a light flicker
    
    private IEnumerator coroutine;
    public Light2D ceilingLight;

    private void Update()
    {
        coroutine = LightFlicker();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(coroutine);
        }
    }

    private IEnumerator LightFlicker()
    {
        Debug.Log("light flicker");
        ceilingLight.enabled = false;
        yield return new WaitForSeconds(.2f);
        ceilingLight.enabled = true;
        yield return new WaitForSeconds(.1f);
        ceilingLight.enabled = false;
        yield return new WaitForSeconds(.2f);
        ceilingLight.enabled = true;
        yield return new WaitForSeconds(.1f);
        ceilingLight.enabled = false;
        Destroy(gameObject);
    }
}
