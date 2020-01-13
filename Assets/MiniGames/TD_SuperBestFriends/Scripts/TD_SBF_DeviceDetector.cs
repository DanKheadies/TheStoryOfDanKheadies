// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 10/21/2019
// Last:  12/12/2019

using UnityEngine;

public class TD_SBF_DeviceDetector : MonoBehaviour
{
    //public GameObject computerControls;
    //public GameObject mobileControls;

    // TODO: put each control set (vibrate included) into a shell
    // Depending on the device, set the size of the ContentGrid object from here
    // And show / hide each device's shell accordingly

    void Start()
    {
        //// Assume computer until proven otherwise
        //computerControls.SetActive(true);
        //mobileControls.SetActive(false);

        ////#if !UNITY_EDITOR
        //    #if UNITY_ANDROID
        //        computerControls.SetActive(false);
        //        mobileControls.SetActive(true);
        //    #endif

        //    #if UNITY_IOS
        //        computerControls.SetActive(false);
        //        mobileControls.SetActive(true);
        //    #endif
        ////#endif
    }
}
