// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 11/08/2017
// Last:  08/12/2018

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// To "move" and execute the arrows on the Pause Menu
public class MovePauseMenuArrow : MonoBehaviour
{
    private Button GoOnBtn;
    private Button SaveBtn;
    private Button KogabiBtn;
    private Button StuffBtn;
    private Button MapBtn;
    private Button SoundBtn;
    private Button ControlsBtn;
    private Button QuitBtn;

    private GameObject GoOnArw;
    private GameObject SaveArw;
    private GameObject KogabiArw;
    private GameObject StuffArw;
    private GameObject MapArw;
    private GameObject SoundArw;
    private GameObject ControlsArw;
    private GameObject QuitArw;

    private Scene scene;
    private TouchControls touches;
    private Transform pauseMenu;


    public enum ArrowPos : int
    {
        GoOn = 167,
        Save = 120,
        Kogabi = 72,
        Stuff = 25,
        Map = -23,
        Sound = -70,
        Controls = -118,
        Quit = -165
    }

    public ArrowPos currentPosition;

    void Start()
    {
        // Initializers
        GoOnBtn = GameObject.Find("GoOn").GetComponent<UnityEngine.UI.Button>();
        SaveBtn = GameObject.Find("Save").GetComponent<UnityEngine.UI.Button>();
        KogabiBtn = GameObject.Find("Kogabi").GetComponent<UnityEngine.UI.Button>();
        StuffBtn = GameObject.Find("Stuff").GetComponent<UnityEngine.UI.Button>();
        MapBtn = GameObject.Find("Map").GetComponent<UnityEngine.UI.Button>();
        SoundBtn = GameObject.Find("Sound").GetComponent<UnityEngine.UI.Button>();
        ControlsBtn = GameObject.Find("Controls").GetComponent<UnityEngine.UI.Button>();
        QuitBtn = GameObject.Find("Quit").GetComponent<UnityEngine.UI.Button>();

        GoOnArw = GameObject.Find("GoOnArw");
        SaveArw = GameObject.Find("SaveArw");
        KogabiArw = GameObject.Find("KogabiArw");
        StuffArw = GameObject.Find("StuffArw");
        MapArw = GameObject.Find("MapArw");
        SoundArw = GameObject.Find("SoundArw");
        ControlsArw = GameObject.Find("ControlsArw");
        QuitArw = GameObject.Find("QuitArw");

        pauseMenu = GameObject.Find("PauseScreen").transform;
        scene = SceneManager.GetActiveScene();
        touches = FindObjectOfType<TouchControls>();

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
                if (currentPosition == ArrowPos.GoOn)
                {
                    if (scene.name == "Minesweeper")
                    {
                        currentPosition = ArrowPos.Stuff;
                        ClearAllArrows();
                        StuffArw.transform.localScale = Vector3.one;
                    }
                    else
                    {
                        currentPosition = ArrowPos.Save;
                        ClearAllArrows();
                        SaveArw.transform.localScale = Vector3.one;
                    }
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
            }
            else if (Input.GetKeyDown(KeyCode.W) ||
                     Input.GetKeyDown(KeyCode.UpArrow)) //||
                      //touches.bUp) DC TODO -- Fix touches w/ arrow movement (also, see OptionsMenu)
            {
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
                    if (scene.name == "Minesweeper")
                    {
                        currentPosition = ArrowPos.GoOn;
                        ClearAllArrows();
                        GoOnArw.transform.localScale = Vector3.one;
                    }
                    else
                    {
                        currentPosition = ArrowPos.Save;
                        ClearAllArrows();
                        SaveArw.transform.localScale = Vector3.one;
                    }

                }
                else if (currentPosition == ArrowPos.Save)
                {
                    currentPosition = ArrowPos.GoOn;
                    ClearAllArrows();
                    GoOnArw.transform.localScale = Vector3.one;
                }
            }
            else if (Input.GetButtonDown("Action") ||
                     touches.bAction)
            {
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
            GoOnArw.transform.localScale = Vector3.zero;
            SaveArw.transform.localScale = Vector3.zero;
            KogabiArw.transform.localScale = Vector3.zero;
            StuffArw.transform.localScale = Vector3.zero;
            MapArw.transform.localScale = Vector3.zero;
            SoundArw.transform.localScale = Vector3.zero;
            ControlsArw.transform.localScale = Vector3.zero;
            QuitArw.transform.localScale = Vector3.zero;
        }
    }

    public void ResetArrows()
    {
        GoOnArw.transform.localScale = Vector3.one;
        SaveArw.transform.localScale = Vector3.zero;
        KogabiArw.transform.localScale = Vector3.zero;
        StuffArw.transform.localScale = Vector3.zero;
        MapArw.transform.localScale = Vector3.zero;
        SoundArw.transform.localScale = Vector3.zero;
        ControlsArw.transform.localScale = Vector3.zero;
        QuitArw.transform.localScale = Vector3.zero;

        currentPosition = ArrowPos.GoOn;
    }
}
