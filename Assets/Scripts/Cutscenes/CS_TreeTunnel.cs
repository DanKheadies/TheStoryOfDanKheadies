// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 02/08/2020
// Last:  02/27/2020

using UnityEngine;
using UnityEngine.UI;

public class CS_TreeTunnel : MonoBehaviour
{
    public Camera mainCamera;
    public DialogueManager dMan;
    public GameObject dBox;
    public GameObject player;
    public GameObject warpTreeTunnel;
    public Image dPic;
    public ImageStrobe dArrow;
    public Inventory inv;
    public PlayerBrioManager brio;
    public SaveGame save;
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
            "Treat everything in our world as 'real' and 'truth'..",
            "Compile a database so rich and extensive about the world that you",
            "arm yourself with skills and knowledge for any situation.",
            "With enough time and an open mind, you will come to understand",
            "the true disposition of your existence. The 'real' value of reality.",
            "After all, it's all real. Think about it...",
            "Haven't Luke Skywalker and Santa Claus affected your life more than",
            "most of the people in your life?",
            "Whether Jesus is real or not, he's had a bigger impact on the world",
            "than any of us have. And the same can be said for South Park and",
            "Superman and Harry Potter. They've changed our lives...",
            "Changed the way we act in the world.",
            "Doesn't that make them kind of real?",
            "They might be imaginary, but they're more important than most of",
            "us here. And they're all gonna be around here long after we're dead.",
            "So, in a way, those things are more realer than any of us..."
        };

        dMan.dialogueLines = dialogueLines;
        dMan.currentLine = 0;
        dText.text = dialogueLines[dMan.currentLine];
        dPic.sprite = portPic[0];
        dBox.transform.localScale = Vector3.one;
        dMan.closingAction = "WarpOut";

        // Hide BrioBar & Pause Button (Opac)
        uMan.HideBrioAndButton();

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

    public void ScaleAnimation()
    {
        if (sceneAni == null) return;
        
        var height = sceneAni.sprite.bounds.size.y;

        var worldScreenHeight = mainCamera.orthographicSize * 2.0;

        sceneAni.transform.localScale = new Vector3(
            (float)worldScreenHeight / height * 
                mainCamera.GetComponent<AspectUtility>()._wantedAspectRatio,
            (float)worldScreenHeight / height,
            1);
    }

    public void CompleteCutscene()
    {
        // Reward 
        PlayerPrefs.SetString("TransferActions", "Quest5Reward");

        WarpOut();
    }

    public void WarpOut()
    {
        warpTreeTunnel.GetComponent<BoxCollider2D>().enabled = true;
        warpTreeTunnel.GetComponent<SceneTransitioner>().bAnimationToTransitionScene = true;
        
        // Save Transfer Values
        save.SaveBrioTransfer();
        save.SaveInventoryTransfer();
        PlayerPrefs.SetInt("Transferring", 1);
        PlayerPrefs.SetString("TransferScene", warpTreeTunnel.GetComponent<SceneTransitioner>().BetaLoad);

        // Stop the player from bringing up the dialog again
        dMan.gameObject.transform.localScale = Vector3.zero;

        // Set player scale to start transition
        player.transform.localScale = Vector3.one;

        // Freeze animation
        sceneAni.GetComponent<Animator>().enabled = false;

        // Set the transition animation via the camera's scale
        var height = mainCamera.GetComponentInChildren<SpriteRenderer>().sprite.bounds.size.y;
        var worldScreenHeight = mainCamera.orthographicSize * 2.0;

        mainCamera.GetComponentInChildren<Transform>().localScale = new Vector3(
            (float)worldScreenHeight / height,
            (float)worldScreenHeight / height,
            1);
    }
}
