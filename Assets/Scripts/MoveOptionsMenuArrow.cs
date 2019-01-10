// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/23/2018
// Last:  01/10/2019

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// To "move" and execute the arrows on the Options Menu
public class MoveOptionsMenuArrow : MonoBehaviour
{
    private Button Opt1Btn;
    private Button Opt2Btn;
    private Button Opt3Btn;
    private Button Opt4Btn;
    private GameObject Opt1Arw;
    private GameObject Opt2Arw;
    private GameObject Opt3Arw;
    private GameObject Opt4Arw;
    private GameObject pauseScreen;
    private OptionsManager oMan;
    private Scene scene;
    private TouchControls touches;
    private Transform optionsBox;

    public bool bControllerDown;
    public bool bControllerUp;
    public bool bFreezeControllerInput;


    public enum ArrowPos : int
    {
        Opt1 = 1,
        Opt2 = 2,
        Opt3 = 3,
        Opt4 = 4
    }

    public ArrowPos currentPosition;

    void Start()
    {
        // Initializers
        Opt1Btn = GameObject.Find("Opt1").GetComponent<UnityEngine.UI.Button>();
        Opt2Btn = GameObject.Find("Opt2").GetComponent<UnityEngine.UI.Button>();
        Opt3Btn = GameObject.Find("Opt3").GetComponent<UnityEngine.UI.Button>();
        Opt4Btn = GameObject.Find("Opt4").GetComponent<UnityEngine.UI.Button>();

        Opt1Arw = GameObject.Find("Opt1Arw");
        Opt2Arw = GameObject.Find("Opt2Arw");
        Opt3Arw = GameObject.Find("Opt3Arw");
        Opt4Arw = GameObject.Find("Opt4Arw");

        oMan = GameObject.FindObjectOfType<OptionsManager>();
        optionsBox = GameObject.Find("Options_Box").transform;
        pauseScreen = GameObject.Find("PauseScreen");
        scene = SceneManager.GetActiveScene();
        touches = FindObjectOfType<TouchControls>();
        
        currentPosition = ArrowPos.Opt1;
    }

    void Update()
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

        if (oMan.bOptionsActive &&
            !oMan.bPauseOptions &&
            pauseScreen.transform.localScale == Vector3.zero)
        {
            // Controller Support 
            // DC TODO 01/10/2019 -- temp bug where sub-pause menus not closing as expected
            // DC TODO 01/10/2019 -- virtual joystick should be able to select choices
            if (Input.GetAxis("Controller DPad Vertical") == 0 &&
               (!touches.bDown &&
                !touches.bUp))
            {
                bFreezeControllerInput = false;
            }
            else if (!bFreezeControllerInput &&
                    (Input.GetAxis("Controller DPad Vertical") > 0 ||
                    touches.bDown))
            {
                bControllerDown = true;
                bFreezeControllerInput = true;
            }
            else if (!bFreezeControllerInput &&
                    (Input.GetAxis("Controller DPad Vertical") < 0 ||
                    touches.bUp))
            {
                bControllerUp = true;
                bFreezeControllerInput = true;
            }

            if (Input.GetKeyDown(KeyCode.S) ||
                Input.GetKeyDown(KeyCode.DownArrow) ||
                bControllerDown)
            {
                bControllerDown = false;

                if (currentPosition == ArrowPos.Opt1 && oMan.tempOptsCount > 1)
                {
                    currentPosition = ArrowPos.Opt2;
                    ClearAllArrows();
                    Opt2Arw.transform.localScale = Vector3.one;
                }
                else if (currentPosition == ArrowPos.Opt2 && oMan.tempOptsCount > 2)
                {
                    currentPosition = ArrowPos.Opt3;
                    ClearAllArrows();
                    Opt3Arw.transform.localScale = Vector3.one;
                }
                else if (currentPosition == ArrowPos.Opt3 && oMan.tempOptsCount > 3)
                {
                    currentPosition = ArrowPos.Opt4;
                    ClearAllArrows();
                    Opt4Arw.transform.localScale = Vector3.one;
                }
            }
            else if (Input.GetKeyDown(KeyCode.W) ||
                     Input.GetKeyDown(KeyCode.UpArrow) ||
                     bControllerUp)
            {
                bControllerUp = false;
                
                if (currentPosition == ArrowPos.Opt4) 
                {
                    currentPosition = ArrowPos.Opt3;
                    ClearAllArrows();
                    Opt3Arw.transform.localScale = Vector3.one;
                }
                else if (currentPosition == ArrowPos.Opt3)
                {
                    currentPosition = ArrowPos.Opt2;
                    ClearAllArrows();
                    Opt2Arw.transform.localScale = Vector3.one;
                }
                else if (currentPosition == ArrowPos.Opt2)
                {
                    currentPosition = ArrowPos.Opt1;
                    ClearAllArrows();
                    Opt1Arw.transform.localScale = Vector3.one;
                }
            }
            else if (Input.GetButtonDown("Action") ||
                     Input.GetKeyDown(KeyCode.JoystickButton0) ||
                     touches.bAaction)
            {
                if (currentPosition == ArrowPos.Opt1)
                {
                    Opt1Btn.onClick.Invoke();
                }
                else if (currentPosition == ArrowPos.Opt2)
                {
                    Opt2Btn.onClick.Invoke();
                }
                else if (currentPosition == ArrowPos.Opt3)
                {
                    Opt3Btn.onClick.Invoke();
                }
                else if (currentPosition == ArrowPos.Opt4)
                {
                    Opt4Btn.onClick.Invoke();
                }

                ResetArrows();
            }
        }
    }

    public void ClearAllArrows()
    {
        if (oMan.bOptionsActive)
        {
            Opt1Arw.transform.localScale = Vector3.zero;
            Opt2Arw.transform.localScale = Vector3.zero;
            Opt3Arw.transform.localScale = Vector3.zero;
            Opt4Arw.transform.localScale = Vector3.zero;
        }
    }

    public void ResetArrows()
    {
        Opt1Arw.transform.localScale = Vector3.one;
        Opt2Arw.transform.localScale = Vector3.zero;
        Opt3Arw.transform.localScale = Vector3.zero;
        Opt4Arw.transform.localScale = Vector3.zero;

        currentPosition = ArrowPos.Opt1;
    }
}
