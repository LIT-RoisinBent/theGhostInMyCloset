using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCollisions : MonoBehaviour
{
    public Collider2D theCollider;

    public void DisableColliders()
    {
        theCollider.enabled = false;
    }
}
