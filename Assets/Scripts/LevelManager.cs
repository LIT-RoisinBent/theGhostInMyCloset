using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public DialogueDead dialogueDead;
    public DialogueTrigger dialogueTrigger;
    public DisableCollisions disableCollisions;
    public Animator charAnimate;
    
    public List<string> keys = new List<string>();
    public List<string> rats = new List<string>();
    public List<GameObject> keyGameObjects = new List<GameObject>();
    public List<int> puzzle1 = new List<int>();
    
    public string doorID;
    public Slider slider;
    public TextMeshProUGUI pressE;
    public GameObject dialogueTrigger3;
    public GameObject dialogueTrigger4;
    public GameObject dialogueTrigger5;
    public GameObject dialogueTrigger10;

    private GameObject theKey;
    
    public int maxHealth = 100;
    public int currentHealth;
    public bool isDead;
    public bool startCreepyMusic;
    public Gradient gradient;
    public Image fill;
    public Image hasKey;

    public AudioSource sfxSource;
    public AudioSource musicSource;
    public AudioClip keySound;
    public AudioClip doorSound;
    public AudioClip ratSound;
    public AudioClip ghoulFade;
    public AudioClip creepyMusic;
    public AudioClip cheerieMusic;

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

        //Sets all values ready for game start
        isDead = false;
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
        hasKey.enabled = false;
        pressE.enabled = false;
        musicSource.clip = cheerieMusic;
        musicSource.Play();
        HideAll();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("quit the game");
            Application.Quit();
        }
    }

    public void SetMaxHealth(int health)
    {
        //Sets the slider's max value to maximum health, sets the UI health slider to what the player's health is
        slider.maxValue = maxHealth;
        slider.value = health;
        //Assigns the gradient to the UI healthbar (will change colour depending on how much damage is done)
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        //Updates the UI slider to player's current health, evaluate what colour the healthbar should be
        slider.value = currentHealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);

        if (health <= 0)
        {
            isDead = true;
            charAnimate.SetBool("dead", true);
            charAnimate.Play("death");
            disableCollisions.DisableColliders();
            dialogueTrigger.DeadDialogue();

            Debug.Log("YOU ARE DEAD");
        }
    }

    public void TakeDamage(int damage)
    {
        //Current health gets subtracted by the damage taken, call SetHealth with the updated current health
        currentHealth -= damage;
        SetHealth(currentHealth);
    }

    //Hide all keys, hide specific dialogue triggers (they enable when you complete requirements)
    private void HideAll()
    {
        foreach (var i in keyGameObjects)
        {
            i.SetActive(false);
        }
        dialogueTrigger3.SetActive(false);
        dialogueTrigger4.SetActive(false);
        dialogueTrigger10.SetActive(false);
    }
    
    //Shows the key UI
    public void ShowKeysUI()
    {
        hasKey.enabled = true;
    }
    
    //Hides the key UI
    public void HideKeysUI()
    {
        hasKey.enabled = false;
    }

    //For when you kill the rats for door requirements
    public void RemoveRatsFromList(int whatRat)
    {
        //Removes specific rat player killed from the rats list
        rats.RemoveAt(whatRat);
        RemainingRatsCheck();
    }

    //Checks if the index of the rats list are certain numbers (requirements achieved)
    private void RemainingRatsCheck()
    {
        if (rats.Count == 5)
        {
            keyGameObjects[0].SetActive(true);
            dialogueTrigger3.SetActive(true);
        }
        
        else if (rats.Count == 2)
        {
            keyGameObjects[1].SetActive(true);
            dialogueTrigger4.SetActive(true);
        }
    }

    //Same idea as the rat checks, if the player correctly puts the vase on the right pressure pad, add vase to the list,
    //and checks if all vases are placed correctly (will show key if requirement achieved)
    public void PressurePadPuzzle(int vaseID)
    {
        Debug.Log("added to list");
        puzzle1.Add(vaseID);
        
        if (puzzle1.Count == 3)
        {
            Debug.Log("door opens");
            keyGameObjects[2].SetActive(true);
        }
    }

    //Function to show the final dialogue, which appears after a certain requirement
    public void End()
    {
        dialogueTrigger10.SetActive(true);
    }
    
    //Makes specific sounds happen when player collides with specific gameobjects. The sound name is passed in from other
    //scripts, so if the string matches, play that specific sound
    public void SfxManager(string sound)
    {
        if (sound == "key")
        {
            sfxSource.clip = keySound;
            sfxSource.Play();
        }
        
        else if (sound == "door")
        {
            sfxSource.clip = doorSound;
            sfxSource.Play();
        }
        
        else if (sound == "rat")
        {
            sfxSource.clip = ratSound;
            sfxSource.Play();
        }
        
        else if (sound == "ghoul")
        {
            sfxSource.clip = ghoulFade;
            sfxSource.Play();
        }
    }

    //Controls the music on a separate audio source
    public void MusicManager(string sound)
    {
        if (sound == "creepy")
        {
            musicSource.clip = creepyMusic;
            musicSource.volume = .3f;
            musicSource.Play();
        }
        else if(sound == "stop")
        {
            Debug.Log("sound stopped");
            musicSource.Stop();
        }
    }
}
