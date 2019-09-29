// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 09/26/2019
// Last:  09/26/2019

using UnityEngine;
using UnityEngine.UI;

public class TD_SBF_HeroBarManager : MonoBehaviour
{
    public Image basicAttackBar;
    public Image secondaryAttackBar;
    public TD_SBF_HeroActions heroActions;

    public float basicAttackCounter;
    public float basicAttackTotalTime;
    public float secondaryAttackCounter;
    public float secondaryAttackTotalTime;

    void Start()
    {
        basicAttackTotalTime = heroActions.basicAttackWaitTime;
        secondaryAttackTotalTime = heroActions.secondaryAttackWaitTime;
    }

    void Update()
    {
        basicAttackCounter = heroActions.basicAttackWaitTime - 
            heroActions.basicAttackWaitCounter;
        secondaryAttackCounter = heroActions.secondaryAttackWaitTime - 
            heroActions.secondaryAttackWaitCounter;

        basicAttackBar.fillAmount = basicAttackCounter / basicAttackTotalTime;
        secondaryAttackBar.fillAmount = secondaryAttackCounter / secondaryAttackTotalTime;
    }

    public void UpgradeAttacks()
    {
        Debug.Log("Show costs over each attack w/ grey backdrop dimming the buttons.");
    }
}
