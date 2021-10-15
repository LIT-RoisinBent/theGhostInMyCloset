using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public GhoulController ghoulController;
    public DialogueTrigger dialogueTrigger;
    
    public Animator dialogueAnimator;
    public Animator ghoulAnimator;
    public Text nameText;
    public Text dialogueText;
    private Queue<string> sentences;
    public bool dialogueRunning;
    public bool finalDialogue;
    private int animValue;
    public int listNum;

    void Awake()
    {
        //Sets instance to this gameobject, and if instance is already assigned, destroy the gameobject
        //(to ensure there aren't multiple instances assigned when the scene is reloaded)
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
        //Sets listNum, which controls where the dialogue/ghoul is, to 0
        listNum = 0;
        //Makes a queue of strings for dialogue
        sentences = new Queue<string>();
    }

    //Called every time you entire a dialogue trigger box
    public void StartDialogue(Dialogue dialogue)
    {
        //Clears any sentences that were loaded in the queue from previous dialogues
        sentences.Clear();
        dialogueRunning = true;
        //Calls the sound file when ghoul appears
        LevelManager.instance.SfxManager("ghoul");
        //Calls OnTheMove, which passes in listNum so ghoul knows which waypoint to go to
        ghoulController.OnTheMove(listNum);
        //Assigns animation bools to the dialogue box and ghoul appears
        dialogueAnimator.SetBool("IsOpen", true);
        ghoulAnimator.SetBool("Appear", true);
        //Sets the UI text to nothing
        dialogueText.text = "";
        //Sets the UI name to the character's name
        nameText.text = dialogue.name;
        
        //Loads each sentence that was passed from the dialogue trigger into the sentences queue
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        //If there's no more sentences, end the dialogue
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        //Moves each sentence through the queue
        string sentence = sentences.Dequeue();

        //Ensures it isn't still typing when you press the "next" UI button, calls TypeSentence and GhoulAnimations
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        StartCoroutine(GhoulAnimations());
    }

    IEnumerator TypeSentence(string sentence)
    {
        //Dialogue text starts with nothing
        dialogueText.text = "";
        
        //Puts each individual letter into a character array
        foreach (char letter in sentence.ToCharArray())
        {
            //Add individual character into the dialogue text, gives a "typing" effect
            dialogueText.text += letter;
            yield return null;
        }
    }
    public void EndDialogue()
    {
        LevelManager.instance.SfxManager("ghoul");
        dialogueAnimator.SetBool("IsOpen", false);

        //If finalDialogue becomes true, start the final battle (which is different to what normally happens)
        if (finalDialogue)
        {
            Debug.Log("final battle is true");
            FinalBattle();
            LevelManager.instance.MusicManager("creepy");
        }
        //Else, continue as normal
        else
        {
            ghoulAnimator.SetBool("Appear", false);
        }
        
        Debug.Log("End of conversation");
        dialogueRunning = false;
    }

    IEnumerator GhoulAnimations()
    {
        //Assigns the animValue a random number between 1 and 4
        animValue = Random.Range(1, 5);
        
        //Assigns the "Faces" number to the animValue, each number does a different animation
        ghoulAnimator.SetInteger("Faces", animValue);

        yield return new WaitForSeconds(2f);
        //After waiting for the animation to play out, assign it back to 0, which is "floating"
        ghoulAnimator.SetInteger("Faces", 0);
        yield return new WaitForSeconds(1f);
        yield return null;
    }

    public void FinalBattle()
    {
        Debug.Log("evilMode");
        //Stops regular animations for Ghoul
        StopCoroutine(GhoulAnimations());
        //Changes animation, calls the coroutine that makes Ghoul move towards player
        ghoulAnimator.SetBool("evilMode", true);
        ghoulController.CoroutineToKill();
        finalDialogue = false;
    }
}
