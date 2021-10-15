using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public string currentDoor;
    public GameObject theDoor;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") //If tagged player
        {
            Debug.Log("the current door is " + currentDoor);
            theDoor = gameObject.GetComponent<GameObject>(); //"theDoor" becomes the instanced GameObject
            
            LevelManager.instance.doorID = currentDoor; //doorID and currentDoor become the same

            foreach (var k in LevelManager.instance.keys.ToList()) //for each key in the keys list
            {
                if (k == LevelManager.instance.doorID) //if k equals the doorID on LvlMgr
                {
                    LevelManager.instance.SfxManager("door");
                    LevelManager.instance.HideKeysUI();
                    Destroy(gameObject);
                    LevelManager.instance.keys.Remove(k); //remove that key from the list
                }

                else if(k != LevelManager.instance.doorID)
                {
                    Debug.Log("wrong key");
                }
            }
        }
    }
}
