// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 09/27/2019
// Last:  04/26/2021

using UnityEngine;
using UnityEngine.UI;

public class TD_SBF_ModeSelector : MonoBehaviour
{
    public Button backToModes;
    public Button[] characterButtons;
    public Button[] modeButtons;
    public ControllerSupport contSupp;
    public GameObject characterCanvas;
    public GameObject modeCanvas;
    public GameObject optionsCanvas;
    public Scrollbar charScrollBar;
    public TD_SBF_MoveModeMenuSelector mmmSelector;
    public TD_SBF_MovePauseMenuSelector mpmSelector;
    public TD_SBF_SceneFader fader;

    public bool bControllerDown;
    //public bool bControllerLeft;
    //public bool bControllerRight;
    public bool bControllerUp;
    public bool bFreezeControllerInput;

    public enum SelectorPosition : int
    {
        jesus = 1,
        modes = 10
    }

    public SelectorPosition currentPosition;

    void Start()
    {
        currentPosition = SelectorPosition.jesus;

        modeButtons[0].interactable = false;
        modeButtons[2].interactable = false;

        Color temp = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 125.0f / 255.0f);

        characterButtons[1].interactable = false;
        characterButtons[2].interactable = false;
        characterButtons[3].interactable = false;
        characterButtons[4].interactable = false;
        characterButtons[5].interactable = false;
        characterButtons[6].interactable = false;
        characterButtons[7].interactable = false;

        characterButtons[1].transform.GetChild(0).GetComponent<Image>().color = temp;
        characterButtons[2].transform.GetChild(0).GetComponent<Image>().color = temp;
        characterButtons[3].transform.GetChild(0).GetComponent<Image>().color = temp;
        characterButtons[4].transform.GetChild(0).GetComponent<Image>().color = temp;
        characterButtons[5].transform.GetChild(0).GetComponent<Image>().color = temp;
        characterButtons[6].transform.GetChild(0).GetComponent<Image>().color = temp;
        characterButtons[7].transform.GetChild(0).GetComponent<Image>().color = temp;


        //int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        //for (int i = 0; i < levelButtons.Length; i++)
        //{
        //    if (i + 1 > levelReached)
        //        levelButtons[i].interactable = false;
        //}
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) ||
             Input.GetKeyDown(KeyCode.P) ||
             contSupp.ControllerMenuRight("down")) &&
             !characterCanvas.activeSelf)
        {
            ToggleOptions();
        }

        if (characterCanvas.activeSelf)
        {
            // Controller Support 
            if (!contSupp.bIsMoving)
                bFreezeControllerInput = false;
            else if (!bFreezeControllerInput &&
                     (contSupp.ControllerDirectionalPadVertical() < 0 ||
                      contSupp.ControllerLeftJoystickVertical() < 0))
            {
                bControllerDown = true;
                bFreezeControllerInput = true;
            }
            else if (!bFreezeControllerInput &&
                     (contSupp.ControllerDirectionalPadVertical() > 0 ||
                      contSupp.ControllerLeftJoystickVertical() > 0))
            {
                bControllerUp = true;
                bFreezeControllerInput = true;
            }
            //else if (!bFreezeControllerInput &&
            //         (contSupp.ControllerDirectionalPadHorizontal() > 0 ||
            //          contSupp.ControllerLeftJoystickHorizontal() > 0))
            //{
            //    bControllerRight = true;
            //    bFreezeControllerInput = true;
            //}
            //else if (!bFreezeControllerInput &&
            //         (contSupp.ControllerDirectionalPadHorizontal() < 0 ||
            //          contSupp.ControllerLeftJoystickHorizontal() < 0))
            //{
            //    bControllerLeft = true;
            //    bFreezeControllerInput = true;
            //}

            if (Input.GetKeyDown(KeyCode.S) ||
                Input.GetKeyDown(KeyCode.DownArrow) ||
                bControllerDown)
            {
                bControllerDown = false;

                if (currentPosition == SelectorPosition.jesus)
                {
                    currentPosition = SelectorPosition.modes;
                    backToModes.Select();
                }
                else if (currentPosition == SelectorPosition.modes)
                {
                    currentPosition = SelectorPosition.jesus;
                    characterButtons[0].Select();
                }
            }
            else if (Input.GetKeyDown(KeyCode.W) ||
                     Input.GetKeyDown(KeyCode.UpArrow) ||
                     bControllerUp)
            {
                bControllerUp = false;

                if (currentPosition == SelectorPosition.jesus)
                {
                    currentPosition = SelectorPosition.modes;
                    backToModes.Select();
                }
                else if (currentPosition == SelectorPosition.modes)
                {
                    currentPosition = SelectorPosition.jesus;
                    characterButtons[0].Select();
                }
            }

            //else if (Input.GetKeyDown(KeyCode.A) ||
            //         Input.GetKeyDown(KeyCode.LeftArrow) ||
            //         bControllerLeft)
            //{
            //    bControllerLeft = false;

            //    if (currentPosition == SelectorPosition.volume)
            //        vMan.LowerVolume();
            //    else if (currentPosition == SelectorPosition.music)
            //        vMan.LowerMusic();
            //}
            //else if (Input.GetKeyDown(KeyCode.D) ||
            //         Input.GetKeyDown(KeyCode.RightArrow) ||
            //         bControllerRight)
            //{
            //    bControllerRight = false;

            //    if (currentPosition == SelectorPosition.volume)
            //        vMan.RaiseVolume();
            //    else if (currentPosition == SelectorPosition.music)
            //        vMan.RaiseMusic();
            //}

            else if (Input.GetButtonDown("Action") ||
                     contSupp.ControllerButtonPadBottom("down"))
            {
                if (currentPosition == SelectorPosition.jesus)
                    characterButtons[0].onClick.Invoke();
                else if (currentPosition == SelectorPosition.modes)
                    backToModes.onClick.Invoke();
            }

            else if (contSupp.ControllerButtonPadRight("down"))
            {
                backToModes.onClick.Invoke();
                BackToArcade();
            }

            if (contSupp.ControllerRightJoystickVertical() > 0)
            {
                if (characterCanvas.transform.GetChild(1).GetChild(0).GetChild(0).
                    GetComponent<RectTransform>().offsetMin.y < 0)
                    characterCanvas.transform.GetChild(1).GetChild(0).GetChild(0).localPosition = new Vector3(
                        characterCanvas.transform.GetChild(1).GetChild(0).GetChild(0).localPosition.x,
                        characterCanvas.transform.GetChild(1).GetChild(0).GetChild(0).localPosition.y + 10 * contSupp.ControllerRightJoystickVertical(),
                        characterCanvas.transform.GetChild(1).GetChild(0).GetChild(0).localPosition.z);
                else
                    characterCanvas.transform.GetChild(1).GetChild(0).GetChild(0).
                        GetComponent<RectTransform>().offsetMin = new Vector2(
                            characterCanvas.transform.GetChild(1).GetChild(0).GetChild(0).
                                GetComponent<RectTransform>().offsetMin.x,
                            0);
            }
            else if (contSupp.ControllerRightJoystickVertical() < 0)
            {
                if (characterCanvas.transform.GetChild(1).GetChild(0).GetChild(0).
                    GetComponent<RectTransform>().offsetMax.y * -1f < 0)
                    characterCanvas.transform.GetChild(1).GetChild(0).GetChild(0).localPosition = new Vector3(
                        characterCanvas.transform.GetChild(1).GetChild(0).GetChild(0).localPosition.x,
                        characterCanvas.transform.GetChild(1).GetChild(0).GetChild(0).localPosition.y + 10 * contSupp.ControllerRightJoystickVertical(),
                        characterCanvas.transform.GetChild(1).GetChild(0).GetChild(0).localPosition.z);
                else
                    characterCanvas.transform.GetChild(1).GetChild(0).GetChild(0).
                        GetComponent<RectTransform>().offsetMax = new Vector2(
                            characterCanvas.transform.GetChild(1).GetChild(0).GetChild(0).
                                GetComponent<RectTransform>().offsetMax.x,
                            0);
            }
        }
    }

    public void ToggleMode()
    {
        if (modeCanvas.activeSelf)
        {
            modeCanvas.SetActive(false);
            characterCanvas.SetActive(true);

            characterButtons[0].Select();

            // Force scrollbar to top on open
            charScrollBar.value = 1;
        }
        else
        {
            modeCanvas.SetActive(true);
            characterCanvas.SetActive(false);

            BackToArcade();
        }
    }

    public void ToggleOptions()
    {
        if (modeCanvas.activeSelf)
        {
            modeCanvas.SetActive(false);
            optionsCanvas.SetActive(true);

            optionsCanvas.transform.GetChild(0).GetComponent<TD_SBF_MovePauseMenuSelector>().
                mainGoOnBtn.Select();
        }
        else
        {
            modeCanvas.SetActive(true);
            optionsCanvas.SetActive(false);
            
            BackToArcade();
            mpmSelector.ResetMenuSelection();
        }
    }

    public void SelectStory()
    {
        //Debug.Log("Selected Story");
    }

    public void SelectArcade()
    {
        //Debug.Log("Selected Arcade");
    }

    public void SelectMultiplayer()
    {
        //Debug.Log("Selected Multiplayer");
    }

    public void LoadLevel(string levelName)
    {
        fader.FadeTo(levelName);
    }

    public void BackToArcade()
    {
        mmmSelector.arcadeBtn.Select();
        mmmSelector.currentPosition =
            TD_SBF_MoveModeMenuSelector.SelectorPosition.arcade;

        mmmSelector.bDelayOnSwitch = true;
    }

    public void EndGame()
    {
        // TODO: for tSoDK
        PlayerPrefs.SetInt("Transferring", 1);
        PlayerPrefs.SetString("TransferScene", "Chp1");

        // TODO: for standalone
        //Application.Quit();
    }
}
