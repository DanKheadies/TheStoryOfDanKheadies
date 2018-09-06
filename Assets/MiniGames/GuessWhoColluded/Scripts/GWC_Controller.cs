// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/31/2018
// Last:  08/26/2018

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Contains 'key' GWC code
public class GWC_Controller : MonoBehaviour
{
    public AspectUtility aUtil;
    public Camera mainCamera;
    public CameraFollow camFollow;
    public DialogueManager dMan;
    public GameObject contArrow;
    public GameObject dArrow;
    public GameObject dBox;
    public GameObject guiConts;
    public GameObject HUD;
    public GameObject muellerCards;
    public GameObject oBox;
    public GameObject pause;
    public GameObject pauseBtn;
    public GameObject playerCard;
    public GameObject sceneTransAnim;
    public GameObject sFaderAnim;
    public GameObject sFaderAnimDia;
    public GameObject thePlayer;
    public GameObject trumpCards;
    public GameObject warpGWC;
    public Image dPic;
    public MoveOptionsMenuArrow moveOptsArw;
    public MusicManager mMan;
    public OptionsManager oMan;
    public PlayerBrioManager brio;
    public SaveGame save;
    public Scene scene;
    public SFXManager SFXMan;
    public Sprite[] portPic;
    public Text dText;
    public TouchControls touches;
    public UIManager uiMan;

    private bool bAvoidUpdate;
    private bool bOptTeamSelect;
    private bool bOptOppSelect;
    private bool bStartGame;

    public float strobeTimer;

    public int randomCharacter;

    public string[] dialogueLines;
    public string[] optionsLines;

    void Start()
    {
        // Initializers
        aUtil = FindObjectOfType<AspectUtility>();
        brio = FindObjectOfType<PlayerBrioManager>();
        camFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        contArrow = GameObject.Find("Dialogue_Arrow");
        dArrow = GameObject.Find("Dialogue_Arrow");
        dBox = GameObject.Find("Dialogue_Box");
        dMan = FindObjectOfType<DialogueManager>();
        dPic = GameObject.Find("Dialogue_Picture").GetComponent<Image>();
        dText = GameObject.Find("Dialogue_Text").GetComponent<Text>();
        guiConts = GameObject.Find("GUIControls");
        HUD = GameObject.Find("HUD");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mMan = FindObjectOfType<MusicManager>();
        moveOptsArw = FindObjectOfType<MoveOptionsMenuArrow>();
        muellerCards = GameObject.Find("Mueller_Cards");
        oBox = GameObject.Find("Options_Box");
        oMan = FindObjectOfType<OptionsManager>();
        pause = GameObject.FindGameObjectWithTag("Pause");
        pauseBtn = GameObject.Find("PauseButton");
        playerCard = GameObject.Find("Player_Character_Card");
        save = FindObjectOfType<SaveGame>();
        scene = SceneManager.GetActiveScene();
        sceneTransAnim = GameObject.Find("SceneTransitioner");
        sFaderAnim = GameObject.Find("Screen_Fader");
        sFaderAnimDia = GameObject.Find("Screen_Fader_Dialogue");
        SFXMan = FindObjectOfType<SFXManager>();
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        touches = FindObjectOfType<TouchControls>();
        trumpCards = GameObject.Find("Trump_Cards");
        warpGWC = GameObject.Find("GuessWhoColluded.to.Chp1");
        uiMan = FindObjectOfType<UIManager>();

        strobeTimer = 1.0f;

        // Trump Dialogue
        //dialogueLines = new string[] {
        //        "There was no collusion. Everybody knows there was no collusion.",
        //        "The Fake News, the Disgusting Democrats, and the Deep State would say different."
        //    };

        // Initial prompt to pick a side
        dMan.bDialogueActive = false;
        guiConts.transform.localScale = Vector3.zero;
        pauseBtn.transform.localScale = Vector3.zero;
        mMan.bMusicCanPlay = false;
        thePlayer.GetComponent<PlayerMovement>().bStopPlayerMovement = true;

        dialogueLines = new string[] {
                "I want YOU.. to         Guess Who Colluded."
            };

        dMan.dialogueLines = dialogueLines;
        dMan.currentLine = 0;
        dText.text = dialogueLines[dMan.currentLine];
        dPic.sprite = portPic[48];
        dBox.transform.localScale = Vector3.one;
        sFaderAnimDia.GetComponent<Animator>().enabled = true;
    }

    void Update()
    {
        // New Game -- Dialogue activation & strobe arrow start
        if (strobeTimer > 0)
        {
            strobeTimer -= Time.deltaTime;

            if (strobeTimer <= 0)
            {
                bOptTeamSelect = true;
                contArrow.GetComponent<ImageStrobe>().bStartStrobe = true;
                dMan.bDialogueActive = true;
                sFaderAnimDia.transform.localScale = Vector3.zero; // Remove to allow mouse click on options prompts

                // Sound Effect
                SFXMan.sounds[2].PlayOneShot(SFXMan.sounds[2].clip);
            }
        }

        if (bOptTeamSelect &&
            !dMan.bDialogueActive)
        {
            thePlayer.GetComponent<PlayerMovement>().bStopPlayerMovement = true;

            touches.transform.localScale = Vector3.zero;

            dialogueLines = new string[] {
                "First and first mostly, whose side are you on?"
            };
            // DC -- dman.ResetDialogue()?
            dMan.dialogueLines = dialogueLines;
            dMan.currentLine = 0;
            dText.text = dialogueLines[dMan.currentLine];
            dBox.transform.localScale = Vector3.one;
            dMan.bDialogueActive = true;


            optionsLines = new string[] {
                "Team Trump",
                "Team Mueller"
            };

            for (int i = 0; i < optionsLines.Length; i++)
            {
                GameObject optText = GameObject.Find("Opt" + (i + 1) + "_Text");
                optText.GetComponentInChildren<Text>().text = optionsLines[i];
                oMan.tempOptsCount += 1;
            }

            oMan.bDiaToOpts = true;
            oMan.bOptionsActive = true;
            oMan.HideThirdPlusOpt();
            oBox.transform.localScale = Vector3.one;
            oMan.PauseOptions();
        }

        // Begin play -- Activate music, UI, and fade after team selection
        if (!dMan.bDialogueActive &&
            !bAvoidUpdate &&
            bStartGame)
        {
            thePlayer.GetComponent<PlayerMovement>().bStopPlayerMovement = false;
            guiConts.transform.localScale = Vector3.one;
            pauseBtn.transform.localScale = Vector3.one;
            mMan.bMusicCanPlay = true;
            sFaderAnim.GetComponent<Animator>().enabled = true;

            // Change to avoid running this logic
            bAvoidUpdate = true;
        }

        // Zoom In -- Scroll Forward or press Y
        if ((Input.GetAxis("Mouse ScrollWheel") > 0 &&
             mainCamera.orthographicSize >= aUtil._wantedAspectRatio) ||
            (Input.GetKeyDown(KeyCode.Comma) &&
             mainCamera.orthographicSize >= aUtil._wantedAspectRatio) ||
            (touches.bYaction &&
             mainCamera.orthographicSize >= aUtil._wantedAspectRatio))
        {
            mainCamera.orthographicSize = mainCamera.orthographicSize - 0.25f;
            touches.bYaction = false;

            sceneTransAnim.transform.localScale = new Vector2
                (sceneTransAnim.transform.localScale.x - 0.35f,
                sceneTransAnim.transform.localScale.y - 0.35f);
        }

        // Zoom Out -- Scroll Back or press X
        if ((Input.GetAxis("Mouse ScrollWheel") < 0 &&
             mainCamera.orthographicSize < 5.5f) ||
            (Input.GetKeyDown(KeyCode.Period) &&
             mainCamera.orthographicSize < 5.5f) ||
            (touches.bXaction &&
             mainCamera.orthographicSize < 5.5f))
        {
            mainCamera.orthographicSize = mainCamera.orthographicSize + 0.25f;
            touches.bXaction = false;

            sceneTransAnim.transform.localScale = new Vector2
                (sceneTransAnim.transform.localScale.x + 0.35f,
                sceneTransAnim.transform.localScale.y + 0.35f);
        }
    }

    public void OptionsSelection()
    {
        // Dialogue 1 - Option 1 - Selected Trump
        if (bOptTeamSelect &&
            moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1)
        {
            bOptTeamSelect = false;
            bOptOppSelect = true;

            thePlayer.GetComponent<PlayerMovement>().bStopPlayerMovement = true;

            touches.transform.localScale = Vector3.zero;

            dialogueLines = new string[] {
                //"But what to do, what to do... Should I go after"
                "But what to do, what to do... Are you going to.."
            };
            // DC -- dman.ResetDialogue()?
            dMan.dialogueLines = dialogueLines;
            dMan.currentLine = 0;
            dText.text = dialogueLines[dMan.currentLine];
            dBox.transform.localScale = Vector3.one;
            dMan.bDialogueActive = true;


            //optionsLines = new string[] {
            //    "Trump (and very Fine People)",
            //    "Mueller (and the Fake News)"
            //};
            optionsLines = new string[] {
                "Stop the leaks!",
                "End the witch hunt!"
            };

            for (int i = 0; i < optionsLines.Length; i++)
            {
                GameObject optText = GameObject.Find("Opt" + (i + 1) + "_Text");
                optText.GetComponentInChildren<Text>().text = optionsLines[i];
                oMan.tempOptsCount += 1;
            }

            oMan.bDiaToOpts = true;
            oMan.bOptionsActive = true;
            oMan.HideThirdPlusOpt();
            oBox.transform.localScale = Vector3.one;
            oMan.PauseOptions();
        }

        // Dialogue 1 - Option 2 - Selected Mueller
        else if (bOptTeamSelect &&
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2)
        {
            bOptTeamSelect = false;

            oMan.ResetOptions();

            // Display Trump board
            trumpCards.transform.localScale = Vector3.one;

            dMan.dialogueLines = new string[] {
                "Time to find out who on Team Trump is colluding...",
                "And I better do it quickly."
            };
            dMan.currentLine = 0;
            dText.text = dMan.dialogueLines[dMan.currentLine];
            dMan.ShowDialogue();
            dArrow.GetComponent<ImageStrobe>().bStartStrobe = true; // DC TODO -- Not strobing?

            bStartGame = true;

            // Pick random Mueller character for the player
            randomCharacter = Random.Range(0, 23);
            dPic.sprite = portPic[randomCharacter];
            playerCard.gameObject.transform.GetChild(randomCharacter).localScale = Vector3.one;
        }

        // Dialogue 2 - Option 1 - Selected Trump (and very Fine People)
        else if (bOptOppSelect &&
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1)
        {
            bOptOppSelect = false;
            bOptTeamSelect = false;

            oMan.ResetOptions();

            // Display Trump board
            trumpCards.transform.localScale = Vector3.one;

            dMan.dialogueLines = new string[] {
                "Time to find out who on Team Trump is leaking information...",
                "And I better do it quickly."
            };
            dMan.currentLine = 0;
            dText.text = dMan.dialogueLines[dMan.currentLine];
            dMan.ShowDialogue();
            dArrow.GetComponent<ImageStrobe>().bStartStrobe = true; // DC TODO -- Not strobing?

            bStartGame = true;

            // Pick random Trump character for the player
            randomCharacter = Random.Range(24, 47);
            dPic.sprite = portPic[randomCharacter];
            playerCard.gameObject.transform.GetChild(randomCharacter).localScale = Vector3.one;
        }

        // Dialogue 2 - Option 2 - Selected Mueller (and the Fake News)
        else if (bOptOppSelect &&
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2)
        {
            bOptOppSelect = false;
            bOptTeamSelect = false;

            oMan.ResetOptions();

            // Display Mueller board
            muellerCards.transform.localScale = Vector3.one;

            dMan.dialogueLines = new string[] {
                "Time to find out who on Team Mueller is leading this witch hunt...",
                "And I better do it quickly."
            };
            dMan.currentLine = 0;
            dText.text = dMan.dialogueLines[dMan.currentLine];
            dMan.ShowDialogue();
            dArrow.GetComponent<ImageStrobe>().bStartStrobe = true; // DC TODO -- Not strobing?

            bStartGame = true;

            // Pick random Trump character for the player
            randomCharacter = Random.Range(24, 47);
            dPic.sprite = portPic[randomCharacter];
            playerCard.gameObject.transform.GetChild(randomCharacter).localScale = Vector3.one;
        }

        // Lose brio every X seconds while playing
        if (brio.playerCurrentBrio > 1 &&
            pause.transform.localScale != Vector3.one &&
            !dMan.bDialogueActive)
        {
            if (warpGWC.GetComponent<SceneTransitioner>().bAnimationToTransitionScene)
            {
                // Avoid losing brio if scene transition animation is going
            }
            else
            {
                brio.FatiguePlayer(0.0025f);
                uiMan.bUpdateBrio = true;
            }
        }
    }

    public void GG()
    {
        warpGWC.GetComponent<BoxCollider2D>().enabled = true;
        warpGWC.GetComponent<SceneTransitioner>().bAnimationToTransitionScene = true;

        // Save Transfer Values
        save.SaveBrioTransfer();
        save.SaveInventoryTransfer();
        PlayerPrefs.SetInt("Transferring", 1);
        PlayerPrefs.SetString("TransferScene", warpGWC.GetComponent<SceneTransitioner>().BetaLoad);

        // Stop Dan from moving
        thePlayer.GetComponent<PlayerMovement>().bStopPlayerMovement = true;

        // Stop the player from bringing up the dialog again
        dMan.gameObject.SetActive(false);
    }
    public void OpenKREAMinac()
    {
        #if !UNITY_WEBGL
            Application.OpenURL("https://docs.google.com/document/d/1Q8-YiK7TAVkGBsrL_3F9a92JjTFYVCyLcg-RQNNKYkM/edit?usp=sharing");
        #endif
    }

    public void OpenIcons()
    {
        #if !UNITY_WEBGL
            Application.OpenURL("https://docs.google.com/document/d/1Q8-YiK7TAVkGBsrL_3F9a92JjTFYVCyLcg-RQNNKYkM/edit?usp=sharing#bookmark=id.2ukwna434o1k");
        #endif
    }
}