﻿// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/23/2017
// Last:  04/26/2021

using UnityEngine;

// Manage music
public class MusicManager : MonoBehaviour
{
    public AudioSource[] musicTracks;

    public bool bMusicCanPlay;
    public static bool bMusicExists;

    public int currentTrack;
    
	void Start ()
    {
        if (!bMusicExists)
        {
            bMusicExists = true;
        }
    }

	void Update ()
    {
        if (bMusicCanPlay)
        {
            if (!musicTracks[currentTrack].isPlaying)
            {
                musicTracks[currentTrack].Play();
            }
        }
        else if (!bMusicExists)
        {
            musicTracks[currentTrack].Stop();
        }
	}

    public void SwitchTrack(int newTrack)
    {
        musicTracks[currentTrack].Stop();
        currentTrack = newTrack;
        musicTracks[currentTrack].Play();
    }
}
