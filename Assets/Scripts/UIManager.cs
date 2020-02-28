// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  02/27/2020

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Manage Overworld UI Display
public class UIManager : MonoBehaviour
{
    public Camera mainCamera;
    public Canvas HUD;
    public CanvasGroup guiControlsCan;
    public ControllerSupport contSupp;
    public DeviceDetector devDetect;
    public DialogueManager dMan;
    public FixedJoystick fixedJoystick;
    public GameObject pauseButtOpac; 
    public GameObject[] dPads;
    public GameObject[] joySticks;
    public OptionsManager oMan;
    public PlayerBrioManager playerBrio;
    public RectTransform controlsMenu;
    public RectTransform gwcMenu;
    public RectTransform iconsMenu;
    public RectTransform pauseMenu;
    public RectTransform soundMenu;
    public RectTransform stuffMenu;
    public Scene scene;
    public Slider brioBar;
    public Slider fullBrioBar;
    public Slider contOpacSlider;
    public Text fullBrioText;
    public Toggle conTog;
    public Toggle dPadTog;
    public TouchControls touches;

    public bool bControlsActive;
    public bool bControlsDPad;

    public float currentContOpac;

    public int currentContDPad;
    
    void Start ()
    {
        // Initializers 
        scene = SceneManager.GetActiveScene();

        // Sets initial activation off saved data (or transfer, which always saves UI)
        // In other words, this first IF only occurs on Chp0 - New Game
        if (!PlayerPrefs.HasKey("ControlsActive"))
            CheckIfMobile();
        else
        {
            if (PlayerPrefs.GetInt("ControlsActive") == 1)
                DisplayControls(); 
            else
                HideControls();
        }

        // Sets initial opacity based off saved data (or transfer, which always saves UI)
        // In other words, this first IF only occurs on Chp0 - New Game
        if (!PlayerPrefs.HasKey("ControlsOpac"))
        {
            currentContOpac = 1.0f;
            contOpacSlider.value = 1.0f;
            guiControlsCan.alpha = 1.0f;
        }
        else
        {
            currentContOpac = PlayerPrefs.GetFloat("ControlsOpac");
            contOpacSlider.value = currentContOpac;
            guiControlsCan.alpha = currentContOpac;
        }

        // Sets initial control type based off saved data (or transfer, which always saves UI)
        // In other words, this first IF only occurs on Chp0 - New Game
        if (!PlayerPrefs.HasKey("ControlsDPad"))
        {
            currentContDPad = 1;
            DisplayDPad();
        }
        else
        {
            currentContDPad = PlayerPrefs.GetInt("ControlsDPad");

            if (currentContDPad == 1)
            {
                DisplayDPad();
            }
            else if (currentContDPad == 0)
            {
                HideDPad();
            }
        }

        // Sets menu position based off scene
        CheckAndSetMenus();

        // Sets brio bar
        UpdateBrio();
    }

    public void UpdateBrio()
    {
        brioBar.maxValue = playerBrio.playerMaxBrio;
        brioBar.value = playerBrio.playerCurrentBrio;

        fullBrioBar.maxValue = playerBrio.playerMaxBrio;
        fullBrioBar.value = playerBrio.playerCurrentBrio;
        fullBrioText.text = "BR:  " + (int)(playerBrio.playerCurrentBrio) + 
            " / " + (int)(playerBrio.playerMaxBrio);
    }

    public void DisplayControls()
    {
        bControlsActive = true;
        conTog.isOn = true;
        touches.transform.localScale = Vector3.one;

        fixedJoystick.GetComponent<FixedJoystick>().JoystickPosition();
    }

    public void HideControls()
    {
        bControlsActive = false;
        conTog.isOn = false;
        touches.transform.localScale = Vector3.zero;
    }

    public void CheckIfMobile()
    {
        // Set based off device
        //#if !UNITY_EDITOR
        //    #if UNITY_IOS
        //        bMobileDevice = true;
        //    #endif

        //    #if UNITY_ANDROID
        //        bMobileDevice = true;
        //    #endif
        //#endif

        devDetect.CheckIfMobile();
        contSupp.FindControllers();
        contSupp.CheckControllers();

        // Show GUI Controls for Mobile Devices
        if (devDetect.bIsMobile &&
            !contSupp.bControllerConnected)
        {
            DisplayControls();
        }
        else
            HideControls();
    }

    // Toggles the UI controls
    public void ToggleControls()
    {
        if (bControlsActive)
            HideControls();
        else if (!bControlsActive)
            DisplayControls();
    }

    public void CheckAndSetControls()
    {
        if (bControlsActive)
            DisplayControls();
        else if (!bControlsActive)
            HideControls();
    }

    public void DisplayDPad()
    {
        bControlsDPad = true;
        currentContDPad = 1;
        dPadTog.isOn = true;

        foreach (GameObject dPad in dPads)
        {
            dPad.transform.localScale = Vector3.one;
        }
        foreach (GameObject joyStick in joySticks)
        {
            joyStick.transform.localScale = Vector3.zero;
        }
    }

    public void HideDPad()
    {
        bControlsDPad = false;
        currentContDPad = 0;
        dPadTog.isOn = false;

        foreach (GameObject dPad in dPads)
        {
            dPad.transform.localScale = Vector3.zero;
        }
        foreach (GameObject joyStick in joySticks)
        {
            joyStick.transform.localScale = Vector3.one;
        }

        fixedJoystick.GetComponent<FixedJoystick>().JoystickPosition();
    }

    // Toggles the movement type control
    public void ToggleDPadControl()
    {
        if (bControlsDPad)
            HideDPad();
        else if (!bControlsDPad)
            DisplayDPad();
    }

    public void CheckAndSetDPad()
    {
        if (bControlsDPad)
            DisplayDPad();
        else if (!bControlsDPad)
            HideDPad();
    }

    public void HideBrioAndButton()
    {
        brioBar.gameObject.transform.localScale = Vector3.zero;
        fullBrioBar.gameObject.transform.localScale = Vector3.zero;
        pauseButtOpac.transform.localScale = Vector3.zero;
    }

    public void ShowBrioAndButton()
    {
        brioBar.gameObject.transform.localScale = Vector3.one;
        fullBrioBar.gameObject.transform.localScale = Vector3.one;
        pauseButtOpac.transform.localScale = Vector3.one;
    }

    // Adjust the opacity of the UI controls
    public void ContOpacSliderChange()
    {
        currentContOpac = contOpacSlider.value;
        guiControlsCan.alpha = currentContOpac;
    }

    public void CheckAndSetMenus()
    {
        // Width > height = center in the screen
        if (Screen.width >= Screen.height)
        {
            controlsMenu.anchorMin = new Vector2(0.5f, 0.5f);
            controlsMenu.anchorMax = new Vector2(0.5f, 0.5f);
            controlsMenu.anchoredPosition = new Vector2(0, 0);

            pauseMenu.anchorMin = new Vector2(0.5f, 0.5f);
            pauseMenu.anchorMax = new Vector2(0.5f, 0.5f);
            pauseMenu.anchoredPosition = new Vector2(0, 0);

            soundMenu.anchorMin = new Vector2(0.5f, 0.5f);
            soundMenu.anchorMax = new Vector2(0.5f, 0.5f);
            soundMenu.anchoredPosition = new Vector2(0, 0);

            stuffMenu.anchorMin = new Vector2(0.5f, 0.5f);
            stuffMenu.anchorMax = new Vector2(0.5f, 0.5f);
            stuffMenu.anchoredPosition = new Vector2(0, 0);

            if (scene.name == "GuessWhoColluded")
            {
                gwcMenu.anchorMin = new Vector2(0.5f, 0.5f);
                gwcMenu.anchorMax = new Vector2(0.5f, 0.5f);
                gwcMenu.anchoredPosition = new Vector2(140f, 0);

                iconsMenu.anchorMin = new Vector2(0.5f, 0.5f);
                iconsMenu.anchorMax = new Vector2(0.5f, 0.5f);
                iconsMenu.anchoredPosition = new Vector2(0, 0);

                pauseMenu.anchoredPosition = new Vector2(-140f, 0);
            }
        }
        // Height > width = stick to the top but below brio and menu button
        else
        {
            controlsMenu.anchorMin = new Vector2(0.5f, 1f);
            controlsMenu.anchorMax = new Vector2(0.5f, 1f);
            controlsMenu.anchoredPosition = new Vector2(0, -275f);

            pauseMenu.anchorMin = new Vector2(0.5f, 1f);
            pauseMenu.anchorMax = new Vector2(0.5f, 1f);
            pauseMenu.anchoredPosition = new Vector2(0, -275f);

            soundMenu.anchorMin = new Vector2(0.5f, 1f);
            soundMenu.anchorMax = new Vector2(0.5f, 1f);
            soundMenu.anchoredPosition = new Vector2(0, -275f);

            stuffMenu.anchorMin = new Vector2(0.5f, 1f);
            stuffMenu.anchorMax = new Vector2(0.5f, 1f);
            stuffMenu.anchoredPosition = new Vector2(0, -275f);

            if (scene.name == "GuessWhoColluded")
            {
                gwcMenu.anchorMin = new Vector2(0.5f, 1f);
                gwcMenu.anchorMax = new Vector2(0.5f, 1f);
                gwcMenu.anchoredPosition = new Vector2(140f, -275f);

                iconsMenu.anchorMin = new Vector2(0.5f, 1f);
                iconsMenu.anchorMax = new Vector2(0.5f, 1f);
                iconsMenu.anchoredPosition = new Vector2(0, -275f);

                pauseMenu.anchoredPosition = new Vector2(-140f, -275f);
            }
        }
    }
}
