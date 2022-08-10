// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 01/14/2020
// Last:  10/30/2021

using System.Collections;
using UnityEngine;

public class AreaAnimator : MonoBehaviour
{
    public CameraFollow camFollow;
    public GameObject[] EastArmGrass;
    public GameObject[] EastArmFlowers;
    public GameObject[] NorthArmGrass;
    public GameObject[] NorthArmFlowers;
    public GameObject[] SouthArmGrass;
    public GameObject[] SouthArmFlowers;
    public GameObject[] WestArmGrass;
    public GameObject[] WestArmFlowers;

    public GameObject[] BatteryNWTrees;
    public GameObject[] BatterySETrees;
    public GameObject[] CampusGrass;
    public GameObject[] CampusFlowers;
    public GameObject[] CampusTrees;
    public GameObject[] CannaHouseTrees;
    public GameObject[] HomeTrees;
    public GameObject[] HousesETrees;
    public GameObject[] HousesNTrees;
    public GameObject[] HousesSTrees;
    public GameObject[] HousesWTrees;
    public GameObject[] LakeTrees;
    public GameObject[] PlaygroundNTrees;
    public GameObject[] PlaygroundSTrees;
    public GameObject[] RiverTrees;
    public GameObject[] WoodsWTrees;
    public GameObject[] WoodsWSecretTrees;

    public bool bAlterateGrass;
    
    void Start()
    {
        StartCoroutine(DelayAreaCheck());
    }

    //EastArm
    public void CycleEastArmFlowers()
    {
        EastArmFlowers[0].SetActive(false);
        EastArmFlowers[1].SetActive(true);

        StartCoroutine(NormalizeEastArmFlowers());
    }

    public IEnumerator NormalizeEastArmFlowers()
    {
        yield return new WaitForSeconds(1f);

        EastArmFlowers[0].SetActive(true);
        EastArmFlowers[1].SetActive(false);
    }

    public void CycleEastArmGrass()
    {
        if (EastArmGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                EastArmGrass[1].SetActive(true);
            else
                EastArmGrass[2].SetActive(true);

            EastArmGrass[0].SetActive(false);
        }
        else if (EastArmGrass[1].activeSelf)
        {
            EastArmGrass[0].SetActive(true);
            EastArmGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (EastArmGrass[2].activeSelf)
        {
            EastArmGrass[0].SetActive(true);
            EastArmGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
    }


    //NorthArm
    public void CycleNorthArmFlowers()
    {
        NorthArmFlowers[0].SetActive(false);
        NorthArmFlowers[1].SetActive(true);

        StartCoroutine(NormalizeNorthArmFlowers());
    }

    public IEnumerator NormalizeNorthArmFlowers()
    {
        yield return new WaitForSeconds(1f);

        NorthArmFlowers[0].SetActive(true);
        NorthArmFlowers[1].SetActive(false);
    }

    public void CycleNorthArmGrass()
    {
        if (NorthArmGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                NorthArmGrass[1].SetActive(true);
            else
                NorthArmGrass[2].SetActive(true);

            NorthArmGrass[0].SetActive(false);
        }
        else if (NorthArmGrass[1].activeSelf)
        {
            NorthArmGrass[0].SetActive(true);
            NorthArmGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (NorthArmGrass[2].activeSelf)
        {
            NorthArmGrass[0].SetActive(true);
            NorthArmGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
    }


    //SouthArm
    public void CycleSouthArmFlowers()
    {
        SouthArmFlowers[0].SetActive(false);
        SouthArmFlowers[1].SetActive(true);

        StartCoroutine(NormalizeSouthArmFlowers());
    }

    public IEnumerator NormalizeSouthArmFlowers()
    {
        yield return new WaitForSeconds(1f);

        SouthArmFlowers[0].SetActive(true);
        SouthArmFlowers[1].SetActive(false);
    }

    public void CycleSouthArmGrass()
    {
        if (SouthArmGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                SouthArmGrass[1].SetActive(true);
            else
                SouthArmGrass[2].SetActive(true);

            SouthArmGrass[0].SetActive(false);
        }
        else if (SouthArmGrass[1].activeSelf)
        {
            SouthArmGrass[0].SetActive(true);
            SouthArmGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (SouthArmGrass[2].activeSelf)
        {
            SouthArmGrass[0].SetActive(true);
            SouthArmGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
    }


    //WestArm
    public void CycleWestArmFlowers()
    {
        WestArmFlowers[0].SetActive(false);
        WestArmFlowers[1].SetActive(true);

        StartCoroutine(NormalizeWestArmFlowers());
    }

    public IEnumerator NormalizeWestArmFlowers()
    {
        yield return new WaitForSeconds(1f);

        WestArmFlowers[0].SetActive(true);
        WestArmFlowers[1].SetActive(false);
    }

    public void CycleWestArmGrass()
    {
        if (WestArmGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                WestArmGrass[1].SetActive(true);
            else
                WestArmGrass[2].SetActive(true);

            WestArmGrass[0].SetActive(false);
        }
        else if (WestArmGrass[1].activeSelf)
        {
            WestArmGrass[0].SetActive(true);
            WestArmGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (WestArmGrass[2].activeSelf)
        {
            WestArmGrass[0].SetActive(true);
            WestArmGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
    }

    public void ToggleBatteryNWTrees()
    {
        for (int i = 0; i < BatteryNWTrees.Length; i++)
            BatteryNWTrees[i].GetComponent<Animator>().enabled = !BatteryNWTrees[i].GetComponent<Animator>().enabled;
    }

    public void ToggleBatterySETrees()
    {
        for (int i = 0; i < BatterySETrees.Length; i++)
            BatterySETrees[i].GetComponent<Animator>().enabled = !BatterySETrees[i].GetComponent<Animator>().enabled;
    }


    //Campus 
    public void CycleCampusFlowers()
    {
        CampusFlowers[0].SetActive(false);
        CampusFlowers[1].SetActive(true);

        StartCoroutine(NormalizeCampusFlowers());
    }

    public IEnumerator NormalizeCampusFlowers()
    {
        yield return new WaitForSeconds(1f);

        CampusFlowers[0].SetActive(true);
        CampusFlowers[1].SetActive(false);
    }

    public void CycleCampusGrass()
    {
        if (CampusGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                CampusGrass[1].SetActive(true);
            else
                CampusGrass[2].SetActive(true);

            CampusGrass[0].SetActive(false);
        }
        else if (CampusGrass[1].activeSelf)
        {
            CampusGrass[0].SetActive(true);
            CampusGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (CampusGrass[2].activeSelf)
        {
            CampusGrass[0].SetActive(true);
            CampusGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
    }

    public void ToggleCampusTrees()
    {
        for (int i = 0; i < CampusTrees.Length; i++)
            CampusTrees[i].GetComponent<Animator>().enabled = !CampusTrees[i].GetComponent<Animator>().enabled;
    }

    public void ToggleCannaHouseTrees()
    {
        for (int i = 0; i < CannaHouseTrees.Length; i++)
            CannaHouseTrees[i].GetComponent<Animator>().enabled = !CannaHouseTrees[i].GetComponent<Animator>().enabled;
    }

    public void ToggleHomeTrees()
    {
        for (int i = 0; i < HomeTrees.Length; i++)
            HomeTrees[i].GetComponent<Animator>().enabled = !HomeTrees[i].GetComponent<Animator>().enabled;
    }

    public void ToggleHousesETrees()
    {
        for (int i = 0; i < HousesETrees.Length; i++)
            HousesETrees[i].GetComponent<Animator>().enabled = !HousesETrees[i].GetComponent<Animator>().enabled;
    }

    public void ToggleHousesNTrees()
    {
        for (int i = 0; i < HousesNTrees.Length; i++)
            HousesNTrees[i].GetComponent<Animator>().enabled = !HousesNTrees[i].GetComponent<Animator>().enabled;
    }

    public void ToggleHousesSTrees()
    {
        for (int i = 0; i < HousesSTrees.Length; i++)
            HousesSTrees[i].GetComponent<Animator>().enabled = !HousesSTrees[i].GetComponent<Animator>().enabled;
    }

    public void ToggleHousesWTrees()
    {
        for (int i = 0; i < HousesWTrees.Length; i++)
            HousesWTrees[i].GetComponent<Animator>().enabled = !HousesWTrees[i].GetComponent<Animator>().enabled;
    }

    public void ToggleLakeTrees()
    {
        for (int i = 0; i < LakeTrees.Length; i++)
            LakeTrees[i].GetComponent<Animator>().enabled = !LakeTrees[i].GetComponent<Animator>().enabled;
    }

    public void TogglePlaygroundNTrees()
    {
        for (int i = 0; i < PlaygroundNTrees.Length; i++)
            PlaygroundNTrees[i].GetComponent<Animator>().enabled = !PlaygroundNTrees[i].GetComponent<Animator>().enabled;
    }

    public void TogglePlaygroundSTrees()
    {
        for (int i = 0; i < PlaygroundSTrees.Length; i++)
            PlaygroundSTrees[i].GetComponent<Animator>().enabled = !PlaygroundSTrees[i].GetComponent<Animator>().enabled;
    }

    public void ToggleRiverTrees()
    {
        for (int i = 0; i < RiverTrees.Length; i++)
            RiverTrees[i].GetComponent<Animator>().enabled = !RiverTrees[i].GetComponent<Animator>().enabled;
    }

    public void ToggleWoodsWTrees()
    {
        for (int i = 0; i < WoodsWTrees.Length; i++)
            WoodsWTrees[i].GetComponent<Animator>().enabled = !WoodsWTrees[i].GetComponent<Animator>().enabled;
    }

    public void ToggleWoodsWSecretTrees()
    {
        for (int i = 0; i < WoodsWSecretTrees.Length; i++)
            WoodsWSecretTrees[i].GetComponent<Animator>().enabled = !WoodsWSecretTrees[i].GetComponent<Animator>().enabled;
    }


    public void DisableAnimators()
    {
        // TODO: finish adding trees in WoodsW; see how to use Profiler first for performance understanding

        if (camFollow.currentCoords == CameraFollow.AnandaCoords.BatteryNE)
        {
            if (BatteryNWTrees[0].GetComponent<Animator>().enabled)
                ToggleBatteryNWTrees();

            if (BatterySETrees[0].GetComponent<Animator>().enabled)
                ToggleBatterySETrees();
        }

        if (camFollow.currentCoords == CameraFollow.AnandaCoords.BatterySE)
        {
            if (HousesETrees[0].GetComponent<Animator>().enabled)
                ToggleHousesETrees();
        }

        if (camFollow.currentCoords == CameraFollow.AnandaCoords.BatterySW)
        {
            if (BatteryNWTrees[0].GetComponent<Animator>().enabled)
                ToggleBatteryNWTrees();

            if (BatterySETrees[0].GetComponent<Animator>().enabled)
                ToggleBatterySETrees();

            if (CampusTrees[0].GetComponent<Animator>().enabled)
                ToggleCampusTrees();
        }

        if (camFollow.currentCoords == CameraFollow.AnandaCoords.BuildersNE)
        {
            if (HousesNTrees[0].GetComponent<Animator>().enabled)
                ToggleHousesNTrees();
        }

        if (camFollow.currentCoords == CameraFollow.AnandaCoords.BuildersSE)
        {
            if (CampusTrees[0].GetComponent<Animator>().enabled)
                ToggleCampusTrees();
        }

        if (camFollow.currentCoords == CameraFollow.AnandaCoords.Campus)
        {
            if (CannaHouseTrees[0].GetComponent<Animator>().enabled)
                ToggleCannaHouseTrees();
        }

        if (camFollow.currentCoords == CameraFollow.AnandaCoords.CannaFieldNW)
        {
            if (CannaHouseTrees[0].GetComponent<Animator>().enabled)
                ToggleCannaHouseTrees();

            if (HomeTrees[0].GetComponent<Animator>().enabled)
                ToggleHomeTrees();
        }

        if (camFollow.currentCoords == CameraFollow.AnandaCoords.CannaFieldSE)
        {
            if (CannaHouseTrees[0].GetComponent<Animator>().enabled)
                ToggleCannaHouseTrees();
        }

        if (camFollow.currentCoords == CameraFollow.AnandaCoords.CannaHouse)
        {
            if (CampusTrees[0].GetComponent<Animator>().enabled)
                ToggleCampusTrees();
        }

        if (camFollow.currentCoords == CameraFollow.AnandaCoords.FarmNW)
        {
            if (CampusTrees[0].GetComponent<Animator>().enabled)
                ToggleCampusTrees();
        }

        if (camFollow.currentCoords == CameraFollow.AnandaCoords.FarmSW)
        {
            if (HousesSTrees[0].GetComponent<Animator>().enabled)
                ToggleHousesSTrees();
        }

        if (camFollow.currentCoords == CameraFollow.AnandaCoords.Home)
        {
            if (HousesWTrees[0].GetComponent<Animator>().enabled)
                ToggleHousesWTrees();
        }

        if (camFollow.currentCoords == CameraFollow.AnandaCoords.HousesE)
        {
            if (BatterySETrees[0].GetComponent<Animator>().enabled)
                ToggleBatterySETrees();
        }

        if (camFollow.currentCoords == CameraFollow.AnandaCoords.HousesN)
        {
            if (PlaygroundNTrees[0].GetComponent<Animator>().enabled)
                TogglePlaygroundNTrees();

            if (RiverTrees[0].GetComponent<Animator>().enabled)
                ToggleRiverTrees();
        }

        if (camFollow.currentCoords == CameraFollow.AnandaCoords.HousesS)
        {
            if (PlaygroundSTrees[0].GetComponent<Animator>().enabled)
                TogglePlaygroundSTrees();
        }

        if (camFollow.currentCoords == CameraFollow.AnandaCoords.HousesW)
        {
            if (HomeTrees[0].GetComponent<Animator>().enabled)
                ToggleHomeTrees();

            if (WoodsWTrees[0].GetComponent<Animator>().enabled)
                ToggleWoodsWTrees();
        }

        if (camFollow.currentCoords == CameraFollow.AnandaCoords.Lake)
        {
            if (PlaygroundNTrees[0].GetComponent<Animator>().enabled)
                TogglePlaygroundNTrees();

            if (RiverTrees[0].GetComponent<Animator>().enabled)
                ToggleRiverTrees();
        }

        if (camFollow.currentCoords == CameraFollow.AnandaCoords.PlaygroundE)
        {
            if (HousesETrees[0].GetComponent<Animator>().enabled)
                ToggleHousesETrees();
        }

        if (camFollow.currentCoords == CameraFollow.AnandaCoords.PlaygroundN)
        {
            if (HousesNTrees[0].GetComponent<Animator>().enabled)
                ToggleHousesNTrees();

            if (LakeTrees[0].GetComponent<Animator>().enabled)
                ToggleLakeTrees();
        }

        if (camFollow.currentCoords == CameraFollow.AnandaCoords.PlaygroundS)
        {
            if (HousesSTrees[0].GetComponent<Animator>().enabled)
                ToggleHousesSTrees();
        }

        if (camFollow.currentCoords == CameraFollow.AnandaCoords.PlaygroundN)
        {
            if (HomeTrees[0].GetComponent<Animator>().enabled)
                ToggleHomeTrees();

            if (WoodsWTrees[0].GetComponent<Animator>().enabled)
                ToggleWoodsWTrees();
        }

        if (camFollow.currentCoords == CameraFollow.AnandaCoords.River)
        {
            if (HousesNTrees[0].GetComponent<Animator>().enabled)
                ToggleHousesNTrees();

            if (LakeTrees[0].GetComponent<Animator>().enabled)
                ToggleLakeTrees();
        }

        if (camFollow.currentCoords == CameraFollow.AnandaCoords.WoodsW)
        {
            if (HousesWTrees[0].GetComponent<Animator>().enabled)
                ToggleHousesWTrees();

            if (WoodsWSecretTrees[0].GetComponent<Animator>().enabled)
                ToggleWoodsWSecretTrees();
        }

        if (camFollow.currentCoords == CameraFollow.AnandaCoords.WoodsWSecret)
        {
            if (WoodsWTrees[0].GetComponent<Animator>().enabled)
                ToggleWoodsWTrees();
        }
    }

    public IEnumerator DelayAreaCheck()
    {
        yield return new WaitForSeconds(0.5f);
        CheckAreaToAnimate();
    }

    public void CheckAreaToAnimate()
    {
        CancelInvoke();
        DisableAnimators();

        if (camFollow.currentCoords == CameraFollow.AnandaCoords.BatteryNE)
        {
            InvokeRepeating("CycleEastArmFlowers", 3f, 3f);
            InvokeRepeating("CycleEastArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.BatteryNW)
        {
            ToggleBatteryNWTrees();
            InvokeRepeating("CycleEastArmFlowers", 3f, 3f);
            InvokeRepeating("CycleEastArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.BatterySE)
        {
            ToggleBatterySETrees();
            InvokeRepeating("CycleEastArmFlowers", 3f, 3f);
            InvokeRepeating("CycleEastArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.BatterySW)
        {
            InvokeRepeating("CycleEastArmFlowers", 3f, 3f);
            InvokeRepeating("CycleEastArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.BuildersNE)
        {
            InvokeRepeating("CycleNorthArmFlowers", 3f, 3f);
            InvokeRepeating("CycleNorthArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.BuildersNW)
        {
            InvokeRepeating("CycleNorthArmFlowers", 3f, 3f);
            InvokeRepeating("CycleNorthArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.BuildersSE)
        {
            InvokeRepeating("CycleNorthArmFlowers", 3f, 3f);
            InvokeRepeating("CycleNorthArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.BuildersSW)
        {
            InvokeRepeating("CycleNorthArmFlowers", 3f, 3f);
            InvokeRepeating("CycleNorthArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.Campus)
        {
            ToggleCampusTrees();
            InvokeRepeating("CycleCampusFlowers", 3f, 3f);
            InvokeRepeating("CycleCampusGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.CannaFieldNW)
        {
            InvokeRepeating("CycleWestArmFlowers", 3f, 3f);
            InvokeRepeating("CycleWestArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.CannaFieldSE)
        {
            InvokeRepeating("CycleWestArmFlowers", 3f, 3f);
            InvokeRepeating("CycleWestArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.CannaFieldSW)
        {
            InvokeRepeating("CycleWestArmFlowers", 3f, 3f);
            InvokeRepeating("CycleWestArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.CannaHouse)
        {
            ToggleCannaHouseTrees();
            InvokeRepeating("CycleWestArmFlowers", 3f, 3f);
            InvokeRepeating("CycleWestArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.FarmNW)
        {
            InvokeRepeating("CycleSouthArmFlowers", 3f, 3f);
            InvokeRepeating("CycleSouthArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.FarmNC)
        {
            InvokeRepeating("CycleSouthArmFlowers", 3f, 3f);
            InvokeRepeating("CycleSouthArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.FarmNE)
        {
            InvokeRepeating("CycleSouthArmFlowers", 3f, 3f);
            InvokeRepeating("CycleSouthArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.FarmWC)
        {
            InvokeRepeating("CycleSouthArmFlowers", 3f, 3f);
            InvokeRepeating("CycleSouthArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.FarmCC)
        {
            InvokeRepeating("CycleSouthArmFlowers", 3f, 3f);
            InvokeRepeating("CycleSouthArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.FarmEC)
        {
            InvokeRepeating("CycleSouthArmFlowers", 3f, 3f);
            InvokeRepeating("CycleSouthArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.FarmSW)
        {
            InvokeRepeating("CycleSouthArmFlowers", 3f, 3f);
            InvokeRepeating("CycleSouthArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.FarmSC)
        {
            InvokeRepeating("CycleSouthArmFlowers", 3f, 3f);
            InvokeRepeating("CycleSouthArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.FarmSE)
        {
            InvokeRepeating("CycleSouthArmFlowers", 3f, 3f);
            InvokeRepeating("CycleSouthArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.Home)
        {
            ToggleHomeTrees();
            InvokeRepeating("CycleWestArmFlowers", 3f, 3f);
            InvokeRepeating("CycleWestArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.HousesE)
        {
            ToggleHousesETrees();
            InvokeRepeating("CycleEastArmFlowers", 3f, 3f);
            InvokeRepeating("CycleEastArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.HousesN)
        {
            ToggleHousesNTrees();
            InvokeRepeating("CycleNorthArmFlowers", 3f, 3f);
            InvokeRepeating("CycleNorthArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.HousesS)
        {
            ToggleHousesSTrees();
            InvokeRepeating("CycleSouthArmFlowers", 3f, 3f);
            InvokeRepeating("CycleSouthArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.HousesW)
        {
            ToggleHousesWTrees();
            InvokeRepeating("CycleWestArmFlowers", 3f, 3f);
            InvokeRepeating("CycleWestArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.Lake)
        {
            ToggleLakeTrees();
            InvokeRepeating("CycleNorthArmFlowers", 3f, 3f);
            InvokeRepeating("CycleNorthArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.PlaygroundE)
        {
            InvokeRepeating("CycleEastArmFlowers", 3f, 3f);
            InvokeRepeating("CycleEastArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.PlaygroundN)
        {
            TogglePlaygroundNTrees();
            InvokeRepeating("CycleNorthArmFlowers", 3f, 3f);
            InvokeRepeating("CycleNorthArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.PlaygroundS)
        {
            TogglePlaygroundSTrees();
            InvokeRepeating("CycleSouthArmFlowers", 3f, 3f);
            InvokeRepeating("CycleSouthArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.PlaygroundW)
        {
            InvokeRepeating("CycleWestArmFlowers", 3f, 3f);
            InvokeRepeating("CycleWestArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.RaceTrackE)
        {
            InvokeRepeating("CycleEastArmFlowers", 3f, 3f);
            InvokeRepeating("CycleEastArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.River)
        {
            ToggleRiverTrees();
            InvokeRepeating("CycleNorthArmFlowers", 3f, 3f);
            InvokeRepeating("CycleNorthArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.WoodsW)
        {
            ToggleWoodsWTrees();
            InvokeRepeating("CycleWestArmFlowers", 3f, 3f);
            InvokeRepeating("CycleWestArmGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.WoodsWSecret)
        {
            ToggleWoodsWSecretTrees();
            InvokeRepeating("CycleWestArmFlowers", 3f, 3f);
            InvokeRepeating("CycleWestArmGrass", 1f, 1f);
        }
    }
}
