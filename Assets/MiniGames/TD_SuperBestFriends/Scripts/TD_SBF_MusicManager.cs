// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 10/17/2019
// Last:  04/26/2021

using UnityEngine;

public class TD_SBF_MusicManager : MonoBehaviour
{
    public AudioSource[] musicTracks;

    public bool bMusicCanPlay;
    public int currentTrack;

    void Start()
    {
        if (bMusicCanPlay &&
            !musicTracks[currentTrack].isPlaying)
        {
            StartMusic();
        }
    }

    public void StartMusic()
    {
        bMusicCanPlay = true;
        musicTracks[currentTrack].Play();
    }

    public void StopMusic()
    {
        bMusicCanPlay = false;
        musicTracks[currentTrack].Stop();
    }

    public void SwitchTrack(int newTrack)
    {
        musicTracks[currentTrack].Stop();
        currentTrack = newTrack;
        musicTracks[currentTrack].Play();
    }
}
