using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FixedJoint2D))] //If GameObject has this script a fixedJoint2D is required, will automatically add one

public class PickUppable : MonoBehaviour
{
    public bool iPickedUp = false;
    private float assetXPosition;
    private FixedJoint2D theFJ;
    private Rigidbody2D objectRB;
    public CharPickUp charPickUp;
    private bool pickingUp;

    // Use this for initialization
    void Awake()
    {
        assetXPosition = transform.position.x;

        theFJ = GetComponent<FixedJoint2D>();
        objectRB = GetComponent<Rigidbody2D>();
        // Make sure the FixedJoint2D component is disabled, needs the same code with .enabled = true to work
        theFJ.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (iPickedUp == false)
        {
            //Locks x position of gameobject
            transform.position = new Vector2(assetXPosition, transform.position.y);
        }
        else
        {
            // Update gameobject so that it locks into this position when not dragged
            assetXPosition = transform.position.x;
        }

    }

    public void Attach(Rigidbody2D charRB)
    {
        Debug.Log("I'm picking up something");
        //Connects the fixedJoint to character rigidBody
        theFJ.connectedBody = charRB; 
        theFJ.enabled = true;
        iPickedUp = true;
    }

    public void Detach()
    {
        Debug.Log("I'm not picking up something");
        //Removes character rigidBody
        theFJ.connectedBody = null; 
        theFJ.enabled = false;
        iPickedUp = false;
    }

}