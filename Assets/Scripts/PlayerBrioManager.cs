// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  02/24/2020

using UnityEngine;
using UnityEngine.SceneManagement;

// Manage the Player's Brio
public class PlayerBrioManager : MonoBehaviour
{
    public Animator playerAnim;
    public Animator sceneTrans;
    public ControllerSupport contSupp;
    public DialogueManager dMan;
    public PauseGame pause;
    public Scene scene;
    public Sprite portPic;
    public UIManager uMan;

    public bool bRestoreOverTime;

    public float diffMaxAndCurrent;
    public float playerMaxBrio;
    public float playerCurrentBrio;

    private string[] warningLines;

    void Start ()
    {
        // Initializers
        scene = SceneManager.GetActiveScene();

        // Set warning dialogue
        warningLines = new string[]
        {
            "Phew.. I am le tired...",
            "I may need a nap or something."
        };

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

        if (scene.name == "Chp0" ||
            scene.name == "Chp1")
            bRestoreOverTime = true;
	}
	
	void Update ()
    {
        // Restore on certain scenes / conditions
        if (bRestoreOverTime &&
            (!pause.bPauseActive &&
             !dMan.bDialogueActive &&
             !sceneTrans.isActiveAndEnabled))
            BasicRestorePlayer();

        // TODO: may not work if controller uses left joystick button
        // Temp solution to give Brio
        if (Input.GetKeyUp(KeyCode.X) ||
            //Input.GetKeyUp(KeyCode.JoystickButton2))
            contSupp.ControllerRightJoystickButton("up"))
        {
            RestorePlayer(50);
            uMan.UpdateBrio();
        }
    }

    // Removes Brio
    public void FatiguePlayer (float brioToRemove)
    {
        playerCurrentBrio -= brioToRemove;

        CheckIfROT();
        CheckForZeroBrio();
    }

    // Adds Brio
    public void RestorePlayer (float brioToGive)
    {
        playerCurrentBrio += brioToGive;

        CheckIfROT();
    }

    // Adds Brio once below half (temp algo)
    public void BasicRestorePlayer()
    {
        diffMaxAndCurrent = playerMaxBrio - playerCurrentBrio;

        playerCurrentBrio += 0.01f;
        uMan.UpdateBrio();

        CheckIfROT();
    }

    // Increase the Max Brio
    public void IncreaseMaxBrio(float increaseAmount)
    {
        playerMaxBrio = playerMaxBrio + increaseAmount;

        CheckIfROT();
    }

    // Checks for no brio
    public void CheckForZeroBrio()
    {
        if (playerCurrentBrio <= 0)
        {
            // Opens Dialogue Manager and gives a warning
            dMan.dialogueLines = warningLines;
            dMan.portPic = portPic;
            dMan.ShowDialogue();

            if (playerAnim)
                playerAnim.SetBool("bIsWalking", false);

            playerCurrentBrio = 1;
        }
    }

    public void CheckIfROT()
    {
        if (playerCurrentBrio < diffMaxAndCurrent)
            bRestoreOverTime = true;
        else
            bRestoreOverTime = false;
    }
}
