using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
   //Allows for larger text boxes that dialogue is assigned at authorTime.
   
   public string name;
   
   [TextArea(3, 10)]
   public string[] sentences;
}
