using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;

public class DialogueDead : MonoBehaviour
{
    public Animator dialogueAnimator;
    public Text nameText;
    public Text dialogueText;
    private Queue<string> sentences;
    public bool dialogueRunning;

    void Awake()
    {
        sentences = new Queue<string>();
    }

    //Called on death
    public void StartDialogue(Dialogue dialogue)
    {
        //Clears any sentences that were loaded in the queue from previous dialogues
        sentences.Clear();
        dialogueRunning = true;
        //Assigns animation bools to the dialogue box
        dialogueAnimator.SetBool("IsOpen", true);
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
        dialogueAnimator.SetBool("IsOpen", false);
        Debug.Log("End of conversation");
        dialogueRunning = false;
    }
}
