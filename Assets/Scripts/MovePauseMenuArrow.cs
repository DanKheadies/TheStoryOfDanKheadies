// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 11/08/2017
// Last:  08/26/2018

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// To "move" and execute the arrows on the Pause Menu
public class MovePauseMenuArrow : MonoBehaviour
{
    private Button ControlsBtn;
    private Button GGBtn;
    private Button GoOnBtn;
    private Button IconsBtn;
    private Button KogabiBtn;
    private Button KREAMBtn;
    private Button MapBtn;
    private Button MyCardBtn;
    private Button QuitBtn;
    private Button SaveBtn;
    private Button SoundBtn;
    private Button StuffBtn;

    private GameObject ControlsArw;
    private GameObject GGArw;
    private GameObject GoOnArw;
    private GameObject IconsArw;
    private GameObject KogabiArw;
    private GameObject KREAMArw;
    private GameObject MapArw;
    private GameObject MyCardArw;
    private GameObject QuitArw;
    private GameObject SaveArw;
    private GameObject SoundArw;
    private GameObject StuffArw;

    private Scene scene;
    private TouchControls touches;
    private Transform pauseMenu;


    public enum ArrowPos : int
    {
        // For Core
        GoOn = 167,
        Save = 120,
        Kogabi = 72,
        Stuff = 25,
        Map = -23,
        Sound = -70,
        Controls = -118,
        Quit = -165,

        // For Guess Who Colluded
        KREAMinac = 1,
        MyCard = 2,
        Icons = 3,
        GG = 4
    }

    public ArrowPos currentPosition;

    public bool bSecondaryMenu;

    void Start()
    {
        // Initializers
        pauseMenu = GameObject.Find("PauseScreen").transform;
        scene = SceneManager.GetActiveScene();
        touches = FindObjectOfType<TouchControls>();

        ControlsBtn = GameObject.Find("Controls").GetComponent<Button>();
        GoOnBtn = GameObject.Find("GoOn").GetComponent<Button>();
        KogabiBtn = GameObject.Find("Kogabi").GetComponent<Button>();
        MapBtn = GameObject.Find("Map").GetComponent<Button>();
        QuitBtn = GameObject.Find("Quit").GetComponent<Button>();
        SaveBtn = GameObject.Find("Save").GetComponent<Button>();
        SoundBtn = GameObject.Find("Sound").GetComponent<Button>();
        StuffBtn = GameObject.Find("Stuff").GetComponent<Button>();

        ControlsArw = GameObject.Find("ControlsArw");
        GoOnArw = GameObject.Find("GoOnArw");
        KogabiArw = GameObject.Find("KogabiArw");
        MapArw = GameObject.Find("MapArw");
        QuitArw = GameObject.Find("QuitArw");
        SaveArw = GameObject.Find("SaveArw");
        SoundArw = GameObject.Find("SoundArw");
        StuffArw = GameObject.Find("StuffArw");

        if (scene.name == "GuessWhoColluded")
        {
            GGBtn = GameObject.Find("GG").GetComponent<Button>();
            IconsBtn = GameObject.Find("Icons").GetComponent<Button>();
            KREAMBtn = GameObject.Find("KREAMinac").GetComponent<Button>();
            MyCardBtn = GameObject.Find("MyCard").GetComponent<Button>();

            GGArw = GameObject.Find("GGArw");
            IconsArw = GameObject.Find("IconsArw");
            KREAMArw = GameObject.Find("KREAMinacArw");
            MyCardArw = GameObject.Find("MyCardArw");
        }

        currentPosition = ArrowPos.GoOn;
    }
	
	void Update ()
    {
        //Debug.Log((float)currentPosition);
        //var newfloat = (float)currentPosition;
        //Debug.Log(newfloat);
        //PauseArrow.transform.position = new Vector2(
        //    PauseArrow.transform.position.x,
        //    (int)currentPosition
        //    );
        //Debug.Log(newfloat);
        //Debug.Log((int)currentPosition);
        
        if (pauseMenu.localScale == Vector3.one)
        {
            if (Input.GetKeyDown(KeyCode.S) ||
                Input.GetKeyDown(KeyCode.DownArrow)) //||
                //touches.bDown) DC TODO -- Fix touches w/ arrow movement (also, see OptionsMenu)
            {
                // For core
                if (currentPosition == ArrowPos.GoOn)
                {
                    currentPosition = ArrowPos.Save;
                    ClearAllArrows();
                    SaveArw.transform.localScale = Vector3.one;
                }
                else if (currentPosition == ArrowPos.Save)
                {
                    currentPosition = ArrowPos.Stuff;
                    ClearAllArrows();
                    StuffArw.transform.localScale = Vector3.one;
                }
                else if (currentPosition == ArrowPos.Stuff)
                {
                    currentPosition = ArrowPos.Sound;
                    ClearAllArrows();
                    SoundArw.transform.localScale = Vector3.one;
                }
                else if (currentPosition == ArrowPos.Sound)
                {
                    currentPosition = ArrowPos.Controls;
                    ClearAllArrows();
                    ControlsArw.transform.localScale = Vector3.one;
                }
                else if (currentPosition == ArrowPos.Controls)
                {
                    currentPosition = ArrowPos.Quit;
                    ClearAllArrows();
                    QuitArw.transform.localScale = Vector3.one;
                }

                // For Guess Who Colluded
                if (scene.name == "GuessWhoColluded")
                {
                    // Skips & goes back "down" to correct option
                    if (currentPosition == ArrowPos.Save)
                    {
                        currentPosition = ArrowPos.Stuff;
                        ClearAllArrows();
                        StuffArw.transform.localScale = Vector3.one;
                    }
                    // Tiggers arrow to move to secondary menu
                    else if (currentPosition == ArrowPos.Quit &&
                             !bSecondaryMenu)
                    {
                        bSecondaryMenu = true;
                    }
                    // GWC only options
                    else if (currentPosition == ArrowPos.Quit &&
                             bSecondaryMenu)
                    {
                        currentPosition = ArrowPos.KREAMinac;
                        ClearAllArrows();
                        KREAMArw.transform.localScale = new Vector3(1, 1, 1);
                    }
                    else if (currentPosition == ArrowPos.KREAMinac)
                    {
                        currentPosition = ArrowPos.Icons;
                        ClearAllArrows();
                        IconsArw.transform.localScale = new Vector3(1, 1, 1);
                    }
                    else if (currentPosition == ArrowPos.Icons)
                    {
                        currentPosition = ArrowPos.GG;
                        ClearAllArrows();
                        GGArw.transform.localScale = new Vector3(1, 1, 1);
                    }
                }

                // For Minesweeper
                else if (scene.name == "Minesweeper")
                {
                    // Skips & goes back "down" to correct option
                    if (currentPosition == ArrowPos.Save)
                    {
                        currentPosition = ArrowPos.Stuff;
                        ClearAllArrows();
                        StuffArw.transform.localScale = Vector3.one;
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.W) ||
                     Input.GetKeyDown(KeyCode.UpArrow)) //||
                      //touches.bUp) DC TODO -- Fix touches w/ arrow movement (also, see OptionsMenu)
            {
                // For core
                if (currentPosition == ArrowPos.Quit)
                {
                    currentPosition = ArrowPos.Controls;
                    ClearAllArrows();
                    ControlsArw.transform.localScale = Vector3.one;
                }
                else if (currentPosition == ArrowPos.Controls)
                {
                    currentPosition = ArrowPos.Sound;
                    ClearAllArrows();
                    SoundArw.transform.localScale = Vector3.one;
                }
                else if (currentPosition == ArrowPos.Sound)
                {
                    currentPosition = ArrowPos.Stuff;
                    ClearAllArrows();
                    StuffArw.transform.localScale = Vector3.one;
                }
                else if (currentPosition == ArrowPos.Stuff)
                {
                    currentPosition = ArrowPos.Save;
                    ClearAllArrows();
                    SaveArw.transform.localScale = Vector3.one;
                }
                else if (currentPosition == ArrowPos.Save)
                {
                    currentPosition = ArrowPos.GoOn;
                    ClearAllArrows();
                    GoOnArw.transform.localScale = Vector3.one;
                }

                // For Guess Who Colluded
                if (scene.name == "GuessWhoColluded")
                {
                    // GWC only options
                    if (currentPosition == ArrowPos.GG)
                    {
                        currentPosition = ArrowPos.Icons;
                        ClearAllArrows();
                        IconsArw.transform.localScale = Vector3.one;
                    }
                    else if (currentPosition == ArrowPos.Icons)
                    {
                        currentPosition = ArrowPos.KREAMinac;
                        ClearAllArrows();
                        KREAMArw.transform.localScale = Vector3.one;
                    }
                    else if (currentPosition == ArrowPos.KREAMinac)
                    {
                        currentPosition = ArrowPos.Quit;
                        ClearAllArrows();
                        QuitArw.transform.localScale = new Vector3(1, 1, 1);

                        // Resets secondary menu positioning
                        bSecondaryMenu = false;
                    }
                    // Skips & goes back "up" to correct option
                    else if (currentPosition == ArrowPos.Save)
                    {
                        currentPosition = ArrowPos.GoOn;
                        ClearAllArrows();
                        GoOnArw.transform.localScale = new Vector3(1, 1, 1);
                    }
                }

                // For Minesweeper
                if (scene.name == "Minesweeper")
                {
                    // Skips & goes back "down" to correct option
                    if (currentPosition == ArrowPos.Save)
                    {
                        currentPosition = ArrowPos.GoOn;
                        ClearAllArrows();
                        GoOnArw.transform.localScale = Vector3.one;
                    }
                }
            }
            else if (Input.GetButtonDown("Action") ||
                     touches.bAaction)
            {
                // For core
                if (currentPosition == ArrowPos.GoOn)
                {
                    GoOnBtn.onClick.Invoke();
                }
                else if (currentPosition == ArrowPos.Save)
                {
                    SaveBtn.onClick.Invoke();
                }
                else if (currentPosition == ArrowPos.Kogabi)
                {
                    KogabiBtn.onClick.Invoke();
                }
                else if (currentPosition == ArrowPos.Stuff)
                {
                    StuffBtn.onClick.Invoke();
                }
                else if (currentPosition == ArrowPos.Map)
                {
                    MapBtn.onClick.Invoke();
                }
                else if (currentPosition == ArrowPos.Sound)
                {
                    SoundBtn.onClick.Invoke();
                }
                else if (currentPosition == ArrowPos.Controls)
                {
                    ControlsBtn.onClick.Invoke();
                }
                else if (currentPosition == ArrowPos.Quit)
                {
                    QuitBtn.onClick.Invoke();
                }

                // For Guess Who Colluded
                if (scene.name == "GuessWhoColluded")
                {
                    if (currentPosition == ArrowPos.KREAMinac)
                    {
                        KREAMBtn.onClick.Invoke();
                    }
                    else if (currentPosition == ArrowPos.MyCard)
                    {
                        MyCardBtn.onClick.Invoke();
                    }
                    else if (currentPosition == ArrowPos.Icons)
                    {
                        IconsBtn.onClick.Invoke();
                    }
                    else if (currentPosition == ArrowPos.GG)
                    {
                        GGBtn.onClick.Invoke();
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Escape) ||
                     Input.GetButton("BAction") ||
                     touches.bBaction)
            {
                SaveBtn.GetComponentInChildren<Text>().text = "Save";
                ResetArrows();
            }
        }
    }

    public void ClearAllArrows()
    {
        if (pauseMenu.localScale == Vector3.one)
        {
            ControlsArw.transform.localScale = Vector3.zero;
            GoOnArw.transform.localScale = Vector3.zero;
            KogabiArw.transform.localScale = Vector3.zero;
            MapArw.transform.localScale = Vector3.zero;
            QuitArw.transform.localScale = Vector3.zero;
            SaveArw.transform.localScale = Vector3.zero;
            SoundArw.transform.localScale = Vector3.zero;
            StuffArw.transform.localScale = Vector3.zero; 

            if (scene.name == "GuessWhoColluded")
            {
                GGArw.transform.localScale = Vector3.zero;
                IconsArw.transform.localScale = Vector3.zero;
                KREAMArw.transform.localScale = Vector3.zero;
                MyCardArw.transform.localScale = Vector3.zero;
            }
        }
    }

    public void ResetArrows()
    {
        ControlsArw.transform.localScale = Vector3.zero;
        GoOnArw.transform.localScale = Vector3.zero;
        KogabiArw.transform.localScale = Vector3.zero;
        MapArw.transform.localScale = Vector3.zero;
        QuitArw.transform.localScale = Vector3.zero;
        SaveArw.transform.localScale = Vector3.zero;
        SoundArw.transform.localScale = Vector3.zero;
        StuffArw.transform.localScale = Vector3.zero;

        if (scene.name == "GuessWhoColluded")
        {
            GGArw.transform.localScale = Vector3.zero;
            IconsArw.transform.localScale = Vector3.zero;
            KREAMArw.transform.localScale = Vector3.zero;
            MyCardArw.transform.localScale = Vector3.zero;
        }

        GoOnArw.transform.localScale = Vector3.one;
        currentPosition = ArrowPos.GoOn;
        
        // Resets secondary menu positioning
        bSecondaryMenu = false;
    }
}
