// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 10/17/2019
// Last:  10/17/2019

using UnityEngine;

public class TD_SBF_VolumeController : MonoBehaviour
{
    public AudioSource theAudio;

    public float audioLevel;
    public float defaultAudio;

    public void SetAudioLevel(float volume)
    {
        audioLevel = defaultAudio * volume;
        theAudio.volume = audioLevel;
    }
}
