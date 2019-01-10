// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  01/10/2019

using UnityEngine;
using UnityEngine.SceneManagement;

// Manage the Player's Brio
public class PlayerBrioManager : MonoBehaviour
{
    private Animator pAnim;
    private DialogueManager dMan;
    public GameObject pause;
    public Scene scene;
    public Sprite portPic;
    public UIManager uiMan;

    public float diffMaxAndCurrent;
    public float playerMaxBrio;
    public float playerCurrentBrio;

    private string[] warningLines;

    void Start ()
    {
        // Initializers
        dMan = FindObjectOfType<DialogueManager>();
        pause = GameObject.FindGameObjectWithTag("Pause");
        scene = SceneManager.GetActiveScene();
        uiMan = FindObjectOfType<UIManager>();

        if (scene.name != "GuessWhoColluded")
        {
            pAnim = GetComponent<Animator>();
        }

        // Setting the brio
        if (PlayerPrefs.GetInt("Transferring") == 1)
        {
            playerMaxBrio = PlayerPrefs.GetFloat("TransferBrioMax");
            playerCurrentBrio = PlayerPrefs.GetFloat("TransferBrio");
        }
        else if (PlayerPrefs.GetInt("Saved") == 1)
        {
            playerMaxBrio = PlayerPrefs.GetFloat("BrioMax");
            playerCurrentBrio = PlayerPrefs.GetFloat("Brio");
        }
        else
        {
            playerMaxBrio = 50;
            playerCurrentBrio = 50;
        }

        // Set warning dialogue
        warningLines = new string[2];
        warningLines[0] = "Phew.. I am le tired...";
        warningLines[1] = "I may need a nap or something.";
	}
	
	void Update ()
    {
        // Warning Message when Brio is out
        if (playerCurrentBrio <= 0)
        {
            // Opens Dialogue Manager and gives a warning
            dMan.dialogueLines = warningLines;
            dMan.portPic = portPic;
            dMan.ShowDialogue();
            if (scene.name != "GuessWhoColluded")
            {
                pAnim.SetBool("bIsWalking", false);
            }
            playerCurrentBrio = 1;
        }
        
        // Restore on certain scenes / conditions
        if (pause.transform.localScale == Vector3.one ||
            dMan.bDialogueActive ||
            FindObjectOfType<SceneTransitioner>().bAnimationToTransitionScene == true)
        {
            // Avoid basic restore
        }
        else if (scene.name == "Chp1")
        {
            BasicRestorePlayer();
        }

        // Temp solution to give Brio
        if (Input.GetKeyUp(KeyCode.X) ||
            Input.GetKeyUp(KeyCode.JoystickButton2))
        {
            RestorePlayer(50);
            uiMan.bUpdateBrio = true;
        }
    }

    // Removes Brio
    public void FatiguePlayer (float damageToGive)
    {
        playerCurrentBrio -= damageToGive;
    }

    // Adds Brio
    public void RestorePlayer (float brioToGive)
    {
        playerCurrentBrio += brioToGive;
    }

    // Adds Brio once below half (temp algo)
    public void BasicRestorePlayer()
    {
        diffMaxAndCurrent = playerMaxBrio - playerCurrentBrio;

        if ((playerCurrentBrio < diffMaxAndCurrent) && !dMan.bDialogueActive)
        {
            playerCurrentBrio += 0.01f;
            uiMan.bUpdateBrio = true;
        }
    }

    // Increase the Max Brio
    public void IncreaseMaxBrio(float increaseAmount)
    {
        playerMaxBrio = playerMaxBrio + increaseAmount;
    }
}
