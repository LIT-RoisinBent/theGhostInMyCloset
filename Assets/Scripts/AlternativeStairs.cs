using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternativeStairs : MonoBehaviour
{
    //NOT MADE BY ME, MADE BY JOHN HANNAFIN
    
    // Two private variables that are set in the Start function to hold 
    // a reference the the RigidBody2D component and the Character_Controller
    // script component that are attached to the MPC.
    private Rigidbody2D theRB;
    public Character_Controller player;
    public GameObject thePlayer;

    // The gravity scale as set on the rigidbody
    private float rbGravityScale;

    // The step size when climbing up/down the stairs
    public float stepSize = 0.5f;

    // What Layer is the Stairs on
    public LayerMask whatIsStairs;

    // Start is called before the first frame update
    void Start()
    {
        theRB = thePlayer.GetComponent<Rigidbody2D>();

        // Get the gravity scale that is set in the RigidBody
        rbGravityScale = theRB.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        // Check to see if the player is pressing the climb up or down buttons
        bool climbDown = Input.GetKey(KeyCode.S);
        bool climbUp = Input.GetKey(KeyCode.W);

        /*
         * This is important. Let's assume that the Stairs in on a Layer called Stairs. A stairs
         * game object is nothing more that a box collider that is set to be a trigger and is:
         *      - the width of the stairs
         *      - is the height of the stairs PlUS the height of the character
         *      - the box collider is "pulled up" slightly so that the bottom of the box collider
         *        is not touching the ground.
         *    
         * The player.grounded variable is true if the groundcheck (which is at the feet of the character)
         * is touching a collider that is in the "whatIsGround" LayerMask. The Ground layer and the Stairs
         * layer are both in this LayerMask. So the character is "on the ground" if it's feet are touching
         * the ground OR the stairs.
         * 
         * If the character is grounded I disable the gravity scale so that when the character is at the top
         * of the stiars they would fall down.
         */
        if (player.grounded)
        {
            theRB.gravityScale = 0;
        }
        else
        {
            theRB.gravityScale = rbGravityScale;
        }

        // Check to see if players groundcheck (feet) are colliding with anything in the whatIsGround layer mask
        // i.e. ground or Stairs. If it is then we can go down.
        Collider2D canGoDown = Physics2D.OverlapCircle(player.groundcheck.position, player.groundRadius, whatIsStairs);

        // Check if the character head is colliding with anything in the whatIsStairs layer mask
        // i.e. just Stairs. If it is then we can go up.
        Collider2D canGoUp = Physics2D.OverlapCircle(player.headcheck.position, player.headRadius, whatIsStairs);

        if (canGoDown && climbDown)
        {
            ChangeCharactersYPosition(stepSize * -1);
        }
        else if (canGoUp && climbUp)
        {
            ChangeCharactersYPosition(stepSize);
        }
    }


    private void ChangeCharactersYPosition(float amount)
    {
        Vector3 charsPos = transform.position;
        charsPos.y += amount;
        transform.position = charsPos;
    }
}