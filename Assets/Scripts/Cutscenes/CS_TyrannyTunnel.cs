// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 06/28/2021
// Last:  07/19/2021

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CS_TyrannyTunnel : MonoBehaviour
{
    public Camera mainCamera;
    public DialogueManager dMan;
    public GameObject dBox;
    public GameObject player;
    public GameObject warpTyrannyTunnel;
    public Image dPic;
    public ImageStrobe dArrow;
    public Inventory inv;
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
            "To wit!",
            "I would like to add, as many have already heard, that exhaustion",
            "is the point. They flood us with terrible news, terrible events,",
            "propaganda to make the most vicious and cruelest among us",
            "appear as kings, unstoppable, undefeatable.",
            "The tyrant's greatest weapon is their ability to instill fear",
            "and despair.",
            "I would strongly encourage everyone to look across history.",
            "Look at all the horrific acts, the authoritarian regimes, the dictators",
            "who appeared to be indominable gods, until suddenly, they weren't.",
            "I know it's hard.",
            "But I cannot emphasize enough the power of even a single optimist.",
            "Hope is infectious. The authoritarian seeks to control our perception",
            "of reality. They are very skilled at it.",
            "But just one individual in someone's lives who is a beacon of hope and",
            "positivity can have profound ripple effects on their community.",
            "Despair is the enemy. Without despair, there is only life and action.",
            "In bad times, you must act.",
            "In good times, you must act.",
            "We live, undoubtedly, under a curtain of shit and terror and fear.",
            "The authoritarians are on the rise everywhere.",
            "The single best way that you can combat them is to not despair.",
            "No matter how dire the news makes the world seem, do not despair.",
            "Encourage action, vigilance, and most importantly,",
            "love and community.",
            "It isn't easy.",
            "Lord knows.",
            "Sometimes, it takes everything.",
            "But nothing you can do with your life is more noble than to be a",
            "stalwart defender against despair and hopelessness.",
            "To be the person that even in the darkest hour,",
            "inspires others to turn on their lights, too.",
            "There are more of us than there are of them.",
            "Which is why they put all their energies into making people seem",
            "small, weak, or divide.",
            "They are, in essence, trying to salt the Earth.",
            "To take away that which makes life rich and worth fighting for.",
            "To make you believe this world is dark and doomed and that it",
            "is not even worth challenging them for their dominance over it.",
            "By being someone who inspires hope, by being that rock,",
            "that anchor, that beacon, you give others the strength to",
            "keep getting up, to keep taking action, to keep fighting back.",
            "No one could run a four-minute mile.",
            "Until someone could.",
            "And then, everyone could.",
            "We look to those people who shine.",
            "We take our strength from them.",
            "They offer us an alternative to the climate of fear and",
            "terror around us.",
            "They give us a different path.",
            "[Others] will tell you its pointless.",
            "[Others] will tell you its hopeless.",
            "[Others] will tell you to give up, they will mock you, spit on you, and",
            "everything else to demean and degrade you.",
            "Because they are terrified of that person who does not despair.",
            "Do not allow them to do so, and you've already taken the greatest",
            "step to victory that you can take."
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

        AdjustCamera();
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

        if (Screen.width >= Screen.height)
        {
            // TODO - not quite right; need a different formula for width
            var width = sceneAni.sprite.bounds.size.x;

            var worldScreenWidth = mainCamera.orthographicSize * 4.20;

            sceneAni.transform.localScale = new Vector3(
                (float)worldScreenWidth / width,
                (float)worldScreenWidth / width,
                1);
        }
        else
        {
            var height = sceneAni.sprite.bounds.size.y;

            var worldScreenHeight = mainCamera.orthographicSize * 2.0;

            sceneAni.transform.localScale = new Vector3(
                (float)worldScreenHeight / height,
                (float)worldScreenHeight / height,
                1);
        }
    }

    public void CompleteCutscene()
    {
        // Reward 
        PlayerPrefs.SetString("TransferActions", "Quest12Reward");

        WarpOut();
    }

    public void WarpOut()
    {
        warpTyrannyTunnel.GetComponent<BoxCollider2D>().enabled = true;
        warpTyrannyTunnel.GetComponent<SceneTransitioner>().bAnimationToTransitionScene = true;

        // Save Transfer Values
        save.SaveBrioTransfer();
        save.SaveInventoryTransfer();
        PlayerPrefs.SetInt("Transferring", 1);
        PlayerPrefs.SetString("TransferScene", warpTyrannyTunnel.GetComponent<SceneTransitioner>().BetaLoad);

        // Stop the player from bringing up the dialog again
        dMan.gameObject.transform.localScale = Vector3.zero;

        // Set player scale to start transition
        player.transform.localScale = Vector3.one;

        // Freeze animation
        sceneAni.GetComponent<Animator>().enabled = false;

        // Set the transition animation via the camera's scale
        //var height = mainCamera.GetComponentInChildren<SpriteRenderer>().sprite.bounds.size.y;
        //var worldScreenHeight = mainCamera.orthographicSize * 2.0;

        //mainCamera.GetComponentInChildren<Transform>().localScale = new Vector3(
        //    (float)worldScreenHeight / height,
        //    (float)worldScreenHeight / height,
        //    1);

        // Activate the other fader
        sFader.GetComponent<Animator>().SetBool("FadeOut", true);
    }
}
