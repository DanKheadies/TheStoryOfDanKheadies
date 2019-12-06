﻿// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 11/24/2019
// Last:  12/05/2019

using UnityEngine;
using UnityEngine.UI;

public class TD_SBF_HeroUpgrade : MonoBehaviour
{
    public GameObject sec1;
    public GameObject sec2;
    public GameObject sec3;
    public GameObject sec4;
    public GameObject sec5;
    public GameObject sec6;
    public GameObject sec7;
    public GameObject sec8;
    public TD_SBF_HeroActions heroActions;
    public TD_SBF_HeroBarManager hBarMan;
    public TD_SBF_HeroStats heroStats;
    public TD_SBF_PlayerStatistics pStats;
    public TD_SBF_ThoughtsPrayersUI tpUI;

    public int costSec1;
    public int costSec2;
    public int costSec3;
    public int costSec4;
    public int costSec5;
    public int costSec6;
    public int costSec7;
    public int costSec8;
    public int lvlSec1;
    public int lvlSec2;
    public int lvlSec3;
    public int lvlSec4;
    public int lvlSec5;
    public int lvlSec6;
    public int lvlSec7;
    //public int lvlSec8;

    void Start()
    {
        sec8.GetComponentInChildren<Button>().interactable = false;

        sec1.transform.GetChild(2).GetComponent<Text>().text = costSec1.ToString();
        sec2.transform.GetChild(2).GetComponent<Text>().text = costSec2.ToString();
        sec3.transform.GetChild(2).GetComponent<Text>().text = costSec3.ToString();
        sec4.transform.GetChild(2).GetComponent<Text>().text = costSec4.ToString();
        sec5.transform.GetChild(2).GetComponent<Text>().text = costSec5.ToString();
        sec6.transform.GetChild(2).GetComponent<Text>().text = costSec6.ToString();
        sec7.transform.GetChild(2).GetComponent<Text>().text = costSec7.ToString();
        sec8.transform.GetChild(2).GetComponent<Text>().text = costSec8.ToString();
    }

    public void DisableRevive()
    {
        sec8.GetComponentInChildren<Button>().interactable = false;
    }

    public void EnableRevive()
    {
        sec8.GetComponentInChildren<Button>().interactable = true;
    }

    public void Upgrade1()
    {
        if (TD_SBF_PlayerStatistics.ThoughtsPrayers < costSec1)
        {
            tpUI.FlashWarning();
            hBarMan.ToggleHeroUpgradeShells();
            return;
        }

        if (lvlSec1 == 1)
        {
            TD_SBF_PlayerStatistics.ThoughtsPrayers -= costSec1;
            costSec1 *= 2;
        }
        else if (lvlSec1 == 2)
        {
            TD_SBF_PlayerStatistics.ThoughtsPrayers -= costSec1 * 2;
            costSec1 *= 4;
        }
        else if (lvlSec1 == 3)
        {
            TD_SBF_PlayerStatistics.ThoughtsPrayers -= costSec1 * 4;
            costSec1 = 999;
        }
        else if (lvlSec1 > 3)
        {
            TD_SBF_PlayerStatistics.ThoughtsPrayers -= 999;
            costSec1 = 999;
        }

        lvlSec1++;

        heroStats.health *= 2;
        heroStats.startHealth *= 2;
        heroStats.healthBar.fillAmount = heroStats.health / heroStats.startHealth;

        sec1.transform.GetChild(2).GetComponent<Text>().text = costSec1.ToString();
    }

    public void Upgrade2()
    {
        if (TD_SBF_PlayerStatistics.ThoughtsPrayers < costSec2)
        {
            tpUI.FlashWarning();
            hBarMan.ToggleHeroUpgradeShells();
            return;
        }

        if (lvlSec2 == 1)
        {
            TD_SBF_PlayerStatistics.ThoughtsPrayers -= costSec2;
            costSec2 *= 2;
        }
        else if (lvlSec2 == 2)
        {
            TD_SBF_PlayerStatistics.ThoughtsPrayers -= costSec2;
            costSec2 *= 4;
        }
        else if (lvlSec2 == 3)
        {
            TD_SBF_PlayerStatistics.ThoughtsPrayers -= costSec2;
            costSec2 = 999;
        }
        else if (lvlSec2 > 3)
        {
            TD_SBF_PlayerStatistics.ThoughtsPrayers -= 999;
            costSec2 = 999;
        }

        lvlSec2++;

        TD_SBF_PlayerStatistics.ThoughtsPrayersModifier += 2;

        sec2.transform.GetChild(2).GetComponent<Text>().text = costSec2.ToString();
    }

    public void Upgrade3()
    {
        if (TD_SBF_PlayerStatistics.ThoughtsPrayers < costSec3)
        {
            tpUI.FlashWarning();
            hBarMan.ToggleHeroUpgradeShells();
            return;
        }

        if (lvlSec3 == 1)
        {
            TD_SBF_PlayerStatistics.ThoughtsPrayers -= costSec3;
            costSec3 *= 2;
        }
        else if (lvlSec3 == 2)
        {
            TD_SBF_PlayerStatistics.ThoughtsPrayers -= costSec3;
            costSec3 *= 4;
        }
        else if (lvlSec3 == 3)
        {
            TD_SBF_PlayerStatistics.ThoughtsPrayers -= costSec3;
            costSec3 = 999;
        }
        else if (lvlSec3 > 3)
        {
            TD_SBF_PlayerStatistics.ThoughtsPrayers -= 999;
            costSec3 = 999;
        }

        lvlSec3++;

        heroActions.secondaryAttackWaitTime *= 0.8f;
        hBarMan.secondaryAttackTotalTime *= 0.8f;

        sec3.transform.GetChild(2).GetComponent<Text>().text = costSec3.ToString();
    }

    public void Upgrade4()
    {
        if (TD_SBF_PlayerStatistics.ThoughtsPrayers < costSec4)
        {
            tpUI.FlashWarning();
            hBarMan.ToggleHeroUpgradeShells();
            return;
        }

        if (lvlSec4 == 1)
        {
            TD_SBF_PlayerStatistics.ThoughtsPrayers -= costSec4;
            costSec4 *= 2;
        }
        else if (lvlSec4 == 2)
        {
            TD_SBF_PlayerStatistics.ThoughtsPrayers -= costSec4;
            costSec4 *= 4;
        }
        else if (lvlSec4 == 3)
        {
            TD_SBF_PlayerStatistics.ThoughtsPrayers -= costSec4;
            costSec4 = 999;
        }
        else if (lvlSec4 > 3)
        {
            TD_SBF_PlayerStatistics.ThoughtsPrayers -= 999;
            costSec4 = 999;
        }

        lvlSec4++;

        heroStats.boostModifier *= 2;

        sec4.transform.GetChild(2).GetComponent<Text>().text = costSec4.ToString();
    }

    public void Upgrade5()
    {
        if (TD_SBF_PlayerStatistics.ThoughtsPrayers < costSec5)
        {
            tpUI.FlashWarning();
            hBarMan.ToggleHeroUpgradeShells();
            return;
        }

        if (lvlSec5 == 1)
        {
            TD_SBF_PlayerStatistics.ThoughtsPrayers -= costSec5;
            costSec5 *= 2;
        }
        else if (lvlSec5 == 2)
        {
            TD_SBF_PlayerStatistics.ThoughtsPrayers -= costSec5;
            costSec5 *= 4;
        }
        else if (lvlSec5 == 3)
        {
            TD_SBF_PlayerStatistics.ThoughtsPrayers -= costSec5;
            costSec5 = 999;
        }
        else if (lvlSec5 > 3)
        {
            TD_SBF_PlayerStatistics.ThoughtsPrayers -= 999;
            costSec5 = 999;
        }

        lvlSec5++;

        heroActions.attackRangeX *= 1.1f;
        heroActions.attackRangeY *= 1.1f;

        sec5.transform.GetChild(2).GetComponent<Text>().text = costSec5.ToString();
    }

    public void Upgrade6()
    {
        if (TD_SBF_PlayerStatistics.ThoughtsPrayers < costSec6)
        {
            tpUI.FlashWarning();
            hBarMan.ToggleHeroUpgradeShells();
            return;
        }

        if (lvlSec6 == 1)
        {
            TD_SBF_PlayerStatistics.ThoughtsPrayers -= costSec6;
            costSec6 *= 2;
        }
        else if (lvlSec6 == 2)
        {
            TD_SBF_PlayerStatistics.ThoughtsPrayers -= costSec6;
            costSec6 *= 4;
        }
        else if (lvlSec6 == 3)
        {
            TD_SBF_PlayerStatistics.ThoughtsPrayers -= costSec6;
            costSec6 = 999;
        }
        else if (lvlSec6 > 3)
        {
            TD_SBF_PlayerStatistics.ThoughtsPrayers -= 999;
            costSec6 = 999;
        }

        lvlSec6++;

        heroStats.damage *= 1.3f;

        sec6.transform.GetChild(2).GetComponent<Text>().text = costSec6.ToString();
    }

    public void Upgrade7()
    {
        if (TD_SBF_PlayerStatistics.ThoughtsPrayers < costSec7)
        {
            tpUI.FlashWarning();
            hBarMan.ToggleHeroUpgradeShells();
            return;
        }

        if (lvlSec7 == 1)
        {
            TD_SBF_PlayerStatistics.ThoughtsPrayers -= costSec7;
            costSec7 *= 2;
        }
        else if (lvlSec7 == 2)
        {
            TD_SBF_PlayerStatistics.ThoughtsPrayers -= costSec7;
            costSec7 *= 4;
        }
        else if (lvlSec7 == 3)
        {
            TD_SBF_PlayerStatistics.ThoughtsPrayers -= costSec7;
            costSec7 = 999;
        }
        else if (lvlSec7 > 3)
        {
            TD_SBF_PlayerStatistics.ThoughtsPrayers -= 999;
            costSec7 = 999;
        }

        lvlSec7++;

        heroActions.basicAttackWaitTime *= 0.8f;
        hBarMan.basicAttackTotalTime *= 0.8f;

        sec7.transform.GetChild(2).GetComponent<Text>().text = costSec7.ToString();
    }

    public void Upgrade8()
    {
        if (TD_SBF_PlayerStatistics.ThoughtsPrayers < costSec8)
        {
            tpUI.FlashWarning();
            hBarMan.ToggleHeroUpgradeShells();
            return;
        }

        if (heroStats.bIsDead)
            heroStats.Revive(heroStats.startHealth);
    }
}