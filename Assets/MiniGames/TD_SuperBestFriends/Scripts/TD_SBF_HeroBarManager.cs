// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 09/26/2019
// Last:  06/26/2021

using UnityEngine;
using UnityEngine.UI;

public class TD_SBF_HeroBarManager : MonoBehaviour
{
    public Color basicAttackColor;
    public Color secondaryAttackColor;
    public ControllerSupport contSupp;
    public DeviceDetector devDetect;
    public GameObject heroShell_H;
    public GameObject heroShell_V;
    public GameObject heroBackButton_H;
    public GameObject heroBackButton_V;
    public GameObject upgradeShell_H;
    public GameObject upgradeShell_V;
    public GameObject upgradeBackButton_H;
    public GameObject upgradeBackButton_V;
    public Image basicAttackBar_H;
    public Image basicAttackBar_V;
    public Image secondaryAttackBar_H;
    public Image secondaryAttackBar_V;
    public TD_SBF_ControlManagement cMan;
    public TD_SBF_HeroActions heroActions;
    public TD_SBF_HeroMovement heroMove;
    public TD_SBF_HeroStats heroStats;
    public TD_SBF_HeroUpgrade heroUpgrade_H;
    public TD_SBF_HeroUpgrade heroUpgrade_V;
    public TD_SBF_TouchControls touchConts;

    public bool bUpgrading;
    public float basicAttackCounter;
    public float basicAttackTotalTime;
    public float secondaryAttackCounter;
    public float secondaryAttackTotalTime;

    void Start()
    {
        basicAttackTotalTime = heroActions.basicAttackWaitTime;
        secondaryAttackTotalTime = heroActions.secondaryAttackWaitTime;

        basicAttackColor = new Color(136f / 255f, 157f / 255f, 255f / 255f, 125f / 255f);
        secondaryAttackColor = new Color(207f / 255f, 178f / 255f, 0f / 255f, 125f / 255f);
    }

    void Update()
    {
        if (heroStats.bIsDead)
            return;

        basicAttackCounter = heroActions.basicAttackWaitTime - 
            heroActions.basicAttackWaitCounter;
        secondaryAttackCounter = heroActions.secondaryAttackWaitTime - 
            heroActions.secondaryAttackWaitCounter;

        if (cMan.bIsHorizontal)
        {
            basicAttackBar_H.fillAmount = basicAttackCounter / basicAttackTotalTime;
            secondaryAttackBar_H.fillAmount = secondaryAttackCounter / secondaryAttackTotalTime;
        }
        else
        {
            basicAttackBar_V.fillAmount = basicAttackCounter / basicAttackTotalTime;
            secondaryAttackBar_V.fillAmount = secondaryAttackCounter / secondaryAttackTotalTime;
        }
    }

    public void ToggleHeroUpgradeShells()
    {
        bUpgrading = !bUpgrading;

        if (cMan.bIsHorizontal)
        {
            heroShell_H.SetActive(!heroShell_H.activeSelf);
            heroBackButton_H.SetActive(!heroBackButton_H.activeSelf);

            upgradeShell_H.SetActive(!upgradeShell_H.activeSelf);
            upgradeBackButton_H.SetActive(!upgradeBackButton_H.activeSelf);
        }
        else
        {
            heroShell_V.SetActive(!heroShell_V.activeSelf);
            heroBackButton_V.SetActive(!heroBackButton_V.activeSelf);

            upgradeShell_V.SetActive(!upgradeShell_V.activeSelf);
            upgradeBackButton_V.SetActive(!upgradeBackButton_V.activeSelf);
        }


        if (contSupp.bControllerConnected)
        {
            if (cMan.bIsHorizontal)
            {
                heroUpgrade_H.currentSelection = (TD_SBF_HeroUpgrade.UpgradeSection)1;
                heroUpgrade_H.sec1.GetComponentInChildren<Button>().Select();
            }
            else
            {
                heroUpgrade_V.currentSelection = (TD_SBF_HeroUpgrade.UpgradeSection)1;
                heroUpgrade_V.sec1.GetComponentInChildren<Button>().Select();
            }
        }
        
        if (devDetect.bIsMobile)
        {
            if (bUpgrading)
                touchConts.HideControls();
            else
                touchConts.DisplayControls();
        }
    }

    public void DisableHeroAttacks()
    {
        basicAttackCounter = heroActions.basicAttackWaitTime;
        secondaryAttackCounter = heroActions.secondaryAttackWaitTime;

        if (cMan.bIsHorizontal)
        {
            basicAttackBar_H.fillAmount = 0;
            secondaryAttackBar_H.fillAmount = 0;

            basicAttackBar_H.transform.parent.parent.GetChild(1).GetComponent<Button>()
                .interactable = false;
            basicAttackBar_H.transform.parent.parent.GetChild(1).GetComponent<Animator>()
                .enabled = false;
            basicAttackBar_H.transform.parent.parent.GetChild(1).GetComponent<Image>()
                .color = new Color(0f / 255f, 0f / 255f, 0f / 255f, 125f / 255f);

            secondaryAttackBar_H.transform.parent.parent.GetChild(1).GetComponent<Button>()
                .interactable = false;
            secondaryAttackBar_H.transform.parent.parent.GetChild(1).GetComponent<Animator>()
                .enabled = false;
            secondaryAttackBar_H.transform.parent.parent.GetChild(1).GetComponent<Image>()
                .color = new Color(0f / 255f, 0f / 255f, 0f / 255f, 125f / 255f);
        }
        else
        {
            basicAttackBar_V.fillAmount = 0;
            secondaryAttackBar_V.fillAmount = 0;

            basicAttackBar_V.transform.parent.parent.GetChild(1).GetComponent<Button>()
                .interactable = false;
            basicAttackBar_V.transform.parent.parent.GetChild(1).GetComponent<Animator>()
                .enabled = false;
            basicAttackBar_V.transform.parent.parent.GetChild(1).GetComponent<Image>()
                .color = new Color(0f / 255f, 0f / 255f, 0f / 255f, 125f / 255f);

            secondaryAttackBar_V.transform.parent.parent.GetChild(1).GetComponent<Button>()
                .interactable = false;
            secondaryAttackBar_V.transform.parent.parent.GetChild(1).GetComponent<Animator>()
                .enabled = false;
            secondaryAttackBar_V.transform.parent.parent.GetChild(1).GetComponent<Image>()
                .color = new Color(0f / 255f, 0f / 255f, 0f / 255f, 125f / 255f);
        }
    }

    public void EnableHeroAttacks()
    {
        basicAttackCounter = heroActions.basicAttackWaitTime;
        secondaryAttackCounter = heroActions.secondaryAttackWaitTime;

        if (cMan.bIsHorizontal)
        {
            basicAttackBar_H.fillAmount = basicAttackCounter / basicAttackTotalTime;
            secondaryAttackBar_H.fillAmount = secondaryAttackCounter / secondaryAttackTotalTime;

            basicAttackBar_H.transform.parent.parent.GetChild(1).GetComponent<Button>()
                .interactable = true;
            basicAttackBar_H.transform.parent.parent.GetChild(1).GetComponent<Animator>()
                .enabled = true;
            basicAttackBar_H.transform.parent.parent.GetChild(1).GetComponent<Image>()
                .color = basicAttackColor;

            secondaryAttackBar_H.transform.parent.parent.GetChild(1).GetComponent<Button>()
                .interactable = true;
            secondaryAttackBar_H.transform.parent.parent.GetChild(1).GetComponent<Animator>()
                .enabled = true;
            secondaryAttackBar_H.transform.parent.parent.GetChild(1).GetComponent<Image>()
                .color = secondaryAttackColor;
        }
        else
        {
            basicAttackBar_V.fillAmount = basicAttackCounter / basicAttackTotalTime;
            secondaryAttackBar_V.fillAmount = secondaryAttackCounter / secondaryAttackTotalTime;

            basicAttackBar_V.transform.parent.parent.GetChild(1).GetComponent<Button>()
                .interactable = true;
            basicAttackBar_V.transform.parent.parent.GetChild(1).GetComponent<Animator>()
                .enabled = true;
            basicAttackBar_V.transform.parent.parent.GetChild(1).GetComponent<Image>()
                .color = basicAttackColor;

            secondaryAttackBar_V.transform.parent.parent.GetChild(1).GetComponent<Button>()
                .interactable = true;
            secondaryAttackBar_V.transform.parent.parent.GetChild(1).GetComponent<Animator>()
                .enabled = true;
            secondaryAttackBar_V.transform.parent.parent.GetChild(1).GetComponent<Image>()
                .color = secondaryAttackColor;
        }
    }

    public void ShowGUIControls() 
    {
        touchConts.ShowOnMobile();
    }

    public void HideGUIControls()
    {
        touchConts.HideOnMobile();
    }
}
