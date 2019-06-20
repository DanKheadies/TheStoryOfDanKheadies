﻿// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 03/08/2018
// Last:  06/11/2019

using UnityEngine;
using UnityEngine.SceneManagement;

// Assigned to Interactable Objects (i.e. on an item)
public class ItemManager : MonoBehaviour
{
    public Animator pAnim;
    public DialogueManager dMan;
    public GameObject player;
    public Inventory inv;
    public Item item;
    public PauseGame pause;
    public Scene scene;
    public Sprite portPic;
    public TouchControls touches;
    public UIManager uMan;

    public GameObject greenBud;
    public GameObject orangeBud;
    public GameObject purpleBud;
    public GameObject whiteBud;
    public GameObject vrGoggles;

    public GameObject homeVRGoggles;

    public bool bAcquired; // Checks & Balances
    public bool bDoneAcquiring; // Checks & Balances
    public bool bHasEntered;
    public bool bHasExited;

    public string[] AcquireItem;

    void Start()
    {
        // Initializers
        dMan = FindObjectOfType<DialogueManager>();
        inv = FindObjectOfType<Inventory>();
        pAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        pause = FindObjectOfType<PauseGame>();
        player = GameObject.FindGameObjectWithTag("Player");
        scene = SceneManager.GetActiveScene();
        touches = FindObjectOfType<TouchControls>();
        uMan = FindObjectOfType<UIManager>();

        // 04/07/2018 DC TODO -- Extract to individual scene files
        if (scene.name == "Chp0")
        {
            vrGoggles = GameObject.Find("VR.Goggles");

            homeVRGoggles = GameObject.Find("HomeVRGoggles");
        }
        else if (scene.name == "Chp1")
        {
            greenBud = GameObject.Find("Cannabis.Bud.Green");
            orangeBud = GameObject.Find("Cannabis.Bud.Orange");
            purpleBud = GameObject.Find("Cannabis.Bud.Purple");
            whiteBud = GameObject.Find("Cannabis.Bud.White");
            vrGoggles = GameObject.Find("VR.Goggles");

            homeVRGoggles = GameObject.Find("HomeVRGoggles");
        }

        // Check for VR Goggles & hide if in inventory
        if (scene.name == "Chp1")
        {
            for (int i = 0; i < PlayerPrefs.GetInt("ItemTotal"); i++)
            {
                string savedItem = PlayerPrefs.GetString("Item" + i);
                savedItem = savedItem.Substring(0, savedItem.Length - 7);
                
                if (savedItem == "VR.Goggles")
                {
                    homeVRGoggles.transform.localScale = Vector3.zero;
                }
            }
        }
    }

    void Update()
    {
        if (bHasEntered && 
            !bHasExited && 
            !dMan.bDialogueActive && 
            !dMan.bPauseDialogue &&
            !pause.bPausing &&
            !pause.bPauseActive &&
            (touches.bAaction ||
             Input.GetButtonDown("Action") ||
             (Input.GetButtonDown("DialogueAction") &&
              !uMan.bControlsActive)))
        {
            InteractWithItem();
        }

        if (bAcquired && 
            dMan.bDialogueActive)
        {
            bAcquired = false;
            bDoneAcquiring = true;
        }
        if (!dMan.bDialogueActive && 
            !bAcquired && 
            bDoneAcquiring)
        {
            HideItem();
            bDoneAcquiring = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bHasEntered = true;
            bHasExited = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bHasEntered = false;
            bHasExited = true;
        }
    }

    public void InteractWithItem()
    {
        if (inv.items.Count < inv.totalItems)
        {
            pAnim.Play("Acquire");

            // Display and add item to inventory
            if (this.name == "Cannabis.Bud.Green")
            {
                greenBud.transform.localScale = Vector3.one;
                Inventory.instance.Add(item);
            }
            else if (this.name == "Cannabis.Bud.Orange")
            {
                orangeBud.transform.localScale = Vector3.one;
                Inventory.instance.Add(item);
            }
            else if (this.name == "Cannabis.Bud.Purple")
            {
                purpleBud.transform.localScale = Vector3.one;
                Inventory.instance.Add(item);
            }
            else if (this.name == "Cannabis.Bud.White")
            {
                whiteBud.transform.localScale = Vector3.one;
                Inventory.instance.Add(item);
            }
            else if (this.name == "HomeVRGoggles")
            {
                vrGoggles.transform.localScale = Vector3.one;
                Inventory.instance.Add(item);
                
                // Disables the item's interactivity and "removes"
                // Note: cannabis is taken care of in CannabisPlant.cs
                this.GetComponent<BoxCollider2D>().enabled = false;
                this.transform.localScale = Vector2.zero;
            }

            dMan.portPic = portPic;
            dMan.dialogueLines = AcquireItem;
            dMan.ShowDialogue();
        }
        else if (inv.items.Count >= inv.totalItems)
        {
            dMan.portPic = portPic;
            string[] outOfSpace = { "Rats.. We have no more space for stuff." };
            dMan.dialogueLines = outOfSpace;
            dMan.ShowDialogue();
        }

        bAcquired = true;
        dMan.PauseDialogue();
        touches.bAaction = false;
    }

    public void HideItem()
    {
        if (scene.name == "Chp0")
        {
            vrGoggles.transform.localScale = Vector3.zero;
        }
        else if (scene.name == "Chp1")
        {
            greenBud.transform.localScale = Vector3.zero;
            orangeBud.transform.localScale = Vector3.zero;
            purpleBud.transform.localScale = Vector3.zero;
            whiteBud.transform.localScale = Vector3.zero;
            vrGoggles.transform.localScale = Vector3.zero;
        }
    }
}
