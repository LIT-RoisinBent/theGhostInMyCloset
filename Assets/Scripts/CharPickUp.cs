using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharPickUp : MonoBehaviour {
    // The transfrom from where the grab ray will start
    public Transform grabRay;

    // The distance within which a draggable item has to be in order for it to be grabbed.
    public float grabDistance = 2.0f;

    // Set to true if you want the ray to be drawn.
    public bool drawRay = false;
    
    // A variable to store the DraggableController that the character is dragging.
    private PickUppable thePickedUpObject;

    private void Awake()
    {
        thePickedUpObject = gameObject.GetComponent<PickUppable>();
    }

    void Update()
    {
        //If you press E, pick up or drop item
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickupOrDrop();
        }
    }

    public void PickupOrDrop()
    {
        //If you aren't picking up anything, pick up something
        if (this.thePickedUpObject == null)
        {
            GrabItem();
        }
        //If you are picking up something, drop it
        else 
        {
            this.thePickedUpObject.Detach();
            this.thePickedUpObject = null;
        }
    }
    
    public void GrabItem()
    {
        //Gets the raycast ready
        Vector2 rayDirection = Vector2.right * transform.localScale.x;
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D rayHit = Physics2D.Raycast(grabRay.position, rayDirection, grabDistance);

        //If you collide with an object
        if (rayHit.collider != null) 
        {
            //If you collide with an object that has a grabbable script
            if (rayHit.collider.gameObject.GetComponent<PickUppable>() != null)
            {
                //Stores collided gameobject in thePickedUpObject
                this.thePickedUpObject = rayHit.collider.gameObject.GetComponent<PickUppable>();

                //Call Attach on PickUppable script
                thePickedUpObject.Attach(GetComponent<Rigidbody2D>()); 
            }
        }
    }

    void OnDrawGizmos()
    {
        if (drawRay)
        {
            Gizmos.color = Color.green;

            Vector2 rayStartPosition = grabRay.position;
            Vector2 rayDirection = Vector2.right * transform.localScale.x * 5;

            Gizmos.DrawLine(rayStartPosition, rayStartPosition + (rayDirection * grabDistance));
        }
    }
}
