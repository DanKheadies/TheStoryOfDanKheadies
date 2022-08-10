// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 01/12/2020
// Last:  04/26/2021

using UnityEngine;

public class DeviceDetector : MonoBehaviour
{
    public bool bIsIpad;
    public bool bIsMobile;
    
    void Start()
    {
        DetectDevice();
    }

    public void DetectDevice()
    {
        CheckIfMobile();

        if (bIsMobile)
            CheckIfIpad();
    }

    public void CheckIfMobile()
    {
        // Set based off device
        #if !UNITY_EDITOR
            #if UNITY_IOS
                bIsMobile = true;
            #endif

            #if UNITY_ANDROID
                bIsMobile = true;
            #endif
        #endif
    }

    public void CheckIfIpad()
    {
#if UNITY_IOS
        if ((UnityEngine.iOS.Device.generation.ToString()).IndexOf("iPad") > -1)
            bIsIpad = true;
#endif
    }
}
