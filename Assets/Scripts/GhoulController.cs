using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhoulController : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public Damageable damageable;
    
    public List<Vector3> waypointList = new List<Vector3>();
    private Vector3 nextWaypoint;
    private Transform ghoulTransform;
    private CapsuleCollider2D capsuleCollider2D;
    private SpriteRenderer spriteRenderer;
    private IEnumerator moveAtEnd;
    public bool facingRight;
    public bool ghoulDead;
    public float speed;
    private float step;

    private void Start()
    {
        facingRight = true;
        ghoulDead = false;
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        ghoulTransform = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider2D.enabled = false;
        moveAtEnd = MoveAtEnd();
    }

    //Makes Ghoul move to each dialogue trigger, passes in the listNum
    public void OnTheMove(int listNum)
    {
        //The next waypoint to go to is taken from the index of the waypoint list (a list of vector3s)
        nextWaypoint = waypointList[listNum];
        Debug.Log("moving to " + listNum);
        //Move Ghoul to the next waypoint
        ghoulTransform.localPosition = nextWaypoint;

        //Specific instances where Ghoul should be facing a certain direction
        if (listNum == 4)
        {
            Debug.Log("flipped");
            Flip();
        }
        else if (listNum == 6)
        {
            Flip();
        }
        else if (listNum == 7)
        {
            Flip();
        }
        else if(listNum == 8)
        {
            Flip();
        }
    }
    
    //Flip Ghoul
    public void Flip()
    {
        facingRight = !facingRight;
        Debug.Log(facingRight);
        spriteRenderer.flipX = !facingRight;
        Vector3 theScale = transform.localScale;
        
        theScale.x *= -1;
        
        transform.localScale = theScale;  
    }

    //Starts the coroutine for Ghoul to move at the end
    public void CoroutineToKill()
    {
        damageable.damage = 20;
        capsuleCollider2D.enabled = true;
        StartCoroutine(moveAtEnd);
    }

    IEnumerator MoveAtEnd()
    {
        //While Ghoul isn't dead (no collision with gas), make ghoul move according to the speed set in inspector
        while (!ghoulDead)
        {
            step = speed * Time.deltaTime;
            Debug.Log("coroutine moving");
            Vector3 ghoulPos = transform.position;
            ghoulPos.x += step;
            transform.position = ghoulPos;
            yield return new WaitForSeconds(.01f);

            if (ghoulDead)
            {
                Debug.Log("exited loop");
                break;
            }
        }
    }

    public void Dead()
    {
        //Stop moving Ghoul
        Debug.Log("dead called");
        ghoulDead = true;
        StopAllCoroutines();
        Vector3 ghoulPos = transform.localPosition;
        transform.position = ghoulPos;
    }
}
