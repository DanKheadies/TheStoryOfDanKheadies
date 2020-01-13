// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 09/26/2019
// Last:  12/12/2019

using UnityEngine;
using UnityEngine.UI;

public class TD_SBF_HeroBarManager : MonoBehaviour
{
    public Color basicAttackColor;
    public Color secondaryAttackColor;
    public GameObject heroShell;
    public GameObject heroBackButton;
    public GameObject upgradeShell;
    public GameObject upgradeBackButton;
    public Image basicAttackBar;
    public Image secondaryAttackBar;
    public TD_SBF_ControllerSupport contSupp;
    public TD_SBF_HeroActions heroActions;
    public TD_SBF_HeroStats heroStats;
    public TD_SBF_HeroUpgrade heroUpgrade;
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

        basicAttackBar.fillAmount = basicAttackCounter / basicAttackTotalTime;
        secondaryAttackBar.fillAmount = secondaryAttackCounter / secondaryAttackTotalTime;
    }

    public void ToggleHeroUpgradeShells()
    {
        bUpgrading = !bUpgrading;

        heroShell.SetActive(!heroShell.activeSelf);
        heroBackButton.SetActive(!heroBackButton.activeSelf);

        upgradeShell.SetActive(!upgradeShell.activeSelf);
        upgradeBackButton.SetActive(!upgradeBackButton.activeSelf);

        if (contSupp.bControllerConnected)
        {
            heroUpgrade.currentSelection = (TD_SBF_HeroUpgrade.UpgradeSection)1;
            heroUpgrade.sec1.GetComponentInChildren<Button>().Select();
        }

        if (bUpgrading)
            touchConts.HideControls();
        else
            touchConts.DisplayControls();
    }

    public void DisableHeroAttacks()
    {
        basicAttackCounter = heroActions.basicAttackWaitTime;
        secondaryAttackCounter = heroActions.secondaryAttackWaitTime;

        basicAttackBar.fillAmount = 0;
        secondaryAttackBar.fillAmount = 0;

        basicAttackBar.transform.parent.parent.GetChild(1).GetComponent<Button>()
            .interactable = false;
        basicAttackBar.transform.parent.parent.GetChild(1).GetComponent<Animator>()
            .enabled = false;
        basicAttackBar.transform.parent.parent.GetChild(1).GetComponent<Image>()
            .color = new Color(0f / 255f, 0f / 255f, 0f / 255f, 125f / 255f);

        secondaryAttackBar.transform.parent.parent.GetChild(1).GetComponent<Button>()
            .interactable = false;
        secondaryAttackBar.transform.parent.parent.GetChild(1).GetComponent<Animator>()
            .enabled = false;
        secondaryAttackBar.transform.parent.parent.GetChild(1).GetComponent<Image>()
            .color = new Color(0f / 255f, 0f / 255f, 0f / 255f, 125f / 255f);
    }

    public void EnableHeroAttacks()
    {
        basicAttackCounter = heroActions.basicAttackWaitTime;
        secondaryAttackCounter = heroActions.secondaryAttackWaitTime;

        basicAttackBar.fillAmount = basicAttackCounter / basicAttackTotalTime;
        secondaryAttackBar.fillAmount = secondaryAttackCounter / secondaryAttackTotalTime;

        basicAttackBar.transform.parent.parent.GetChild(1).GetComponent<Button>()
            .interactable = true;
        basicAttackBar.transform.parent.parent.GetChild(1).GetComponent<Animator>()
            .enabled = true;
        basicAttackBar.transform.parent.parent.GetChild(1).GetComponent<Image>()
            .color = basicAttackColor;

        secondaryAttackBar.transform.parent.parent.GetChild(1).GetComponent<Button>()
            .interactable = true;
        secondaryAttackBar.transform.parent.parent.GetChild(1).GetComponent<Animator>()
            .enabled = true;
        secondaryAttackBar.transform.parent.parent.GetChild(1).GetComponent<Image>()
            .color = secondaryAttackColor;
    }
}
