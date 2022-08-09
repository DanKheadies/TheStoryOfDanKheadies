// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 09/08/2021
// Last:  08/08/2022

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CS_Wealthy : MonoBehaviour
{
    public Camera mainCamera;
    public DialogueManager dMan;
    public GameObject dBox;
    public GameObject pause;
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
            "Overall, capitalism is intrinsic to the human species.",
            "Capitalism is not something we invented.",
            "It's not even something we discovered.",
            "It is innate to us in every exchange that we have.",
            "When you & I exchange information, I want some information back from you.",
            "I give you information, you give me information.",
            "If we weren't having a good information exchange, 'd go talk to someone else.",
            "So the notion of exchange and keeping track of debits and",
            "credits is built into us as flexible, social animals.",
            "Even if we have very little in common, we can still cooperate.",
            "And what lets us cooperate is we can keep track of debits and credits:",
            "Who put in how much.",
            "That's all free market capitalism is.",
            "So I strongly believe that it is innate to the human species.",
            "And that we are going to create more & more wealth and abundance for everybody.",
            "Everybody can be wealthy.",
            "Everybody can be retired.",
            "Everybody can be successful.",
            "It is merely a question of education and desire..",
            "You have to want it.",
            "If you don't want it, that's fine. Then you opt out of the game.",
            "But don't put down the people who are playing the game.",
            "Because that's the game that keeps you in a comfortable, warm bed at night.",
            "That's the game that keeps a roof over your head.",
            "That's the game that keeps your supermarket stocked.",
            "That's the game that keeps the iPhone buzzing in your pocket.",
            "It is a beautiful game that is worth playing ethically.",
            "Rationally.",
            "Morally.",
            "And socially for the human race.",
            "And it's going to continue to make us all richer and richer.",
            "Until we have massive wealth creation for anybody who wants it.",
            "The US is a very popular country for immigrants because of the American Dream.",
            "Anyone can come here, be poor, work hard, make money, and get wealthy.",
            "But if you get too many takers and not enough makers, society falls apart.",
            "Imagine an organism that has too many parasites.",
            "You actually need some small number of parasites to stay healthy.",
            "And you need a lot of symbiotes, like all the mitochondria in",
            "all of our cells that help us respirate and burn oxygen.",
            "These are symbiotes that help us survive, And we couldn't survive without them.",
            "But to me, those are partners in the wealth creation that creates the human body.",
            "But if you were just filled with parasites, you would die.",
            "Any organism can only withstand a small number of parasites.",
            "And when the parasitic element gets too far out of control, you die.",
            "Everybody can be wealthy.",
            "Everybody can be retired.",
            "Everybody can be successful.",
            "It is merely a question of education and desire.. You have to want it.",
            "I'm talking about ethical, wealth creation.",
            "I'm not talking about monopolies.",
            "I'm not talking about crony capitalism.",
            "I'm not talking about mispriced externalities, like the environment.",
            "I'm talking about free minds and free markets.",
            "Small scale exchange between humans as voluntary.",
            "aND doesn't have an outside impact on others.",
            "But I think that kind of wealth creation, if a society doesn't respect it..",
            "The group does not respect it, that society will plunge into ruin & darkness."
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

        // Lose brio every X seconds while watching
        if (brio.playerCurrentBrio > 1 &&
            pause.transform.localScale != Vector3.one)
        {
            if (!warpWealthy.GetComponent<SceneTransitioner>().bAnimationToTransitionScene)
            {
                brio.FatiguePlayer(0.0025f);
                brio.bRestoreOverTime = false;
                uMan.UpdateBrio(); // Since hidden, don't need?
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
