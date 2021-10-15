using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairClimb : MonoBehaviour
{
    public float climbSpeed;
    public float wallJumpForce;
    public LayerMask stairsLayer;
    public float rayLength;
    public Transform rayCheckOrigin;

    public bool atStairs;
    private float vAxis;
    private float hAxis;
    private Rigidbody2D theRB;
    private Character_Controller player;
    private Animator theAnim;
    
    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        theAnim = GetComponent<Animator>();
        player = GetComponent<Character_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        vAxis = Input.GetAxis("Vertical");
        hAxis = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        StairCheck();
        
        //If at wall & player is pressing up key, move up
        if (vAxis > 0 && atStairs)
        {
            theRB.velocity = Vector2.up * climbSpeed;
        }
        
        //if at stairs, not grounded, and you are pressing left or right
        if (atStairs && !player.grounded && hAxis != 0)
        {
            if (hAxis < 0 && player.facingRight)
            {
                player.Flip();
                theRB.AddForce(new Vector2(-1, 1) * wallJumpForce, ForceMode2D.Impulse);
            }

            if (hAxis > 0 && !player.facingRight)
            {
                player.Flip();
                theRB.AddForce(new Vector2(1, 1) * wallJumpForce, ForceMode2D.Impulse);
            }
        }
    }
    
    private void StairCheck()
    {
        RaycastHit2D rayHit;

        if (player.facingRight)
        {
            rayHit = Physics2D.Raycast(rayCheckOrigin.position, Vector2.right, rayLength, stairsLayer);
            Debug.DrawRay(rayCheckOrigin.position, Vector2.right * rayLength, Color.green);
        }

        else
        {
            rayHit = Physics2D.Raycast(rayCheckOrigin.position, Vector2.left, rayLength, stairsLayer);
            Debug.DrawRay(rayCheckOrigin.position, Vector2.left * rayLength, Color.green);
        }

        if (rayHit.collider != null)
        {
            this.atStairs = true;
        }

        else
        {
            this.atStairs = false;
        }
    }
}
