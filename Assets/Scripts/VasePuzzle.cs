using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VasePuzzle : MonoBehaviour
{
    public int vaseID;
    private Collider2D boxCollider;
    private Collider2D circleCollider;
    private Rigidbody2D theRb;
    private GameObject theVase;
    private PickUppable pickUppable;

    private void Awake()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        theRb = gameObject.GetComponent<Rigidbody2D>();
        circleCollider = gameObject.GetComponentInChildren<CircleCollider2D>();
        theVase = gameObject;
        //Ignore any collisions within itself (there are two colliders total)
        Physics2D.IgnoreCollision(boxCollider, circleCollider, true);
    }

    public void Deactivate()
    {
        //Make the RB static so it doesn't move
        theRb.bodyType = RigidbodyType2D.Static;
        //Detach the vase from the person
        pickUppable = GetComponent<PickUppable>();
        pickUppable.Detach();
        //Disable everything so the player doesn't collide
        theVase.isStatic = true;
        boxCollider.enabled = false;
        circleCollider.enabled = false;

    }
}
