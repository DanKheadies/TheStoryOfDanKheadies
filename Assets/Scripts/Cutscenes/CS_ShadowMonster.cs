// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/19/2021
// Last:  09/23/2021

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CS_ShadowMonster : MonoBehaviour
{
    public Camera mainCamera;
    public DialogueManager dMan;
    public GameObject dBox;
    public GameObject player;
    public GameObject warpShadowMonster;
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

    public float musicTimer1;
    public float strobeTimer;
    public float timer;

    public string[] dialogueLines;


    void Start()
    {
        musicTimer1 = 21.35f;
        strobeTimer = 1.0f;
        timer = 0.333f;

        // Restrict player movement
        player.GetComponent<PlayerMovement>().bStopPlayerMovement = true;

        dialogueLines = new string[] {
            "I read this New Testament line decades ago..",
            "I could never understand it.",
            //"The line is 'The meek shall inherit the Earth,' and I thought..",
            //"There's something wrong with that line. It doesn't make sense",
            //"to me. Meek doesn't seem to me to be a moral virtue.",
            //"Meek is not a good translation. What it means is this...",
            //"Those who have swords and know how to use them,",
            //"but keep them sheathed, will inherit the world.",
            //"That's a BIG difference, it's so great. Ya know, cuz everyone says..",
            //"You should be harmless, virtuous, you shouldn't do anyone any harm.",
            //"You should sheath your competitive instinct. You shouldn't try to win.",
            //"You don't want to be too aggressive, to assertive.",
            //"You want to take a backset. And all of that...",
            //"It's like, NO.. Wrong....",
            //"You should be a monster.",
            //"An absolute monster.",
            //"And you should learn how to control it.",
            //"It's like, is there something wrong with being competitive?",
            //"There's nothing wrong with it. There's something wrong with cheating.",
            //"There's something wrong with winning unfairly..",
            //"All of those things are bad. But you don't want people to win?",
            //"What's the difference between trying to win and striving?",
            //"You want to eradicate striving?",
            //"A definition of a winner is someone who never let losing stop them.",
            //"Life isn't a game. It's a series of games.",
            //"And the right ethic is to be the winner of the series of games.",
            //"And part of that means you have to learn to be a good loser.",
            //"Because you're not going to win every single game.",
            //"You can pick your level of competition in life, to some degree.",
            //"So lets say you pick a level of competition where you're always",
            //"winning. Well all that means is that you picked the wrong level",
            //"of competition. Lets say you're a grand master chess player,",
            //"and all you do is play amateurs. And every night you go home and",
            //"congratulate yourself on what a genius you are.",
            //"It's like, you're not a genius. You're dim-witted.",
            //"What you should be doing is playing people who are beating you",
            //"as much as you can tolerate. You want to be on the edge,",
            //"where your skills are being developed.",
            //"Where loss is always a possibility.",
            //"You should be a monster.",
            //"You should be a monster.."
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

        // Change from first music track to second
        if (musicTimer1 > 0)
        {
            musicTimer1 -= Time.deltaTime;

            if (musicTimer1 <= 0)
                mMan.SwitchTrack(1);
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
        PlayerPrefs.SetString("TransferActions", "Quest13Reward");

        WarpOut();
    }

    public void WarpOut()
    {
        warpShadowMonster.GetComponent<BoxCollider2D>().enabled = true;
        // Using fader below; inactivating Sprite Renderer on this component
        //warpShadowMonster.GetComponent<SceneTransitioner>().bAnimationToTransitionScene = true;

        // Save Transfer Values
        save.SaveBrioTransfer();
        save.SaveInventoryTransfer();
        PlayerPrefs.SetInt("Transferring", 1);
        PlayerPrefs.SetString("TransferScene", warpShadowMonster.GetComponent<SceneTransitioner>().BetaLoad);

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
