using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMove : MonoBehaviour
{
    //This is just a generic moving platform script but instead of platforms they're rats
    
    private Vector3 startPos;
    private Vector3 targetPos;
    public float verticalDistance;
    public float horizontalDistance;
    public float speed;
    private bool movingToTarget;
    private bool facingRight;


    //Sets up the values for the starting position
    void Start()
    {
        startPos = transform.position;
        targetPos = new Vector3(startPos.x+ horizontalDistance, startPos.y + verticalDistance, startPos.z);

        movingToTarget = true;
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        
        //If the current position is equal to the set target position, then move to the start position
        if (transform.position == targetPos)
        {
            movingToTarget = false;
        }
        
        //Else if the gameobject is equal to the starting position, then start moving back to the target
        else if (transform.position == startPos)
        {
            movingToTarget = true;
        }

        if (movingToTarget == false)
        {
            //Move gameobject towards the start position, and make it face left
            transform.position = Vector2.MoveTowards(transform.position, startPos, step);

            if (!facingRight)
            {
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
                facingRight = !facingRight;
            }
        }
        else if (movingToTarget)
        {
            //Move gameobject towards the target position, and make it face right
            transform.position = Vector2.MoveTowards(transform.position, targetPos, step);
            
            if (facingRight)
            {
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
                facingRight = !facingRight;
            }
        }
    }
}
