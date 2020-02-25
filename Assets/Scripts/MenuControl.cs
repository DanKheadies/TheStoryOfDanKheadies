// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  02/25/2020

using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// For Buttons only: transition scene or quiting game
public class MenuControl : MonoBehaviour
{
    public ControllerSupport contSupp;
    public GameObject danFace;
    public GameObject dummyB;
    public GameObject endB;
    public GameObject goOnB;
    public GameObject newB;
    public GameObject newT;
    public GameObject noB;
    public GameObject smokeRings;
    public GameObject starExploding;
    public GameObject startB;
    public GameObject title;
    public GameObject whiteness;
    public GameObject yesB;

    public bool bAvoidSelection;
    public bool bControllerDown;
    public bool bControllerLeft;
    public bool bControllerRight;
    public bool bControllerUp;
    public bool bFreezeControllerInput;
    public bool bHasSavedData;
    public bool bIsSelectable;

    public string savedScene;

    public enum SelectionPosition : int
    {
        Start = 1,
        GoOn = 2,
        New = 3,
        End = 4,
        ToBeNew = 5,
        NoNew = 6,
        YesNew = 7
    }

    public SelectionPosition currentPosition;

    void Start()
    {
        if (PlayerPrefs.GetInt("Saved") > 0)
        {
            bHasSavedData = true;
            currentPosition = SelectionPosition.GoOn;
        }
        else
            currentPosition = SelectionPosition.Start;

        if (bHasSavedData)
        {
            startB.transform.localScale = Vector3.zero;
            goOnB.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            newB.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        }

        StartCoroutine(InitialDelayedSelection());
    }

    void Update()
    {
        // Skip animations
        if ((Input.GetKeyDown(KeyCode.Space) ||
             Input.GetMouseButtonDown(0) ||
             //Input.GetButtonDown("Controller Bottom Button") ||
             //Input.GetButtonDown("Controller Right Button")) &&
             contSupp.ControllerButtonPadBottom("down") ||
             contSupp.ControllerButtonPadRight("down")) &&
             danFace.GetComponent<CanvasRenderer>().GetAlpha() != 1f)
        {
            starExploding.GetComponent<Animator>().enabled = false;
            starExploding.GetComponent<ImageFader>().enabled = false;
            starExploding.GetComponent<Image>().enabled = false;

            whiteness.GetComponent<ImageFader>().enabled = false;
            whiteness.GetComponent<Image>().enabled = false;

            smokeRings.GetComponent<ImageFader>().enabled = false;
            smokeRings.GetComponent<CanvasRenderer>().SetAlpha(1f);

            title.GetComponent<ImageFader>().enabled = false;
            title.GetComponent<CanvasRenderer>().SetAlpha(1f);

            danFace.GetComponent<ImageFader>().enabled = false;
            danFace.GetComponent<CanvasRenderer>().SetAlpha(1f);

            endB.GetComponent<ImageFader>().enabled = false;
            endB.GetComponent<CanvasRenderer>().SetAlpha(1f);

            if (bHasSavedData)
            {
                goOnB.GetComponent<ImageFader>().enabled = false;
                goOnB.GetComponent<CanvasRenderer>().SetAlpha(1f);

                newB.GetComponent<ImageFader>().enabled = false;
                newB.GetComponent<CanvasRenderer>().SetAlpha(1f);
            }
            else
            {
                startB.GetComponent<ImageFader>().enabled = false;
                startB.GetComponent<CanvasRenderer>().SetAlpha(1f);
            }

            StartCoroutine(DelaySelectable());
        }

        if ((Input.GetKeyDown(KeyCode.Space) ||
             Input.GetMouseButtonDown(0) ||
             //Input.GetButtonDown("Controller Bottom Button")) &&
             contSupp.ControllerButtonPadBottom("down")) &&
             bIsSelectable &&
             !bAvoidSelection)
        {
            SelectOption();
        }

        // Controller Support 
        //if (Input.GetAxis("Controller DPad Vertical") == 0 &&
        //        Input.GetAxis("Controller Joystick Vertical") == 0 &&
        //        Input.GetAxis("Controller DPad Horizontal") == 0 &&
        //        Input.GetAxis("Controller Joystick Horizontal") == 0)
        if (!contSupp.bIsMoving)
        //if (contSupp.ControllerDirectionalPadVertical() == 0 &&
        //    contSupp.ControllerLeftJoystickVertical() == 0 &&
        //    contSupp.ControllerDirectionalPadHorizontal() == 0 &&
        //    contSupp.ControllerLeftJoystickHorizontal() == 0)
        {
            bFreezeControllerInput = false;
        }
        else if (!bFreezeControllerInput &&
                 //(Input.GetAxis("Controller DPad Vertical") > 0 ||
                 // Input.GetAxis("Controller Joystick Vertical") < 0))
                 (contSupp.ControllerDirectionalPadVertical() < 0 ||
                  contSupp.ControllerLeftJoystickVertical() < 0))
        {
            bControllerDown = true;
            bFreezeControllerInput = true;
        }
        else if (!bFreezeControllerInput &&
                 //(Input.GetAxis("Controller DPad Vertical") < 0 ||
                 // Input.GetAxis("Controller Joystick Vertical") > 0))
                 (contSupp.ControllerDirectionalPadVertical() > 0 ||
                  contSupp.ControllerLeftJoystickVertical() > 0))
        {
            bControllerUp = true;
            bFreezeControllerInput = true;
        }
        else if (!bFreezeControllerInput &&
                 //(Input.GetAxis("Controller DPad Horizontal") > 0 ||
                 // Input.GetAxis("Controller Joystick Horizontal") > 0))
                 (contSupp.ControllerDirectionalPadHorizontal() > 0 ||
                  contSupp.ControllerLeftJoystickHorizontal() > 0))
        {
            bControllerRight = true;
            bFreezeControllerInput = true;
        }
        else if (!bFreezeControllerInput &&
                 //(Input.GetAxis("Controller DPad Horizontal") < 0 ||
                 // Input.GetAxis("Controller Joystick Horizontal") < 0))
                 (contSupp.ControllerDirectionalPadHorizontal() < 0 ||
                  contSupp.ControllerLeftJoystickHorizontal() < 0))
        {
            bControllerLeft = true;
            bFreezeControllerInput = true;
        }

        if (Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.DownArrow) ||
            bControllerDown)
        {
            bControllerDown = false;
            bAvoidSelection = false;

            if (bIsSelectable)
                MoveDown();
        }
        else if (Input.GetKeyDown(KeyCode.W) ||
                 Input.GetKeyDown(KeyCode.UpArrow) ||
                 bControllerUp)
        {
            bControllerUp = false;
            bAvoidSelection = false;

            if (bIsSelectable)
                MoveUp();
        }
        else if (Input.GetKeyDown(KeyCode.A) ||
                 Input.GetKeyDown(KeyCode.LeftArrow) ||
                 bControllerLeft)
        {
            bControllerLeft = false;
            bAvoidSelection = false;

            if (bIsSelectable)
                MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.D) ||
                 Input.GetKeyDown(KeyCode.RightArrow) ||
                 bControllerRight)
        {
            bControllerRight = false;
            bAvoidSelection = false;

            if (bIsSelectable)
                MoveRight();
        }
    }

    IEnumerator DelaySelectable()
    {
        yield return new WaitForSeconds(0.5f);

        bIsSelectable = true;

        if (bHasSavedData)
            goOnB.GetComponent<Button>().Select();
        else
            startB.GetComponent<Button>().Select();
    }

    IEnumerator InitialDelayedSelection()
    {
        yield return new WaitForSeconds(17f);
        
        if (!bIsSelectable)
        {
            bIsSelectable = true;

            if (bHasSavedData)
                goOnB.GetComponent<Button>().Select();
            else
                startB.GetComponent<Button>().Select();
        }
    }

    public void MoveDown()
    {
        if (bHasSavedData)
        {
            if (currentPosition == SelectionPosition.GoOn)
            {
                currentPosition = SelectionPosition.New;
                newB.GetComponent<Button>().Select();
            }
            else if (currentPosition == SelectionPosition.New)
            {
                currentPosition = SelectionPosition.End;
                endB.GetComponent<Button>().Select();
            }
        }
        else 
            if (currentPosition == SelectionPosition.Start)
            {
                currentPosition = SelectionPosition.End;
                endB.GetComponent<Button>().Select();
            }
    }

    public void MoveUp()
    {
        if (bHasSavedData)
        {
            if (currentPosition == SelectionPosition.End)
            {
                currentPosition = SelectionPosition.New;
                newB.GetComponent<Button>().Select();
            }
            else if (currentPosition == SelectionPosition.New)
            {
                currentPosition = SelectionPosition.GoOn;
                goOnB.GetComponent<Button>().Select();
            }
        }
        else
            if (currentPosition == SelectionPosition.End)
        {
            currentPosition = SelectionPosition.Start;
            startB.GetComponent<Button>().Select();
        }
    }

    public void MoveLeft()
    {
        if (currentPosition == SelectionPosition.NoNew)
        {
            currentPosition = SelectionPosition.ToBeNew;
            newB.GetComponent<Button>().Select();
        }
        else if (currentPosition == SelectionPosition.ToBeNew)
        {
            currentPosition = SelectionPosition.YesNew;
            yesB.GetComponent<Button>().Select();
        }
    }

    public void MoveRight()
    {
        if (currentPosition == SelectionPosition.YesNew)
        {
            currentPosition = SelectionPosition.ToBeNew;
            newB.GetComponent<Button>().Select();
        }
        else if (currentPosition == SelectionPosition.ToBeNew)
        {
            currentPosition = SelectionPosition.NoNew;
            noB.GetComponent<Button>().Select();
        }
    }

    public void SelectOption()
    {
        if (currentPosition == SelectionPosition.Start)
            startB.GetComponent<Button>().onClick.Invoke();
        else if (currentPosition == SelectionPosition.GoOn)
            goOnB.GetComponent<Button>().onClick.Invoke();
        else if (currentPosition == SelectionPosition.New)
            newB.GetComponent<Button>().onClick.Invoke();
        else if (currentPosition == SelectionPosition.End)
            endB.GetComponent<Button>().onClick.Invoke();
        else if (currentPosition == SelectionPosition.YesNew)
            yesB.GetComponent<Button>().onClick.Invoke();
        else if (currentPosition == SelectionPosition.NoNew)
            noB.GetComponent<Button>().onClick.Invoke();
    }

    public void GoToScene()
    {
        Time.timeScale = 1;

        if (PlayerPrefs.GetString("Chapter") != "")
            SceneManager.LoadScene(PlayerPrefs.GetString("Chapter"));
        else
            SceneManager.LoadScene("Chp0");
    }

    public void NewGame()
    {
        startB.transform.localScale = Vector3.zero;
        goOnB.transform.localScale = Vector3.zero;
        newB.transform.localScale = Vector3.zero;
        endB.transform.localScale = Vector3.zero;
        newT.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        yesB.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        noB.transform.localScale = new Vector3(0.5f, 0.5f, 1f);

        currentPosition = SelectionPosition.ToBeNew;
    }

    public void NewGameNo()
    {
        newT.transform.localScale = Vector3.zero;
        yesB.transform.localScale = Vector3.zero;
        noB.transform.localScale = Vector3.zero;

        if (PlayerPrefs.GetInt("Saved") > 0)
        {
            goOnB.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            newB.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        }
        else
        {
            startB.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        }

        endB.transform.localScale = new Vector3(0.5f, 0.5f, 1f);

        currentPosition = SelectionPosition.New;
        newB.GetComponent<Button>().Select();
    }

    public void NewGameYes()
    {
        PlayerPrefs.DeleteAll();
        GoToScene();
    }

    public void AvoidSelection()
    {
        bAvoidSelection = true;
        EventSystem.current.SetSelectedGameObject(null);
    }
}
