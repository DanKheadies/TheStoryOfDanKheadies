// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 09/08/2021
// Last:  09/23/2021

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CS_Wealthy : MonoBehaviour
{
    public Camera mainCamera;
    public DialogueManager dMan;
    public GameObject dBox;
    public GameObject player;
    public GameObject warpWealthy;
    public Image dPic;
    public ImageStrobe dArrow;
    public Inventory inv;
    public MusicManager mMan;
    public PlayerBrioManager brio;
    public SaveGame save;
    public ScreenFader sFader;
    public SFXManager SFXMan;
    public Sprite[] portPic;
    public SpriteRenderer sceneAni;
    public Text dText;
    public UIManager uMan;

    public float strobeTimer;
    public float timer;

    public string[] dialogueLines;


    void Start()
    {
        strobeTimer = 1.0f;
        timer = 0.333f;

        // Restrict player movement
        player.GetComponent<PlayerMovement>().bStopPlayerMovement = true;

        dialogueLines = new string[] {
            "I read this New Testament line decades ago..",
            "I could never understand it."
        };

        dMan.dialogueLines = dialogueLines;
        dMan.currentLine = 0;
        dText.text = dialogueLines[dMan.currentLine];
        dPic.sprite = portPic[0];
        dBox.transform.localScale = Vector3.one;
        dMan.closingAction = "WarpOut";

        // Hide BrioBar & Pause Button (Opac)
        uMan.HideBrioAndButton();

        // Hide controls
        uMan.HideControls();

        // Get transfer items (if any)
        inv.LoadInventory("transfer");

        ScaleAnimation();
    }

    void Update()
    {
        if (strobeTimer > 0)
        {
            strobeTimer -= Time.deltaTime;

            if (strobeTimer <= 0)
            {
                StartCoroutine(dArrow.Strobe());
                dMan.bDialogueActive = true;

                // Sound Effect
                SFXMan.sounds[2].PlayOneShot(SFXMan.sounds[2].clip);
            }
        }
    }

    public void AdjustCamera()
    {
        ScaleAnimation();
        StartCoroutine(DelayAdjustCamera());
    }

    public IEnumerator DelayAdjustCamera()
    {
        // Slight deplay allows the DialogueManager to position itself "correctly" while the animation scales "correctly"
        yield return new WaitForSeconds(0.1f);

        mainCamera.rect = new Rect(0, 0, 1, 1);
    }

    public void ScaleAnimation()
    {
        if (sceneAni == null) return;

        sceneAni.transform.localPosition = new Vector2(0, -2.8f);
        sceneAni.transform.localScale = new Vector3(4.20f, 4.20f, 1);
    }

    public void CompleteCutscene()
    {
        // Reward 
        PlayerPrefs.SetString("TransferActions", "Quest14Reward");

        WarpOut();
    }

    public void WarpOut()
    {
        warpWealthy.GetComponent<BoxCollider2D>().enabled = true;
        // Using fader below; inactivating Sprite Renderer on this component
        //warpWealthy.GetComponent<SceneTransitioner>().bAnimationToTransitionScene = true; 

        // Save Transfer Values
        save.SaveBrioTransfer();
        save.SaveInventoryTransfer();
        PlayerPrefs.SetInt("Transferring", 1);
        PlayerPrefs.SetString("TransferScene", warpWealthy.GetComponent<SceneTransitioner>().BetaLoad);

        // Stop the player from bringing up the dialog again
        dMan.gameObject.transform.localScale = Vector3.zero;

        // Set player scale to start transition
        player.transform.localScale = Vector3.one;

        // Freeze animation
        sceneAni.GetComponent<Animator>().enabled = false;

        // Activate the other fader
        sFader.GetComponent<Animator>().SetBool("FadeOut", true);
    }
}
