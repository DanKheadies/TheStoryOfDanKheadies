// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 01/14/2020
// Last:  06/02/2021

using System.Collections;
using UnityEngine;

public class AreaAnimator : MonoBehaviour
{
    public CameraFollow camFollow;
    public GameObject[] BatteryNEGrass;
    public GameObject[] BatteryNEFlowers;
    public GameObject[] BatteryNWGrass;
    public GameObject[] BatteryNWFlowers;
    public GameObject[] BatteryNWTrees;
    public GameObject[] BatterySEGrass;
    public GameObject[] BatterySEFlowers;
    public GameObject[] BatterySETrees;
    public GameObject[] BatterySWGrass;
    public GameObject[] BatterySWFlowers;
    public GameObject[] BuildersNEGrass;
    public GameObject[] BuildersNEFlowers;
    public GameObject[] BuildersNWGrass;
    public GameObject[] BuildersNWFlowers;
    public GameObject[] BuildersSEGrass;
    public GameObject[] BuildersSEFlowers;
    public GameObject[] BuildersSWGrass;
    public GameObject[] BuildersSWFlowers;
    public GameObject[] CampusGrass;
    public GameObject[] CampusFlowers;
    public GameObject[] CampusTrees;
    public GameObject[] CannaFieldNWGrass;
    public GameObject[] CannaFieldNWFlowers;
    public GameObject[] CannaFieldSEGrass;
    public GameObject[] CannaFieldSEFlowers;
    public GameObject[] CannaFieldSWGrass;
    public GameObject[] CannaFieldSWFlowers;
    public GameObject[] CannaHouseGrass;
    public GameObject[] CannaHouseFlowers;
    public GameObject[] CannaHouseTrees;
    //public GameObject[] FarmNWGrass;
    //public GameObject[] FarmNWFlowers;
    //public GameObject[] FarmNCGrass;
    //public GameObject[] FarmNCFlowers;
    //public GameObject[] FarmNEGrass;
    //public GameObject[] FarmNEFlowers;
    //public GameObject[] FarmWCGrass;
    //public GameObject[] FarmWCFlowers;
    //public GameObject[] FarmCCGrass;
    //public GameObject[] FarmCCFlowers;
    //public GameObject[] FarmECGrass;
    //public GameObject[] FarmECFlowers;
    //public GameObject[] FarmSWGrass;
    //public GameObject[] FarmSWFlowers;
    //public GameObject[] FarmSCGrass;
    //public GameObject[] FarmSCFlowers;
    //public GameObject[] FarmSEGrass;
    //public GameObject[] FarmSEFlowers;
    public GameObject[] HomeGrass;
    public GameObject[] HomeFlowers;
    public GameObject[] HomeTrees;
    public GameObject[] HousesEGrass;
    public GameObject[] HousesEFlowers;
    public GameObject[] HousesETrees;
    public GameObject[] HousesNGrass;
    public GameObject[] HousesNFlowers;
    public GameObject[] HousesNTrees;
    public GameObject[] HousesSGrass;
    public GameObject[] HousesSFlowers;
    public GameObject[] HousesSTrees;
    public GameObject[] HousesWGrass;
    public GameObject[] HousesWFlowers;
    public GameObject[] HousesWTrees;
    public GameObject[] LakeGrass;
    public GameObject[] LakeFlowers;
    public GameObject[] LakeTrees;
    public GameObject[] PlaygroundEGrass;
    public GameObject[] PlaygroundEFlowers;
    public GameObject[] PlaygroundNGrass;
    public GameObject[] PlaygroundNFlowers;
    public GameObject[] PlaygroundNTrees;
    public GameObject[] PlaygroundSGrass;
    public GameObject[] PlaygroundSFlowers;
    public GameObject[] PlaygroundSTrees;
    public GameObject[] PlaygroundWGrass;
    public GameObject[] PlaygroundWFlowers;
    public GameObject[] RaceTrackEGrass;
    public GameObject[] RaceTrackEFlowers;
    public GameObject[] RiverGrass;
    public GameObject[] RiverFlowers;
    public GameObject[] RiverTrees;
    public GameObject[] WoodsWGrass;
    public GameObject[] WoodsWFlowers;
    public GameObject[] WoodsWTrees;
    public GameObject[] WoodsWSecretGrass;
    public GameObject[] WoodsWSecretFlowers;
    public GameObject[] WoodsWSecretTrees;

    public bool bAlterateGrass;
    
    void Start()
    {
        StartCoroutine(DelayAreaCheck());
    }

    //BatteryNE
    public void CycleBatteryNEFlowers()
    {
        BatteryNEFlowers[0].SetActive(false);
        BatteryNEFlowers[1].SetActive(true);

        StartCoroutine(NormalizeBatteryNEFlowers());
    }

    public IEnumerator NormalizeBatteryNEFlowers()
    {
        yield return new WaitForSeconds(1f);

        BatteryNEFlowers[0].SetActive(true);
        BatteryNEFlowers[1].SetActive(false);
    }

    public void CycleBatteryNEGrass()
    {
        if (BatteryNEGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                BatteryNEGrass[1].SetActive(true);
            else
                BatteryNEGrass[2].SetActive(true);

            BatteryNEGrass[0].SetActive(false);
        }
        else if (BatteryNEGrass[1].activeSelf)
        {
            BatteryNEGrass[0].SetActive(true);
            BatteryNEGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (BatteryNEGrass[2].activeSelf)
        {
            BatteryNEGrass[0].SetActive(true);
            BatteryNEGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
    }


    //BatteryNW 
    public void CycleBatteryNWFlowers()
    {
        BatteryNWFlowers[0].SetActive(false);
        BatteryNWFlowers[1].SetActive(true);

        StartCoroutine(NormalizeBatteryNWFlowers());
    }

    public IEnumerator NormalizeBatteryNWFlowers()
    {
        yield return new WaitForSeconds(1f);

        BatteryNWFlowers[0].SetActive(true);
        BatteryNWFlowers[1].SetActive(false);
    }

    public void CycleBatteryNWGrass()
    {
        if (BatteryNWGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                BatteryNWGrass[1].SetActive(true);
            else
                BatteryNWGrass[2].SetActive(true);

            BatteryNWGrass[0].SetActive(false);
        }
        else if (BatteryNWGrass[1].activeSelf)
        {
            BatteryNWGrass[0].SetActive(true);
            BatteryNWGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (BatteryNWGrass[2].activeSelf)
        {
            BatteryNWGrass[0].SetActive(true);
            BatteryNWGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
    }

    public void ToggleBatteryNWTrees()
    {
        for (int i = 0; i < BatteryNWTrees.Length; i++)
            BatteryNWTrees[i].GetComponent<Animator>().enabled = !BatteryNWTrees[i].GetComponent<Animator>().enabled;
    }


    //BatterySE
    public void CycleBatterySEFlowers()
    {
        BatterySEFlowers[0].SetActive(false);
        BatterySEFlowers[1].SetActive(true);

        StartCoroutine(NormalizeBatterySEFlowers());
    }

    public IEnumerator NormalizeBatterySEFlowers()
    {
        yield return new WaitForSeconds(1f);

        BatterySEFlowers[0].SetActive(true);
        BatterySEFlowers[1].SetActive(false);
    }

    public void CycleBatterySEGrass()
    {
        if (BatterySEGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                BatterySEGrass[1].SetActive(true);
            else
                BatterySEGrass[2].SetActive(true);

            BatterySEGrass[0].SetActive(false);
        }
        else if (BatterySEGrass[1].activeSelf)
        {
            BatterySEGrass[0].SetActive(true);
            BatterySEGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (BatterySEGrass[2].activeSelf)
        {
            BatterySEGrass[0].SetActive(true);
            BatterySEGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
    }

    public void ToggleBatterySETrees()
    {
        for (int i = 0; i < BatterySETrees.Length; i++)
            BatterySETrees[i].GetComponent<Animator>().enabled = !BatterySETrees[i].GetComponent<Animator>().enabled;
    }


    //BatterySW 
    public void CycleBatterySWFlowers()
    {
        BatterySWFlowers[0].SetActive(false);
        BatterySWFlowers[1].SetActive(true);

        StartCoroutine(NormalizeBatterySWFlowers());
    }

    public IEnumerator NormalizeBatterySWFlowers()
    {
        yield return new WaitForSeconds(1f);

        BatterySWFlowers[0].SetActive(true);
        BatterySWFlowers[1].SetActive(false);
    }

    public void CycleBatterySWGrass()
    {
        if (BatterySWGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                BatterySWGrass[1].SetActive(true);
            else
                BatterySWGrass[2].SetActive(true);

            BatterySWGrass[0].SetActive(false);
        }
        else if (BatterySWGrass[1].activeSelf)
        {
            BatterySWGrass[0].SetActive(true);
            BatterySWGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (BatterySWGrass[2].activeSelf)
        {
            BatterySWGrass[0].SetActive(true);
            BatterySWGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
    }


    //BuildersNE 
    public void CycleBuildersNEFlowers()
    {
        BuildersNEFlowers[0].SetActive(false);
        BuildersNEFlowers[1].SetActive(true);

        StartCoroutine(NormalizeBuildersNEFlowers());
    }

    public IEnumerator NormalizeBuildersNEFlowers()
    {
        yield return new WaitForSeconds(1f);

        BuildersNEFlowers[0].SetActive(true);
        BuildersNEFlowers[1].SetActive(false);
    }

    public void CycleBuildersNEGrass()
    {
        if (BuildersNEGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                BuildersNEGrass[1].SetActive(true);
            else
                BuildersNEGrass[2].SetActive(true);

            BuildersNEGrass[0].SetActive(false);
        }
        else if (BuildersNEGrass[1].activeSelf)
        {
            BuildersNEGrass[0].SetActive(true);
            BuildersNEGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (BuildersNEGrass[2].activeSelf)
        {
            BuildersNEGrass[0].SetActive(true);
            BuildersNEGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
    }


    //BuildersNW 
    public void CycleBuildersNWFlowers()
    {
        BuildersNWFlowers[0].SetActive(false);
        BuildersNWFlowers[1].SetActive(true);

        StartCoroutine(NormalizeBuildersNWFlowers());
    }

    public IEnumerator NormalizeBuildersNWFlowers()
    {
        yield return new WaitForSeconds(1f);

        BuildersNWFlowers[0].SetActive(true);
        BuildersNWFlowers[1].SetActive(false);
    }

    public void CycleBuildersNWGrass()
    {
        if (BuildersNWGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                BuildersNWGrass[1].SetActive(true);
            else
                BuildersNWGrass[2].SetActive(true);

            BuildersNWGrass[0].SetActive(false);
        }
        else if (BuildersNWGrass[1].activeSelf)
        {
            BuildersNWGrass[0].SetActive(true);
            BuildersNWGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (BuildersNWGrass[2].activeSelf)
        {
            BuildersNWGrass[0].SetActive(true);
            BuildersNWGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
    }


    //BuildersSE 
    public void CycleBuildersSEFlowers()
    {
        BuildersSEFlowers[0].SetActive(false);
        BuildersSEFlowers[1].SetActive(true);

        StartCoroutine(NormalizeBuildersSEFlowers());
    }

    public IEnumerator NormalizeBuildersSEFlowers()
    {
        yield return new WaitForSeconds(1f);

        BuildersSEFlowers[0].SetActive(true);
        BuildersSEFlowers[1].SetActive(false);
    }

    public void CycleBuildersSEGrass()
    {
        if (BuildersSEGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                BuildersSEGrass[1].SetActive(true);
            else
                BuildersSEGrass[2].SetActive(true);

            BuildersSEGrass[0].SetActive(false);
        }
        else if (BuildersSEGrass[1].activeSelf)
        {
            BuildersSEGrass[0].SetActive(true);
            BuildersSEGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (BuildersSEGrass[2].activeSelf)
        {
            BuildersSEGrass[0].SetActive(true);
            BuildersSEGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
    }


    //BuildersSW 
    public void CycleBuildersSWFlowers()
    {
        BuildersSWFlowers[0].SetActive(false);
        BuildersSWFlowers[1].SetActive(true);

        StartCoroutine(NormalizeBuildersSWFlowers());
    }

    public IEnumerator NormalizeBuildersSWFlowers()
    {
        yield return new WaitForSeconds(1f);

        BuildersSWFlowers[0].SetActive(true);
        BuildersSWFlowers[1].SetActive(false);
    }

    public void CycleBuildersSWGrass()
    {
        if (BuildersSWGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                BuildersSWGrass[1].SetActive(true);
            else
                BuildersSWGrass[2].SetActive(true);

            BuildersSWGrass[0].SetActive(false);
        }
        else if (BuildersSWGrass[1].activeSelf)
        {
            BuildersSWGrass[0].SetActive(true);
            BuildersSWGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (BuildersSWGrass[2].activeSelf)
        {
            BuildersSWGrass[0].SetActive(true);
            BuildersSWGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
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


    //CannaFieldNW 
    public void CycleCannaFieldNWFlowers()
    {
        CannaFieldNWFlowers[0].SetActive(false);
        CannaFieldNWFlowers[1].SetActive(true);

        StartCoroutine(NormalizeCannaFieldNWFlowers());
    }

    public IEnumerator NormalizeCannaFieldNWFlowers()
    {
        yield return new WaitForSeconds(1f);

        CannaFieldNWFlowers[0].SetActive(true);
        CannaFieldNWFlowers[1].SetActive(false);
    }

    public void CycleCannaFieldNWGrass()
    {
        if (CannaFieldNWGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                CannaFieldNWGrass[1].SetActive(true);
            else
                CannaFieldNWGrass[2].SetActive(true);

            CannaFieldNWGrass[0].SetActive(false);
        }
        else if (CannaFieldNWGrass[1].activeSelf)
        {
            CannaFieldNWGrass[0].SetActive(true);
            CannaFieldNWGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (CannaFieldNWGrass[2].activeSelf)
        {
            CannaFieldNWGrass[0].SetActive(true);
            CannaFieldNWGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
    }


    //CannaFieldSE 
    public void CycleCannaFieldSEFlowers()
    {
        CannaFieldSEFlowers[0].SetActive(false);
        CannaFieldSEFlowers[1].SetActive(true);

        StartCoroutine(NormalizeCannaFieldSEFlowers());
    }

    public IEnumerator NormalizeCannaFieldSEFlowers()
    {
        yield return new WaitForSeconds(1f);

        CannaFieldSEFlowers[0].SetActive(true);
        CannaFieldSEFlowers[1].SetActive(false);
    }

    public void CycleCannaFieldSEGrass()
    {
        if (CannaFieldSEGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                CannaFieldSEGrass[1].SetActive(true);
            else
                CannaFieldSEGrass[2].SetActive(true);

            CannaFieldSEGrass[0].SetActive(false);
        }
        else if (CannaFieldSEGrass[1].activeSelf)
        {
            CannaFieldSEGrass[0].SetActive(true);
            CannaFieldSEGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (CannaFieldSEGrass[2].activeSelf)
        {
            CannaFieldSEGrass[0].SetActive(true);
            CannaFieldSEGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
    }


    //CannaFieldSW 
    public void CycleCannaFieldSWFlowers()
    {
        CannaFieldSWFlowers[0].SetActive(false);
        CannaFieldSWFlowers[1].SetActive(true);

        StartCoroutine(NormalizeCannaFieldSWFlowers());
    }

    public IEnumerator NormalizeCannaFieldSWFlowers()
    {
        yield return new WaitForSeconds(1f);

        CannaFieldSWFlowers[0].SetActive(true);
        CannaFieldSWFlowers[1].SetActive(false);
    }

    public void CycleCannaFieldSWGrass()
    {
        if (CannaFieldSWGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                CannaFieldSWGrass[1].SetActive(true);
            else
                CannaFieldSWGrass[2].SetActive(true);

            CannaFieldSWGrass[0].SetActive(false);
        }
        else if (CannaFieldSWGrass[1].activeSelf)
        {
            CannaFieldSWGrass[0].SetActive(true);
            CannaFieldSWGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (CannaFieldSWGrass[2].activeSelf)
        {
            CannaFieldSWGrass[0].SetActive(true);
            CannaFieldSWGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
    }


    //CannaHouse
    public void CycleCannaHouseFlowers()
    {
        CannaHouseFlowers[0].SetActive(false);
        CannaHouseFlowers[1].SetActive(true);

        StartCoroutine(NormalizeCannaHouseFlowers());
    }

    public IEnumerator NormalizeCannaHouseFlowers()
    {
        yield return new WaitForSeconds(1f);

        CannaHouseFlowers[0].SetActive(true);
        CannaHouseFlowers[1].SetActive(false);
    }

    public void CycleCannaHouseGrass()
    {
        if (CannaHouseGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                CannaHouseGrass[1].SetActive(true);
            else
                CannaHouseGrass[2].SetActive(true);

            CannaHouseGrass[0].SetActive(false);
        }
        else if (CannaHouseGrass[1].activeSelf)
        {
            CannaHouseGrass[0].SetActive(true);
            CannaHouseGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (CannaHouseGrass[2].activeSelf)
        {
            CannaHouseGrass[0].SetActive(true);
            CannaHouseGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
    }

    public void ToggleCannaHouseTrees()
    {
        for (int i = 0; i < CannaHouseTrees.Length; i++)
            CannaHouseTrees[i].GetComponent<Animator>().enabled = !CannaHouseTrees[i].GetComponent<Animator>().enabled;
    }


    //FarmNW 
    //public void CycleFarmNWFlowers()
    //{
    //    FarmNWFlowers[0].SetActive(false);
    //    FarmNWFlowers[1].SetActive(true);

    //    StartCoroutine(NormalizeFarmNWFlowers());
    //}

    //public IEnumerator NormalizeFarmNWFlowers()
    //{
    //    yield return new WaitForSeconds(1f);

    //    FarmNWFlowers[0].SetActive(true);
    //    FarmNWFlowers[1].SetActive(false);
    //}

    //public void CycleFarmNWGrass()
    //{
    //    if (FarmNWGrass[0].activeSelf)
    //    {
    //        if (!bAlterateGrass)
    //            FarmNWGrass[1].SetActive(true);
    //        else
    //            FarmNWGrass[2].SetActive(true);

    //        FarmNWGrass[0].SetActive(false);
    //    }
    //    else if (FarmNWGrass[1].activeSelf)
    //    {
    //        FarmNWGrass[0].SetActive(true);
    //        FarmNWGrass[1].SetActive(false);

    //        bAlterateGrass = true;
    //    }
    //    else if (FarmNWGrass[2].activeSelf)
    //    {
    //        FarmNWGrass[0].SetActive(true);
    //        FarmNWGrass[2].SetActive(false);

    //        bAlterateGrass = false;
    //    }
    //}


    ////FarmNC 
    //public void CycleFarmNCFlowers()
    //{
    //    FarmNCFlowers[0].SetActive(false);
    //    FarmNCFlowers[1].SetActive(true);

    //    StartCoroutine(NormalizeFarmNCFlowers());
    //}

    //public IEnumerator NormalizeFarmNCFlowers()
    //{
    //    yield return new WaitForSeconds(1f);

    //    FarmNCFlowers[0].SetActive(true);
    //    FarmNCFlowers[1].SetActive(false);
    //}

    //public void CycleFarmNCGrass()
    //{
    //    if (FarmNCGrass[0].activeSelf)
    //    {
    //        if (!bAlterateGrass)
    //            FarmNCGrass[1].SetActive(true);
    //        else
    //            FarmNCGrass[2].SetActive(true);

    //        FarmNCGrass[0].SetActive(false);
    //    }
    //    else if (FarmNCGrass[1].activeSelf)
    //    {
    //        FarmNCGrass[0].SetActive(true);
    //        FarmNCGrass[1].SetActive(false);

    //        bAlterateGrass = true;
    //    }
    //    else if (FarmNCGrass[2].activeSelf)
    //    {
    //        FarmNCGrass[0].SetActive(true);
    //        FarmNCGrass[2].SetActive(false);

    //        bAlterateGrass = false;
    //    }
    //}


    ////FarmNE
    //public void CycleFarmNEFlowers()
    //{
    //    FarmNEFlowers[0].SetActive(false);
    //    FarmNEFlowers[1].SetActive(true);

    //    StartCoroutine(NormalizeFarmNEFlowers());
    //}

    //public IEnumerator NormalizeFarmNEFlowers()
    //{
    //    yield return new WaitForSeconds(1f);

    //    FarmNEFlowers[0].SetActive(true);
    //    FarmNEFlowers[1].SetActive(false);
    //}

    //public void CycleFarmNEGrass()
    //{
    //    if (FarmNEGrass[0].activeSelf)
    //    {
    //        if (!bAlterateGrass)
    //            FarmNEGrass[1].SetActive(true);
    //        else
    //            FarmNEGrass[2].SetActive(true);

    //        FarmNEGrass[0].SetActive(false);
    //    }
    //    else if (FarmNEGrass[1].activeSelf)
    //    {
    //        FarmNEGrass[0].SetActive(true);
    //        FarmNEGrass[1].SetActive(false);

    //        bAlterateGrass = true;
    //    }
    //    else if (FarmNEGrass[2].activeSelf)
    //    {
    //        FarmNEGrass[0].SetActive(true);
    //        FarmNEGrass[2].SetActive(false);

    //        bAlterateGrass = false;
    //    }
    //}


    ////FarmWC 
    //public void CycleFarmWCFlowers()
    //{
    //    FarmWCFlowers[0].SetActive(false);
    //    FarmWCFlowers[1].SetActive(true);

    //    StartCoroutine(NormalizeFarmWCFlowers());
    //}

    //public IEnumerator NormalizeFarmWCFlowers()
    //{
    //    yield return new WaitForSeconds(1f);

    //    FarmWCFlowers[0].SetActive(true);
    //    FarmWCFlowers[1].SetActive(false);
    //}

    //public void CycleFarmWCGrass()
    //{
    //    if (FarmWCGrass[0].activeSelf)
    //    {
    //        if (!bAlterateGrass)
    //            FarmWCGrass[1].SetActive(true);
    //        else
    //            FarmWCGrass[2].SetActive(true);

    //        FarmWCGrass[0].SetActive(false);
    //    }
    //    else if (FarmWCGrass[1].activeSelf)
    //    {
    //        FarmWCGrass[0].SetActive(true);
    //        FarmWCGrass[1].SetActive(false);

    //        bAlterateGrass = true;
    //    }
    //    else if (FarmWCGrass[2].activeSelf)
    //    {
    //        FarmWCGrass[0].SetActive(true);
    //        FarmWCGrass[2].SetActive(false);

    //        bAlterateGrass = false;
    //    }
    //}


    ////FarmCC 
    //public void CycleFarmCCFlowers()
    //{
    //    FarmCCFlowers[0].SetActive(false);
    //    FarmCCFlowers[1].SetActive(true);

    //    StartCoroutine(NormalizeFarmCCFlowers());
    //}

    //public IEnumerator NormalizeFarmCCFlowers()
    //{
    //    yield return new WaitForSeconds(1f);

    //    FarmCCFlowers[0].SetActive(true);
    //    FarmCCFlowers[1].SetActive(false);
    //}

    //public void CycleFarmCCGrass()
    //{
    //    if (FarmCCGrass[0].activeSelf)
    //    {
    //        if (!bAlterateGrass)
    //            FarmCCGrass[1].SetActive(true);
    //        else
    //            FarmCCGrass[2].SetActive(true);

    //        FarmCCGrass[0].SetActive(false);
    //    }
    //    else if (FarmCCGrass[1].activeSelf)
    //    {
    //        FarmCCGrass[0].SetActive(true);
    //        FarmCCGrass[1].SetActive(false);

    //        bAlterateGrass = true;
    //    }
    //    else if (FarmCCGrass[2].activeSelf)
    //    {
    //        FarmCCGrass[0].SetActive(true);
    //        FarmCCGrass[2].SetActive(false);

    //        bAlterateGrass = false;
    //    }
    //}


    ////FarmEC 
    //public void CycleFarmECFlowers()
    //{
    //    FarmECFlowers[0].SetActive(false);
    //    FarmECFlowers[1].SetActive(true);

    //    StartCoroutine(NormalizeFarmECFlowers());
    //}

    //public IEnumerator NormalizeFarmECFlowers()
    //{
    //    yield return new WaitForSeconds(1f);

    //    FarmECFlowers[0].SetActive(true);
    //    FarmECFlowers[1].SetActive(false);
    //}

    //public void CycleFarmECGrass()
    //{
    //    if (FarmECGrass[0].activeSelf)
    //    {
    //        if (!bAlterateGrass)
    //            FarmECGrass[1].SetActive(true);
    //        else
    //            FarmECGrass[2].SetActive(true);

    //        FarmECGrass[0].SetActive(false);
    //    }
    //    else if (FarmECGrass[1].activeSelf)
    //    {
    //        FarmECGrass[0].SetActive(true);
    //        FarmECGrass[1].SetActive(false);

    //        bAlterateGrass = true;
    //    }
    //    else if (FarmECGrass[2].activeSelf)
    //    {
    //        FarmECGrass[0].SetActive(true);
    //        FarmECGrass[2].SetActive(false);

    //        bAlterateGrass = false;
    //    }
    //}


    ////FarmSW 
    //public void CycleFarmSWFlowers()
    //{
    //    FarmSWFlowers[0].SetActive(false);
    //    FarmSWFlowers[1].SetActive(true);

    //    StartCoroutine(NormalizeFarmSWFlowers());
    //}

    //public IEnumerator NormalizeFarmSWFlowers()
    //{
    //    yield return new WaitForSeconds(1f);

    //    FarmSWFlowers[0].SetActive(true);
    //    FarmSWFlowers[1].SetActive(false);
    //}

    //public void CycleFarmSWGrass()
    //{
    //    if (FarmSWGrass[0].activeSelf)
    //    {
    //        if (!bAlterateGrass)
    //            FarmSWGrass[1].SetActive(true);
    //        else
    //            FarmSWGrass[2].SetActive(true);

    //        FarmSWGrass[0].SetActive(false);
    //    }
    //    else if (FarmSWGrass[1].activeSelf)
    //    {
    //        FarmSWGrass[0].SetActive(true);
    //        FarmSWGrass[1].SetActive(false);

    //        bAlterateGrass = true;
    //    }
    //    else if (FarmSWGrass[2].activeSelf)
    //    {
    //        FarmSWGrass[0].SetActive(true);
    //        FarmSWGrass[2].SetActive(false);

    //        bAlterateGrass = false;
    //    }
    //}


    ////FarmSC 
    //public void CycleFarmSCFlowers()
    //{
    //    FarmSCFlowers[0].SetActive(false);
    //    FarmSCFlowers[1].SetActive(true);

    //    StartCoroutine(NormalizeFarmSCFlowers());
    //}

    //public IEnumerator NormalizeFarmSCFlowers()
    //{
    //    yield return new WaitForSeconds(1f);

    //    FarmSCFlowers[0].SetActive(true);
    //    FarmSCFlowers[1].SetActive(false);
    //}

    //public void CycleFarmSCGrass()
    //{
    //    if (FarmSCGrass[0].activeSelf)
    //    {
    //        if (!bAlterateGrass)
    //            FarmSCGrass[1].SetActive(true);
    //        else
    //            FarmSCGrass[2].SetActive(true);

    //        FarmSCGrass[0].SetActive(false);
    //    }
    //    else if (FarmSCGrass[1].activeSelf)
    //    {
    //        FarmSCGrass[0].SetActive(true);
    //        FarmSCGrass[1].SetActive(false);

    //        bAlterateGrass = true;
    //    }
    //    else if (FarmSCGrass[2].activeSelf)
    //    {
    //        FarmSCGrass[0].SetActive(true);
    //        FarmSCGrass[2].SetActive(false);

    //        bAlterateGrass = false;
    //    }
    //}


    ////FarmSE 
    //public void CycleFarmSEFlowers()
    //{
    //    FarmSEFlowers[0].SetActive(false);
    //    FarmSEFlowers[1].SetActive(true);

    //    StartCoroutine(NormalizeFarmSEFlowers());
    //}

    //public IEnumerator NormalizeFarmSEFlowers()
    //{
    //    yield return new WaitForSeconds(1f);

    //    FarmSEFlowers[0].SetActive(true);
    //    FarmSEFlowers[1].SetActive(false);
    //}

    //public void CycleFarmSEGrass()
    //{
    //    if (FarmSEGrass[0].activeSelf)
    //    {
    //        if (!bAlterateGrass)
    //            FarmSEGrass[1].SetActive(true);
    //        else
    //            FarmSEGrass[2].SetActive(true);

    //        FarmSEGrass[0].SetActive(false);
    //    }
    //    else if (FarmSEGrass[1].activeSelf)
    //    {
    //        FarmSEGrass[0].SetActive(true);
    //        FarmSEGrass[1].SetActive(false);

    //        bAlterateGrass = true;
    //    }
    //    else if (FarmSEGrass[2].activeSelf)
    //    {
    //        FarmSEGrass[0].SetActive(true);
    //        FarmSEGrass[2].SetActive(false);

    //        bAlterateGrass = false;
    //    }
    //}


    //Home 
    public void CycleHomeFlowers()
    {
        HomeFlowers[0].SetActive(false);
        HomeFlowers[1].SetActive(true);

        StartCoroutine(NormalizeHomeFlowers());
    }

    public IEnumerator NormalizeHomeFlowers()
    {
        yield return new WaitForSeconds(1f);

        HomeFlowers[0].SetActive(true);
        HomeFlowers[1].SetActive(false);
    }

    public void CycleHomeGrass()
    {
        if (HomeGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                HomeGrass[1].SetActive(true);
            else
                HomeGrass[2].SetActive(true);

            HomeGrass[0].SetActive(false);
        }
        else if (HomeGrass[1].activeSelf)
        {
            HomeGrass[0].SetActive(true);
            HomeGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (HomeGrass[2].activeSelf)
        {
            HomeGrass[0].SetActive(true);
            HomeGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
    }

    public void ToggleHomeTrees()
    {
        for (int i = 0; i < HomeTrees.Length; i++)
            HomeTrees[i].GetComponent<Animator>().enabled = !HomeTrees[i].GetComponent<Animator>().enabled;
    }


    //HousesE 
    public void CycleHousesEFlowers()
    {
        HousesEFlowers[0].SetActive(false);
        HousesEFlowers[1].SetActive(true);

        StartCoroutine(NormalizeHousesEFlowers());
    }

    public IEnumerator NormalizeHousesEFlowers()
    {
        yield return new WaitForSeconds(1f);

        HousesEFlowers[0].SetActive(true);
        HousesEFlowers[1].SetActive(false);
    }

    public void CycleHousesEGrass()
    {
        if (HousesEGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                HousesEGrass[1].SetActive(true);
            else
                HousesEGrass[2].SetActive(true);

            HousesEGrass[0].SetActive(false);
        }
        else if (HousesEGrass[1].activeSelf)
        {
            HousesEGrass[0].SetActive(true);
            HousesEGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (HousesEGrass[2].activeSelf)
        {
            HousesEGrass[0].SetActive(true);
            HousesEGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
    }

    public void ToggleHousesETrees()
    {
        for (int i = 0; i < HousesETrees.Length; i++)
            HousesETrees[i].GetComponent<Animator>().enabled = !HousesETrees[i].GetComponent<Animator>().enabled;
    }


    //HousesN 
    public void CycleHousesNFlowers()
    {
        HousesNFlowers[0].SetActive(false);
        HousesNFlowers[1].SetActive(true);

        StartCoroutine(NormalizeHousesNFlowers());
    }

    public IEnumerator NormalizeHousesNFlowers()
    {
        yield return new WaitForSeconds(1f);

        HousesNFlowers[0].SetActive(true);
        HousesNFlowers[1].SetActive(false);
    }

    public void CycleHousesNGrass()
    {
        if (HousesNGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                HousesNGrass[1].SetActive(true);
            else
                HousesNGrass[2].SetActive(true);

            HousesNGrass[0].SetActive(false);
        }
        else if (HousesNGrass[1].activeSelf)
        {
            HousesNGrass[0].SetActive(true);
            HousesNGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (HousesNGrass[2].activeSelf)
        {
            HousesNGrass[0].SetActive(true);
            HousesNGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
    }

    public void ToggleHousesNTrees()
    {
        for (int i = 0; i < HousesNTrees.Length; i++)
            HousesNTrees[i].GetComponent<Animator>().enabled = !HousesNTrees[i].GetComponent<Animator>().enabled;
    }


    //HousesS 
    public void CycleHousesSFlowers()
    {
        HousesSFlowers[0].SetActive(false);
        HousesSFlowers[1].SetActive(true);

        StartCoroutine(NormalizeHousesSFlowers());
    }

    public IEnumerator NormalizeHousesSFlowers()
    {
        yield return new WaitForSeconds(1f);

        HousesSFlowers[0].SetActive(true);
        HousesSFlowers[1].SetActive(false);
    }

    public void CycleHousesSGrass()
    {
        if (HousesSGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                HousesSGrass[1].SetActive(true);
            else
                HousesSGrass[2].SetActive(true);

            HousesSGrass[0].SetActive(false);
        }
        else if (HousesSGrass[1].activeSelf)
        {
            HousesSGrass[0].SetActive(true);
            HousesSGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (HousesSGrass[2].activeSelf)
        {
            HousesSGrass[0].SetActive(true);
            HousesSGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
    }

    public void ToggleHousesSTrees()
    {
        for (int i = 0; i < HousesSTrees.Length; i++)
            HousesSTrees[i].GetComponent<Animator>().enabled = !HousesSTrees[i].GetComponent<Animator>().enabled;
    }


    //HousesW 
    public void CycleHousesWFlowers()
    {
        HousesWFlowers[0].SetActive(false);
        HousesWFlowers[1].SetActive(true);

        StartCoroutine(NormalizeHousesWFlowers());
    }

    public IEnumerator NormalizeHousesWFlowers()
    {
        yield return new WaitForSeconds(1f);

        HousesWFlowers[0].SetActive(true);
        HousesWFlowers[1].SetActive(false);
    }

    public void CycleHousesWGrass()
    {
        if (HousesWGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                HousesWGrass[1].SetActive(true);
            else
                HousesWGrass[2].SetActive(true);

            HousesWGrass[0].SetActive(false);
        }
        else if (HousesWGrass[1].activeSelf)
        {
            HousesWGrass[0].SetActive(true);
            HousesWGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (HousesWGrass[2].activeSelf)
        {
            HousesWGrass[0].SetActive(true);
            HousesWGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
    }

    public void ToggleHousesWTrees()
    {
        for (int i = 0; i < HousesWTrees.Length; i++)
            HousesWTrees[i].GetComponent<Animator>().enabled = !HousesWTrees[i].GetComponent<Animator>().enabled;
    }


    //Lake 
    public void CycleLakeFlowers()
    {
        LakeFlowers[0].SetActive(false);
        LakeFlowers[1].SetActive(true);

        StartCoroutine(NormalizeLakeFlowers());
    }

    public IEnumerator NormalizeLakeFlowers()
    {
        yield return new WaitForSeconds(1f);

        LakeFlowers[0].SetActive(true);
        LakeFlowers[1].SetActive(false);
    }

    public void CycleLakeGrass()
    {
        if (LakeGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                LakeGrass[1].SetActive(true);
            else
                LakeGrass[2].SetActive(true);

            LakeGrass[0].SetActive(false);
        }
        else if (LakeGrass[1].activeSelf)
        {
            LakeGrass[0].SetActive(true);
            LakeGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (LakeGrass[2].activeSelf)
        {
            LakeGrass[0].SetActive(true);
            LakeGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
    }

    public void ToggleLakeTrees()
    {
        for (int i = 0; i < LakeTrees.Length; i++)
            LakeTrees[i].GetComponent<Animator>().enabled = !LakeTrees[i].GetComponent<Animator>().enabled;
    }


    //PlaygroundE 
    public void CyclePlaygroundEFlowers()
    {
        PlaygroundEFlowers[0].SetActive(false);
        PlaygroundEFlowers[1].SetActive(true);

        StartCoroutine(NormalizePlaygroundEFlowers());
    }

    public IEnumerator NormalizePlaygroundEFlowers()
    {
        yield return new WaitForSeconds(1f);

        PlaygroundEFlowers[0].SetActive(true);
        PlaygroundEFlowers[1].SetActive(false);
    }

    public void CyclePlaygroundEGrass()
    {
        if (PlaygroundEGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                PlaygroundEGrass[1].SetActive(true);
            else
                PlaygroundEGrass[2].SetActive(true);

            PlaygroundEGrass[0].SetActive(false);
        }
        else if (PlaygroundEGrass[1].activeSelf)
        {
            PlaygroundEGrass[0].SetActive(true);
            PlaygroundEGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (PlaygroundEGrass[2].activeSelf)
        {
            PlaygroundEGrass[0].SetActive(true);
            PlaygroundEGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
    }


    //PlaygroundN 
    public void CyclePlaygroundNFlowers()
    {
        PlaygroundNFlowers[0].SetActive(false);
        PlaygroundNFlowers[1].SetActive(true);

        StartCoroutine(NormalizePlaygroundNFlowers());
    }

    public IEnumerator NormalizePlaygroundNFlowers()
    {
        yield return new WaitForSeconds(1f);

        PlaygroundNFlowers[0].SetActive(true);
        PlaygroundNFlowers[1].SetActive(false);
    }

    public void CyclePlaygroundNGrass()
    {
        if (PlaygroundNGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                PlaygroundNGrass[1].SetActive(true);
            else
                PlaygroundNGrass[2].SetActive(true);

            PlaygroundNGrass[0].SetActive(false);
        }
        else if (PlaygroundNGrass[1].activeSelf)
        {
            PlaygroundNGrass[0].SetActive(true);
            PlaygroundNGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (PlaygroundNGrass[2].activeSelf)
        {
            PlaygroundNGrass[0].SetActive(true);
            PlaygroundNGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
    }

    public void TogglePlaygroundNTrees()
    {
        for (int i = 0; i < PlaygroundNTrees.Length; i++)
            PlaygroundNTrees[i].GetComponent<Animator>().enabled = !PlaygroundNTrees[i].GetComponent<Animator>().enabled;
    }


    //PlaygroundS 
    public void CyclePlaygroundSFlowers()
    {
        PlaygroundSFlowers[0].SetActive(false);
        PlaygroundSFlowers[1].SetActive(true);

        StartCoroutine(NormalizePlaygroundSFlowers());
    }

    public IEnumerator NormalizePlaygroundSFlowers()
    {
        yield return new WaitForSeconds(1f);

        PlaygroundSFlowers[0].SetActive(true);
        PlaygroundSFlowers[1].SetActive(false);
    }

    public void CyclePlaygroundSGrass()
    {
        if (PlaygroundSGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                PlaygroundSGrass[1].SetActive(true);
            else
                PlaygroundSGrass[2].SetActive(true);

            PlaygroundSGrass[0].SetActive(false);
        }
        else if (PlaygroundSGrass[1].activeSelf)
        {
            PlaygroundSGrass[0].SetActive(true);
            PlaygroundSGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (PlaygroundSGrass[2].activeSelf)
        {
            PlaygroundSGrass[0].SetActive(true);
            PlaygroundSGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
    }

    public void TogglePlaygroundSTrees()
    {
        for (int i = 0; i < PlaygroundSTrees.Length; i++)
            PlaygroundSTrees[i].GetComponent<Animator>().enabled = !PlaygroundSTrees[i].GetComponent<Animator>().enabled;
    }


    //PlaygroundW 
    public void CyclePlaygroundWFlowers()
    {
        PlaygroundWFlowers[0].SetActive(false);
        PlaygroundWFlowers[1].SetActive(true);

        StartCoroutine(NormalizePlaygroundWFlowers());
    }

    public IEnumerator NormalizePlaygroundWFlowers()
    {
        yield return new WaitForSeconds(1f);

        PlaygroundWFlowers[0].SetActive(true);
        PlaygroundWFlowers[1].SetActive(false);
    }

    public void CyclePlaygroundWGrass()
    {
        if (PlaygroundWGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                PlaygroundWGrass[1].SetActive(true);
            else
                PlaygroundWGrass[2].SetActive(true);

            PlaygroundWGrass[0].SetActive(false);
        }
        else if (PlaygroundWGrass[1].activeSelf)
        {
            PlaygroundWGrass[0].SetActive(true);
            PlaygroundWGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (PlaygroundWGrass[2].activeSelf)
        {
            PlaygroundWGrass[0].SetActive(true);
            PlaygroundWGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
    }


    //RaceTrackE 
    public void CycleRaceTrackEFlowers()
    {
        RaceTrackEFlowers[0].SetActive(false);
        RaceTrackEFlowers[1].SetActive(true);

        StartCoroutine(NormalizeRaceTrackEFlowers());
    }

    public IEnumerator NormalizeRaceTrackEFlowers()
    {
        yield return new WaitForSeconds(1f);

        RaceTrackEFlowers[0].SetActive(true);
        RaceTrackEFlowers[1].SetActive(false);
    }

    public void CycleRaceTrackEGrass()
    {
        if (RaceTrackEGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                RaceTrackEGrass[1].SetActive(true);
            else
                RaceTrackEGrass[2].SetActive(true);

            RaceTrackEGrass[0].SetActive(false);
        }
        else if (RaceTrackEGrass[1].activeSelf)
        {
            RaceTrackEGrass[0].SetActive(true);
            RaceTrackEGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (RaceTrackEGrass[2].activeSelf)
        {
            RaceTrackEGrass[0].SetActive(true);
            RaceTrackEGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
    }


    //River 
    public void CycleRiverFlowers()
    {
        RiverFlowers[0].SetActive(false);
        RiverFlowers[1].SetActive(true);

        StartCoroutine(NormalizeRiverFlowers());
    }

    public IEnumerator NormalizeRiverFlowers()
    {
        yield return new WaitForSeconds(1f);

        RiverFlowers[0].SetActive(true);
        RiverFlowers[1].SetActive(false);
    }

    public void CycleRiverGrass()
    {
        if (RiverGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                RiverGrass[1].SetActive(true);
            else
                RiverGrass[2].SetActive(true);

            RiverGrass[0].SetActive(false);
        }
        else if (RiverGrass[1].activeSelf)
        {
            RiverGrass[0].SetActive(true);
            RiverGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (RiverGrass[2].activeSelf)
        {
            RiverGrass[0].SetActive(true);
            RiverGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
    }

    public void ToggleRiverTrees()
    {
        for (int i = 0; i < RiverTrees.Length; i++)
            RiverTrees[i].GetComponent<Animator>().enabled = !RiverTrees[i].GetComponent<Animator>().enabled;
    }


    //WoodsW 
    public void CycleWoodsWFlowers()
    {
        WoodsWFlowers[0].SetActive(false);
        WoodsWFlowers[1].SetActive(true);

        StartCoroutine(NormalizeWoodsWFlowers());
    }

    public IEnumerator NormalizeWoodsWFlowers()
    {
        yield return new WaitForSeconds(1f);

        WoodsWFlowers[0].SetActive(true);
        WoodsWFlowers[1].SetActive(false);
    }

    public void CycleWoodsWGrass()
    {
        if (WoodsWGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                WoodsWGrass[1].SetActive(true);
            else
                WoodsWGrass[2].SetActive(true);

            WoodsWGrass[0].SetActive(false);
        }
        else if (WoodsWGrass[1].activeSelf)
        {
            WoodsWGrass[0].SetActive(true);
            WoodsWGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (WoodsWGrass[2].activeSelf)
        {
            WoodsWGrass[0].SetActive(true);
            WoodsWGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
    }

    public void ToggleWoodsWTrees()
    {
        for (int i = 0; i < WoodsWTrees.Length; i++)
            WoodsWTrees[i].GetComponent<Animator>().enabled = !WoodsWTrees[i].GetComponent<Animator>().enabled;
    }


    //WoodsWSecret 
    public void CycleWoodsWSecretFlowers()
    {
        WoodsWSecretFlowers[0].SetActive(false);
        WoodsWSecretFlowers[1].SetActive(true);

        StartCoroutine(NormalizeWoodsWSecretFlowers());
    }

    public IEnumerator NormalizeWoodsWSecretFlowers()
    {
        yield return new WaitForSeconds(1f);

        WoodsWSecretFlowers[0].SetActive(true);
        WoodsWSecretFlowers[1].SetActive(false);
    }

    public void CycleWoodsWSecretGrass()
    {
        if (WoodsWSecretGrass[0].activeSelf)
        {
            if (!bAlterateGrass)
                WoodsWSecretGrass[1].SetActive(true);
            else
                WoodsWSecretGrass[2].SetActive(true);

            WoodsWSecretGrass[0].SetActive(false);
        }
        else if (WoodsWSecretGrass[1].activeSelf)
        {
            WoodsWSecretGrass[0].SetActive(true);
            WoodsWSecretGrass[1].SetActive(false);

            bAlterateGrass = true;
        }
        else if (WoodsWSecretGrass[2].activeSelf)
        {
            WoodsWSecretGrass[0].SetActive(true);
            WoodsWSecretGrass[2].SetActive(false);

            bAlterateGrass = false;
        }
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

        //if (camFollow.currentCoords == CameraFollow.AnandaCoords.FarmNW)
        //{
        //    if (CampusTrees[0].GetComponent<Animator>().enabled)
        //        ToggleCampusTrees();
        //}

        //if (camFollow.currentCoords == CameraFollow.AnandaCoords.FarmSW)
        //{
        //    if (HousesSTrees[0].GetComponent<Animator>().enabled)
        //        ToggleHousesSTrees();
        //}

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
            InvokeRepeating("CycleBatteryNEFlowers", 3f, 3f);
            InvokeRepeating("CycleBatteryNEGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.BatteryNW)
        {
            //Invoke("AnimateBatteryNWTrees", 0.5f);
            ToggleBatteryNWTrees();
            InvokeRepeating("CycleBatteryNWFlowers", 3f, 3f);
            InvokeRepeating("CycleBatteryNWGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.BatterySE)
        {
            //Invoke("AnimateBatterySETrees", 0.5f);
            ToggleBatterySETrees();
            InvokeRepeating("CycleBatterySEFlowers", 3f, 3f);
            InvokeRepeating("CycleBatterySEGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.BatterySW)
        {
            InvokeRepeating("CycleBatterySWFlowers", 3f, 3f);
            InvokeRepeating("CycleBatterySWGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.BuildersNE)
        {
            InvokeRepeating("CycleBuildersNEFlowers", 3f, 3f);
            InvokeRepeating("CycleBuildersNEGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.BuildersNW)
        {
            InvokeRepeating("CycleBuildersNWFlowers", 3f, 3f);
            InvokeRepeating("CycleBuildersNWGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.BuildersSE)
        {
            InvokeRepeating("CycleBuildersSEFlowers", 3f, 3f);
            InvokeRepeating("CycleBuildersSEGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.BuildersSW)
        {
            InvokeRepeating("CycleBuildersSWFlowers", 3f, 3f);
            InvokeRepeating("CycleBuildersSWGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.Campus)
        {
            //Invoke("AnimateCampusTrees", 0.5f);
            ToggleCampusTrees();
            InvokeRepeating("CycleCampusFlowers", 3f, 3f);
            InvokeRepeating("CycleCampusGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.CannaFieldNW)
        {
            InvokeRepeating("CycleCannaFieldNWFlowers", 3f, 3f);
            InvokeRepeating("CycleCannaFieldNWGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.CannaFieldSE)
        {
            InvokeRepeating("CycleCannaFieldSEFlowers", 3f, 3f);
            InvokeRepeating("CycleCannaFieldSEGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.CannaFieldSW)
        {
            InvokeRepeating("CycleCannaFieldSWFlowers", 3f, 3f);
            InvokeRepeating("CycleCannaFieldSWGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.CannaHouse)
        {
            //Invoke("AnimateCannaHouseTrees", 0.5f);
            ToggleCannaHouseTrees();
            InvokeRepeating("CycleCannaHouseFlowers", 3f, 3f);
            InvokeRepeating("CycleCannaHouseGrass", 1f, 1f);
        }
        //else if (camFollow.currentCoords == CameraFollow.AnandaCoords.FarmNW)
        //{
        //    InvokeRepeating("CycleFarmNWFlowers", 3f, 3f);
        //    InvokeRepeating("CycleFarmNWGrass", 1f, 1f);
        //}
        //else if (camFollow.currentCoords == CameraFollow.AnandaCoords.FarmNC)
        //{
        //    InvokeRepeating("CycleFarmNCFlowers", 3f, 3f);
        //    InvokeRepeating("CycleFarmNCGrass", 1f, 1f);
        //}
        //else if (camFollow.currentCoords == CameraFollow.AnandaCoords.FarmNE)
        //{
        //    InvokeRepeating("CycleFarmNEFlowers", 3f, 3f);
        //    InvokeRepeating("CycleFarmNEGrass", 1f, 1f);
        //}
        //else if (camFollow.currentCoords == CameraFollow.AnandaCoords.FarmWC)
        //{
        //    InvokeRepeating("CycleFarmWCFlowers", 3f, 3f);
        //    InvokeRepeating("CycleFarmWCGrass", 1f, 1f);
        //}
        //else if (camFollow.currentCoords == CameraFollow.AnandaCoords.FarmCC)
        //{
        //    InvokeRepeating("CycleFarmCCFlowers", 3f, 3f);
        //    InvokeRepeating("CycleFarmCCGrass", 1f, 1f);
        //}
        //else if (camFollow.currentCoords == CameraFollow.AnandaCoords.FarmEC)
        //{
        //    InvokeRepeating("CycleFarmECFlowers", 3f, 3f);
        //    InvokeRepeating("CycleFarmECGrass", 1f, 1f);
        //}
        //else if (camFollow.currentCoords == CameraFollow.AnandaCoords.FarmSW)
        //{
        //    InvokeRepeating("CycleFarmSWFlowers", 3f, 3f);
        //    InvokeRepeating("CycleFarmSWGrass", 1f, 1f);
        //}
        //else if (camFollow.currentCoords == CameraFollow.AnandaCoords.FarmSC)
        //{
        //    InvokeRepeating("CycleFarmSCFlowers", 3f, 3f);
        //    InvokeRepeating("CycleFarmSCGrass", 1f, 1f);
        //}
        //else if (camFollow.currentCoords == CameraFollow.AnandaCoords.FarmSE)
        //{
        //    InvokeRepeating("CycleFarmSEFlowers", 3f, 3f);
        //    InvokeRepeating("CycleFarmSEGrass", 1f, 1f);
        //}
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.Home)
        {
            //Invoke("AnimateHomeTrees", 0.5f);
            ToggleHomeTrees();
            InvokeRepeating("CycleHomeFlowers", 3f, 3f);
            InvokeRepeating("CycleHomeGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.HousesE)
        {
            //Invoke("AnimateHousesETrees", 0.5f);
            ToggleHousesETrees();
            InvokeRepeating("CycleHousesEFlowers", 3f, 3f);
            InvokeRepeating("CycleHousesEGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.HousesN)
        {
            //Invoke("AnimateHousesNTrees", 0.5f);
            ToggleHousesNTrees();
            InvokeRepeating("CycleHousesNFlowers", 3f, 3f);
            InvokeRepeating("CycleHousesNGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.HousesS)
        {
            //Invoke("AnimateHousesSTrees", 0.5f);
            ToggleHousesSTrees();
            InvokeRepeating("CycleHousesSFlowers", 3f, 3f);
            InvokeRepeating("CycleHousesSGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.HousesW)
        {
            //Invoke("AnimateHousesWTrees", 0.5f);
            ToggleHousesWTrees();
            InvokeRepeating("CycleHousesWFlowers", 3f, 3f);
            InvokeRepeating("CycleHousesWGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.Lake)
        {
            //Invoke("AnimateLakeTrees", 0.5f);
            ToggleLakeTrees();
            InvokeRepeating("CycleLakeFlowers", 3f, 3f);
            InvokeRepeating("CycleLakeGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.PlaygroundE)
        {
            InvokeRepeating("CyclePlaygroundEFlowers", 3f, 3f);
            InvokeRepeating("CyclePlaygroundEGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.PlaygroundN)
        {
            //Invoke("AnimatePlaygroundNTrees", 0.5f);
            TogglePlaygroundNTrees();
            InvokeRepeating("CyclePlaygroundNFlowers", 3f, 3f);
            InvokeRepeating("CyclePlaygroundNGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.PlaygroundS)
        {
            //Invoke("AnimatePlaygroundSTrees", 0.5f);
            TogglePlaygroundSTrees();
            InvokeRepeating("CyclePlaygroundSFlowers", 3f, 3f);
            InvokeRepeating("CyclePlaygroundSGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.PlaygroundW)
        {
            InvokeRepeating("CyclePlaygroundWFlowers", 3f, 3f);
            InvokeRepeating("CyclePlaygroundWGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.RaceTrackE)
        {
            InvokeRepeating("CycleRaceTrackEFlowers", 3f, 3f);
            InvokeRepeating("CycleRaceTrackEGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.River)
        {
            //Invoke("AnimateRiverTrees", 0.5f);
            ToggleRiverTrees();
            InvokeRepeating("CycleRiverFlowers", 3f, 3f);
            InvokeRepeating("CycleRiverGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.WoodsW)
        {
            //Invoke("AnimateWoodsWTrees", 0.5f);
            ToggleWoodsWTrees();
            InvokeRepeating("CycleWoodsWFlowers", 3f, 3f);
            InvokeRepeating("CycleWoodsWGrass", 1f, 1f);
        }
        else if (camFollow.currentCoords == CameraFollow.AnandaCoords.WoodsWSecret)
        {
            //Invoke("AnimateWoodsWSecretTrees", 0.5f);
            ToggleWoodsWSecretTrees();
            InvokeRepeating("CycleWoodsWSecretFlowers", 3f, 3f);
            InvokeRepeating("CycleWoodsWSecretGrass", 1f, 1f);
        }
    }
}
