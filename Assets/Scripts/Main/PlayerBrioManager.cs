// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  11/02/2021

using UnityEngine;
using UnityEngine.SceneManagement;

// Manage the Player's Brio
// Not present for TD minigames (need dMan, et al.)
public class PlayerBrioManager : MonoBehaviour
{
    public Animator playerAnim;
    public Animator sceneTrans;
    public ControllerSupport contSupp;
    public DialogueManager dMan;
    public PauseGame pause;
    public QuestManager qMan;
    public Scene scene;
    public Sprite portPicDan;
    public Sprite portPicGreatTree;
    public Sprite portPicUnknown;
    public UIManager uMan;

    public bool bAvoidFatigue;
    public bool bRestoreOverTime;

    public float diffMaxAndCurrent;
    public float playerMaxBrio;
    public float playerCurrentBrio;

    public int fatiguedCounter;

    private string[] basicWarningLines;
    private string[] treeKnownWarningLines;
    private string[] treeUnknownWarningLines;

    void Start ()
    {
        // Initializers
        scene = SceneManager.GetActiveScene();

        // Set warning dialogue
        basicWarningLines = new string[]
        {
            "Phew.. I am le tired...",
            "I may need a nap or something."
        };

        treeKnownWarningLines = new string[]
        {
            "Even with my aid, you have much to learn.",
            "Your brio, your energy, is precious.",
            "Invest it well.",
            "Every day..."
        };

        treeUnknownWarningLines = new string[]
        {
            "You are new to this world my child.",
            "You must learn to use your resources more efficiently.",
            "Find me, and I will help you."
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

        //InvokeRepeating("BrioReport", 1f, 1f);
	}
	
	void Update ()
    {
        // Restore on certain scenes / conditions
        if (bRestoreOverTime &&
            (!pause.bPauseActive &&
             !dMan.bDialogueActive &&
             !sceneTrans.isActiveAndEnabled))
            BasicRestorePlayer();
        
        // Temp solution to give Brio
        if (Input.GetKeyUp(KeyCode.X) ||
            (contSupp.ControllerBumperLeft("down") &&
             contSupp.ControllerBumperRight("down")))
        {
            RestorePlayer(50);
            uMan.UpdateBrio();
        }
    }

    public void BrioReport()
    {
        Debug.Log(playerCurrentBrio);
    }

    // Removes Brio
    public void FatiguePlayer (float brioToRemove)
    {
        if (!bAvoidFatigue)
            playerCurrentBrio -= brioToRemove;

        ShouldRestoreOverTime();
        CheckForZeroBrio();
    }

    // Adds Brio
    public void RestorePlayer (float brioToGive)
    {
        playerCurrentBrio += brioToGive;

        ShouldRestoreOverTime();
    }

    // Adds Brio once below half (temp algo)
    public void BasicRestorePlayer()
    {
        diffMaxAndCurrent = playerMaxBrio - playerCurrentBrio;

        playerCurrentBrio += 0.01f;
        uMan.UpdateBrio();

        ShouldRestoreOverTime();
    }

    // Increase the Max Brio
    public void IncreaseMaxBrio(float increaseAmount)
    {
        playerMaxBrio = playerMaxBrio + increaseAmount;

        ShouldRestoreOverTime();
    }

    // Checks for no brio
    public void CheckForZeroBrio()
    {
        if (playerCurrentBrio <= 0)
        {
            // Not saving atm; will reset with each game reload
            fatiguedCounter++;

            if (fatiguedCounter >= 3)
            {
                if (qMan &&
                    qMan.questsStarted[5])
                {
                    dMan.dialogueLines = treeKnownWarningLines;
                    dMan.portPic = portPicGreatTree;
                }
                else
                {
                    dMan.dialogueLines = treeUnknownWarningLines;
                    dMan.portPic = portPicUnknown;
                }
            }
            else
            {
                dMan.dialogueLines = basicWarningLines;
                dMan.portPic = portPicDan;
            }

            dMan.ShowDialogue();

            if (playerAnim)
                playerAnim.SetBool("bIsWalking", false);

            playerCurrentBrio = 1;
        }
    }

    public void ShouldRestoreOverTime()
    {
        if (playerCurrentBrio < diffMaxAndCurrent)
            bRestoreOverTime = true;
        else
            bRestoreOverTime = false;
    }
}
