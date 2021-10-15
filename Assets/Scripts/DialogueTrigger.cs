using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueDead dialogueDead;
    private LeverControl leverScript;
    
    public GameObject theTriggerGO;
    public Collider2D trigger;
    public GameObject theLever;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Calls usual dialogue
        TriggerDialogue();
        
        //If the dialogue trigger is called "DialogueTrigger9 (the end battle), do these things
        if (this.gameObject.name == "DialogueTrigger9")
        {
            //Assign the levercontrol script, call LeverEnabled, sets the finalDialogue bool to true in DialogueManager, stops music
            leverScript = theLever.GetComponent<LeverControl>();
            LevelManager.instance.pressE.enabled = true;
            leverScript.LeverEnabled();
            DialogueManager.instance.finalDialogue = true;
            LevelManager.instance.MusicManager("stop");
        }
    }

    public void TriggerDialogue()
    {
        //Find the dialogue manager, call StartDialogue, passes in the dialogue assigned to this trigger, then call
        //DestroyTrigger()
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        DestroyTrigger();
    }
    
    public void DestroyTrigger()
    {
        //Make the listNum go up by 1, so the position for Ghoul moves
        DialogueManager.instance.listNum++;
        Debug.Log("listnum is " + DialogueManager.instance.listNum);
        //Disable the trigger so it won't get called again
        theTriggerGO.SetActive(false);
    }

    //Calls the dialogue when player is dead
    public void DeadDialogue()
    {
        if (LevelManager.instance.isDead)
        {
            dialogueDead.StartDialogue(dialogue);
        }
    }
}
