// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 05/04/2020
// Last:  04/26/2021

using UnityEngine;
using UnityEngine.UI;

// To "move" and execute the buttons / arrows on the TD SBF Pause Menu
public class TD_SBF_MovePauseMenuSelector : MonoBehaviour
{
    public Button audioBackBtn;
    public Button controlsBackBtn;
    public Button creditsBackBtn;
    public Button mainEndBtn;
    public Button mainGoOnBtn;
    public Button mainOptionsBtn;
    public Button muorBackBtn;
    public Button muorCreditsBtn;
    public Button optsAudioBtn;
    public Button optsBackBtn;
    public Button optsControlsBtn;
    public Button optsMuorBtn;
    public ControllerSupport contSupp;
    public GameObject eventSys;
    public GameObject controlsDescSelector;
    public GameObject controlsOpaSelector;
    public GameObject controlsVibSelector;
    public GameObject menuMain;
    public GameObject menuOptions;
    public GameObject menuAudio;
    public GameObject menuControls;
    public GameObject menuMuor;
    public GameObject menuCredits;
    public GameObject musicSelector;
    public GameObject pauseCanvas;
    public GameObject volumeSelector;
    public ScriptManager scriptMan;
    public Slider controlsOpacSlider;
    public Slider musicSlider;
    public Slider volumeSlider;
    public TD_SBF_PauseMenu pause;
    public TD_SBF_TouchControls touches;
    public TD_SBF_VolumeManager vMan;
    public Toggle controlsVibToggle;

    public bool bControllerDown;
    public bool bControllerDownSecondary;
    public bool bControllerLeft;
    public bool bControllerRight;
    public bool bControllerUp;
    public bool bControllerUpSecondary;
    public bool bFreezeControllerInput;

    public enum SelectorPosition : int
    {
        goOn = 1,
        options = 2,
        end = 3,
        audio = 11,
        controls = 12,
        muor = 13,
        back = 14,
        volume = 21,
        music = 22,
        audioBack = 23,
        controlsOpacity = 31,
        controlsVibrate = 32,
        controlsDescription = 33,
        controlsBack = 34,
        credits = 41,
        muorBack = 42,
        creditsBack = 51
    }

    public SelectorPosition currentPosition;

    void Start()
    {
        currentPosition = SelectorPosition.goOn;
    }

    void Update()
    {
        if (pauseCanvas.activeSelf)
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
            else if (!bFreezeControllerInput &&
                     (contSupp.ControllerDirectionalPadHorizontal() > 0 ||
                      contSupp.ControllerLeftJoystickHorizontal() > 0))
            {
                bControllerRight = true;
                bFreezeControllerInput = true;
            }
            else if (!bFreezeControllerInput &&
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

                if (menuMain.activeSelf)
                {
                    if (currentPosition == SelectorPosition.goOn)
                    {
                        currentPosition = SelectorPosition.options;
                        mainOptionsBtn.Select();
                    }
                    else if (currentPosition == SelectorPosition.options)
                    {
                        currentPosition = SelectorPosition.end;
                        mainEndBtn.Select();
                    }
                    else if (currentPosition == SelectorPosition.end)
                    {
                        currentPosition = SelectorPosition.goOn;
                        mainGoOnBtn.Select();
                    }
                }
                else if (menuOptions.activeSelf) 
                {
                    if (currentPosition == SelectorPosition.audio)
                    {
                        currentPosition = SelectorPosition.controls;
                        optsControlsBtn.Select();
                    }
                    else if (currentPosition == SelectorPosition.controls)
                    {
                        currentPosition = SelectorPosition.muor;
                        optsMuorBtn.Select();
                    }
                    else if (currentPosition == SelectorPosition.muor)
                    {
                        currentPosition = SelectorPosition.back;
                        optsBackBtn.Select();
                    }
                    else if (currentPosition == SelectorPosition.back)
                    {
                        currentPosition = SelectorPosition.audio;
                        optsAudioBtn.Select();
                    }
                }
                else if (menuAudio.activeSelf)
                {
                    if (currentPosition == SelectorPosition.volume)
                    {
                        currentPosition = SelectorPosition.music;
                        volumeSelector.transform.localScale = Vector3.zero;
                        musicSelector.transform.localScale = Vector3.one;
                    }
                    else if (currentPosition == SelectorPosition.music)
                    {
                        currentPosition = SelectorPosition.audioBack;
                        audioBackBtn.Select();
                        musicSelector.transform.localScale = Vector3.zero;
                    }
                    else if (currentPosition == SelectorPosition.audioBack)
                    {
                        currentPosition = SelectorPosition.volume;
                        DeselectAll();
                        volumeSelector.transform.localScale = Vector3.one;
                    }
                }
                else if (menuControls.activeSelf &&
                         controlsOpacSlider)
                {
                    if (currentPosition == SelectorPosition.controlsOpacity)
                    {
                        currentPosition = SelectorPosition.controlsVibrate;
                        controlsOpaSelector.transform.localScale = Vector3.zero;
                        controlsVibSelector.transform.localScale = Vector3.one;
                    }
                    else if (currentPosition == SelectorPosition.controlsVibrate)
                    {
                        currentPosition = SelectorPosition.controlsDescription;
                        controlsVibSelector.transform.localScale = Vector3.zero;
                        controlsDescSelector.transform.localScale = Vector3.one;
                    }
                    else if (currentPosition == SelectorPosition.controlsDescription)
                    {
                        currentPosition = SelectorPosition.controlsBack;
                        controlsDescSelector.transform.localScale = Vector3.zero;
                        controlsBackBtn.Select();
                    }
                    else if (currentPosition == SelectorPosition.controlsBack)
                    {
                        currentPosition = SelectorPosition.controlsOpacity;
                        DeselectAll();
                        controlsOpaSelector.transform.localScale = Vector3.one;
                    }
                }
                else if (menuMuor.activeSelf)
                {
                    if (currentPosition == SelectorPosition.credits)
                    {
                        currentPosition = SelectorPosition.muorBack;
                        muorBackBtn.Select();
                    }
                    else if (currentPosition == SelectorPosition.muorBack)
                    {
                        currentPosition = SelectorPosition.credits;
                        muorCreditsBtn.Select();
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.W) ||
                     Input.GetKeyDown(KeyCode.UpArrow) ||
                     bControllerUp)
            {
                bControllerUp = false;

                if (menuMain.activeSelf)
                {
                    if (currentPosition == SelectorPosition.goOn)
                    {
                        currentPosition = SelectorPosition.end;
                        mainEndBtn.Select();
                    }
                    else if (currentPosition == SelectorPosition.options)
                    {
                        currentPosition = SelectorPosition.goOn;
                        mainGoOnBtn.Select();
                    }
                    else if (currentPosition == SelectorPosition.end)
                    {
                        currentPosition = SelectorPosition.options;
                        mainOptionsBtn.Select();
                    }
                }
                else if (menuOptions.activeSelf)
                {
                    if (currentPosition == SelectorPosition.audio)
                    {
                        currentPosition = SelectorPosition.back;
                        optsBackBtn.Select();
                    }
                    else if (currentPosition == SelectorPosition.controls)
                    {
                        currentPosition = SelectorPosition.audio;
                        optsAudioBtn.Select();
                    }
                    else if (currentPosition == SelectorPosition.muor)
                    {
                        currentPosition = SelectorPosition.controls;
                        optsControlsBtn.Select();
                    }
                    else if (currentPosition == SelectorPosition.back)
                    {
                        currentPosition = SelectorPosition.muor;
                        optsMuorBtn.Select();
                    }
                }
                else if (menuAudio.activeSelf)
                {
                    if (currentPosition == SelectorPosition.volume)
                    {
                        currentPosition = SelectorPosition.audioBack;
                        volumeSelector.transform.localScale = Vector3.zero;
                        audioBackBtn.Select();
                    }
                    else if (currentPosition == SelectorPosition.music)
                    {
                        currentPosition = SelectorPosition.volume;
                        musicSelector.transform.localScale = Vector3.zero;
                        volumeSelector.transform.localScale = Vector3.one;
                    }
                    else if (currentPosition == SelectorPosition.audioBack)
                    {
                        currentPosition = SelectorPosition.music;
                        DeselectAll();
                        musicSelector.transform.localScale = Vector3.one;
                    }
                }
                else if (menuControls.activeSelf &&
                         controlsOpacSlider)
                {
                    if (currentPosition == SelectorPosition.controlsOpacity)
                    {
                        currentPosition = SelectorPosition.controlsBack;
                        controlsOpaSelector.transform.localScale = Vector3.zero;
                        controlsBackBtn.Select();
                    }
                    else if (currentPosition == SelectorPosition.controlsVibrate)
                    {
                        currentPosition = SelectorPosition.controlsOpacity;
                        controlsVibSelector.transform.localScale = Vector3.zero;
                        controlsOpaSelector.transform.localScale = Vector3.one;
                    }
                    else if (currentPosition == SelectorPosition.controlsDescription)
                    {
                        currentPosition = SelectorPosition.controlsVibrate;
                        controlsDescSelector.transform.localScale = Vector3.zero;
                        controlsVibSelector.transform.localScale = Vector3.one;
                    }
                    else if (currentPosition == SelectorPosition.controlsBack)
                    {
                        currentPosition = SelectorPosition.controlsDescription;
                        DeselectAll();
                        controlsDescSelector.transform.localScale = Vector3.one;
                    }
                }
                else if (menuMuor.activeSelf)
                {
                    if (currentPosition == SelectorPosition.credits)
                    {
                        currentPosition = SelectorPosition.muorBack;
                        muorBackBtn.Select();
                    }
                    else if (currentPosition == SelectorPosition.muorBack)
                    {
                        currentPosition = SelectorPosition.credits;
                        muorCreditsBtn.Select();
                    }
                }
            }

            else if (Input.GetKeyDown(KeyCode.A) ||
                     Input.GetKeyDown(KeyCode.LeftArrow) ||
                     bControllerLeft)
            {
                bControllerLeft = false;

                if (currentPosition == SelectorPosition.volume)
                {
                    vMan.LowerVolume();
                    vMan.AdjustSliders();
                }
                else if (currentPosition == SelectorPosition.music)
                {
                    vMan.LowerMusic();
                    vMan.AdjustSliders();
                }
                else if (currentPosition == SelectorPosition.controlsOpacity)
                {
                    touches.DecreaseOpacity();
                    touches.AdjustSlider();
                }
            }
            else if (Input.GetKeyDown(KeyCode.D) ||
                     Input.GetKeyDown(KeyCode.RightArrow) ||
                     bControllerRight)
            {
                bControllerRight = false;

                if (currentPosition == SelectorPosition.volume)
                {
                    vMan.RaiseVolume();
                    vMan.AdjustSliders();
                }
                else if (currentPosition == SelectorPosition.music)
                {
                    vMan.RaiseMusic();
                    vMan.AdjustSliders();
                }
                else if (currentPosition == SelectorPosition.controlsOpacity)
                {
                    touches.IncreaseOpacity();
                    touches.AdjustSlider();
                }
            }

            else if (Input.GetButtonDown("Action") ||
                     contSupp.ControllerButtonPadBottom("down"))
            {
                if (currentPosition == SelectorPosition.goOn)
                {
                    mainGoOnBtn.onClick.Invoke();
                    //scriptMan.ResetParameters("TD_SBF_ModeSelector");
                }
                else if (currentPosition == SelectorPosition.options)
                {
                    mainOptionsBtn.onClick.Invoke();
                    DeselectAll();
                    currentPosition = SelectorPosition.audio;
                    optsAudioBtn.Select();
                }
                else if (currentPosition == SelectorPosition.end)
                {
                    mainEndBtn.onClick.Invoke();
                    DeselectAll();
                }
                else if (currentPosition == SelectorPosition.audio)
                {
                    optsAudioBtn.onClick.Invoke();
                    DeselectAll();
                    currentPosition = SelectorPosition.volume;
                    volumeSelector.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.controls)
                {
                    optsControlsBtn.onClick.Invoke();
                    DeselectAll();

                    if (controlsOpacSlider)
                    {
                        currentPosition = SelectorPosition.controlsOpacity;
                        controlsOpaSelector.transform.localScale = Vector3.one;
                    }
                    else
                    {
                        currentPosition = SelectorPosition.controlsBack;
                        controlsBackBtn.Select();
                    }
                }
                else if (currentPosition == SelectorPosition.muor)
                {
                    optsMuorBtn.onClick.Invoke();
                    DeselectAll();
                    currentPosition = SelectorPosition.credits;
                    muorCreditsBtn.Select();
                }
                else if (currentPosition == SelectorPosition.back)
                {
                    optsBackBtn.onClick.Invoke();
                    DeselectAll();
                    currentPosition = SelectorPosition.goOn;
                    mainGoOnBtn.Select();
                }
                else if (currentPosition == SelectorPosition.audioBack)
                {
                    audioBackBtn.onClick.Invoke();
                    DeselectAll();
                    currentPosition = SelectorPosition.audio;
                    optsAudioBtn.Select();
                }
                else if (currentPosition == SelectorPosition.controlsVibrate)
                {
                    controlsVibToggle.isOn = !controlsVibToggle.isOn;
                    touches.ToggleVibrate();
                    touches.Vibrate();
                }
                else if (currentPosition == SelectorPosition.controlsBack)
                {
                    controlsBackBtn.onClick.Invoke();
                    DeselectAll();
                    currentPosition = SelectorPosition.controls;
                    optsControlsBtn.Select();
                }
                else if (currentPosition == SelectorPosition.credits)
                {
                    muorCreditsBtn.onClick.Invoke();
                    DeselectAll();
                    currentPosition = SelectorPosition.creditsBack;
                    creditsBackBtn.Select();
                }
                else if (currentPosition == SelectorPosition.muorBack)
                {
                    muorBackBtn.onClick.Invoke();
                    DeselectAll();
                    currentPosition = SelectorPosition.audio;
                    optsAudioBtn.Select();
                }
                else if (currentPosition == SelectorPosition.creditsBack)
                {
                    creditsBackBtn.onClick.Invoke();
                    DeselectAll();
                    currentPosition = SelectorPosition.credits;
                    muorCreditsBtn.Select();
                }
            }

            else if (contSupp.ControllerButtonPadRight("down"))
            {
                DeselectAll();
                
                if (menuMain.activeSelf)
                {
                    mainGoOnBtn.onClick.Invoke();
                }
                else if (menuOptions.activeSelf)
                {
                    pause.ToggleOptions();
                    currentPosition = SelectorPosition.goOn;
                    mainGoOnBtn.Select();
                }
                else if (menuAudio.activeSelf )
                {
                    pause.ToggleAudio();
                    currentPosition = SelectorPosition.audio;
                    optsAudioBtn.Select();
                }
                else if (menuControls.activeSelf)
                {
                    pause.ToggleControls();
                    currentPosition = SelectorPosition.audio;
                    optsAudioBtn.Select();
                }
                else if (menuMuor.activeSelf)
                {
                    pause.ToggleMuor();
                    currentPosition = SelectorPosition.audio;
                    optsAudioBtn.Select();
                }
                else if (menuCredits.activeSelf)
                {
                    pause.ToggleCredits();
                    currentPosition = SelectorPosition.credits;
                    muorCreditsBtn.Select();
                }
            }
        }

        if (contSupp.ControllerRightJoystickVertical() > 0)
        {
            if (menuControls.activeSelf)
            {
                if (menuControls.transform.GetChild(0).GetChild(0).GetChild(0).
                    GetComponent<RectTransform>().offsetMin.y < 0)
                    menuControls.transform.GetChild(0).GetChild(0).GetChild(0).localPosition = new Vector3(
                        menuControls.transform.GetChild(0).GetChild(0).GetChild(0).localPosition.x,
                        menuControls.transform.GetChild(0).GetChild(0).GetChild(0).localPosition.y + 10 * contSupp.ControllerRightJoystickVertical(),
                        menuControls.transform.GetChild(0).GetChild(0).GetChild(0).localPosition.z);
                else
                    menuControls.transform.GetChild(0).GetChild(0).GetChild(0).
                        GetComponent<RectTransform>().offsetMin = new Vector2(
                            menuControls.transform.GetChild(0).GetChild(0).GetChild(0).
                                GetComponent<RectTransform>().offsetMin.x,
                            0);
            }
            else if (menuCredits.activeSelf)
            {
                if (menuCredits.transform.GetChild(0).GetChild(0).GetChild(0).
                    GetComponent<RectTransform>().offsetMin.y < 0)
                    menuCredits.transform.GetChild(0).GetChild(0).GetChild(0).localPosition = new Vector3(
                        menuCredits.transform.GetChild(0).GetChild(0).GetChild(0).localPosition.x,
                        menuCredits.transform.GetChild(0).GetChild(0).GetChild(0).localPosition.y + 10 * contSupp.ControllerRightJoystickVertical(),
                        menuCredits.transform.GetChild(0).GetChild(0).GetChild(0).localPosition.z);
                else
                    menuCredits.transform.GetChild(0).GetChild(0).GetChild(0).
                        GetComponent<RectTransform>().offsetMin = new Vector2(
                            menuCredits.transform.GetChild(0).GetChild(0).GetChild(0).
                                GetComponent<RectTransform>().offsetMin.x,
                            0);
            }
        }
        else if (contSupp.ControllerRightJoystickVertical() < 0)
        {
            if (menuControls.activeSelf)
            {
                if (menuControls.transform.GetChild(0).GetChild(0).GetChild(0).
                    GetComponent<RectTransform>().offsetMax.y * -1f < 0)
                    menuControls.transform.GetChild(0).GetChild(0).GetChild(0).localPosition = new Vector3(
                        menuControls.transform.GetChild(0).GetChild(0).GetChild(0).localPosition.x,
                        menuControls.transform.GetChild(0).GetChild(0).GetChild(0).localPosition.y + 10 * contSupp.ControllerRightJoystickVertical(),
                        menuControls.transform.GetChild(0).GetChild(0).GetChild(0).localPosition.z);
                else
                    menuControls.transform.GetChild(0).GetChild(0).GetChild(0).
                        GetComponent<RectTransform>().offsetMax = new Vector2(
                            menuControls.transform.GetChild(0).GetChild(0).GetChild(0).
                                GetComponent<RectTransform>().offsetMax.x,
                            0);
            }
            else if (menuCredits.activeSelf)
            {
                if (menuCredits.transform.GetChild(0).GetChild(0).GetChild(0).
                    GetComponent<RectTransform>().offsetMax.y * -1f < 0)
                    menuCredits.transform.GetChild(0).GetChild(0).GetChild(0).localPosition = new Vector3(
                        menuCredits.transform.GetChild(0).GetChild(0).GetChild(0).localPosition.x,
                        menuCredits.transform.GetChild(0).GetChild(0).GetChild(0).localPosition.y + 10 * contSupp.ControllerRightJoystickVertical(),
                        menuCredits.transform.GetChild(0).GetChild(0).GetChild(0).localPosition.z);
                else
                    menuCredits.transform.GetChild(0).GetChild(0).GetChild(0).
                        GetComponent<RectTransform>().offsetMax = new Vector2(
                            menuCredits.transform.GetChild(0).GetChild(0).GetChild(0).
                                GetComponent<RectTransform>().offsetMax.x,
                            0);
            }
        }
    }

    public void DeselectAll()
    {
        eventSys.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

    public void ResetMenuSelection()
    {
        currentPosition = SelectorPosition.goOn;
        menuMain.SetActive(true);
        menuOptions.SetActive(false);
        menuAudio.SetActive(false);
        menuControls.SetActive(false);
        menuMuor.SetActive(false);
        menuCredits.SetActive(false);
    }
}
