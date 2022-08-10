// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 10/17/2019
// Last:  04/26/2021

using UnityEngine;
using UnityEngine.UI;

public class TD_SBF_VolumeManager : MonoBehaviour
{
    public Slider musicSlider;
    public Slider volumeSlider;
    public TD_SBF_MusicManager mMan;
    public TD_SBF_SFX_Manager sfxMan;

    public float currentMusicLevel;
    public float currentVolumeLevel;
    public float defaultVolume = 0.333f;
    public float incrementor = 0.1f;
    public float maxVolumeLevel = 1.0f;
    public float minVolumeLevel = 0.0f;

    void Start()
    {
        // Order matters here
        GetAndSetMusicVolume();
        GetAndSetVolume();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Equals))
            RaiseVolume();
        else if (Input.GetKeyUp(KeyCode.Minus))
            LowerVolume();
    }

    public void RaiseVolume()
    {
        if (currentVolumeLevel < maxVolumeLevel)
        {
            currentVolumeLevel = currentVolumeLevel + incrementor;

            LoopThruMusic(currentVolumeLevel);
            LoopThruEffects(currentVolumeLevel);
        }
        else
            currentVolumeLevel = maxVolumeLevel;

        if (volumeSlider)
            volumeSlider.value = currentVolumeLevel;

        PlayerPrefs.SetFloat("TD_SBF_Volume", currentVolumeLevel);
        
        AdjustSliders();
    }

    public void LowerVolume()
    {
        if (currentVolumeLevel > minVolumeLevel)
        {
            currentVolumeLevel = currentVolumeLevel - incrementor;

            LoopThruMusic(currentVolumeLevel);
            LoopThruEffects(currentVolumeLevel);
        }
        else
            currentVolumeLevel = minVolumeLevel;

        if (volumeSlider)
            volumeSlider.value = currentVolumeLevel;

        PlayerPrefs.SetFloat("TD_SBF_Volume", currentVolumeLevel);

        AdjustSliders();
    }

    public void RaiseMusic()
    {
        if (currentMusicLevel < maxVolumeLevel)
        {
            currentMusicLevel = currentMusicLevel + incrementor;

            LoopThruMusic(currentMusicLevel);
        }
        else
            currentMusicLevel = maxVolumeLevel;

        if (musicSlider)
            musicSlider.value = currentMusicLevel;

        PlayerPrefs.SetFloat("TD_SBF_MusicVolume", currentMusicLevel);

        AdjustSliders();
    }

    public void LowerMusic()
    {
        if (currentMusicLevel > minVolumeLevel)
        {
            currentMusicLevel = currentMusicLevel - incrementor;

            LoopThruMusic(currentMusicLevel);
        }
        else
            currentMusicLevel = minVolumeLevel;

        if (musicSlider)
            musicSlider.value = currentMusicLevel;

        PlayerPrefs.SetFloat("TD_SBF_MusicVolume", currentMusicLevel);

        AdjustSliders();
    }

    public void GetAndSetVolume()
    {
        // Sets initial volume based off saved data
        if (!PlayerPrefs.HasKey("TD_SBF_Volume"))
            currentVolumeLevel = defaultVolume;
        else
        {
            currentVolumeLevel = PlayerPrefs.GetFloat("TD_SBF_Volume");

            // Adjusts the slider to the saved volume and voids error
            if (volumeSlider)
                volumeSlider.value = currentVolumeLevel;
        }

        // Sets all volume control objects to the current / saved volume
        LoopThruMusic(currentVolumeLevel);
        LoopThruEffects(currentVolumeLevel);
    }

    public void GetAndSetMusicVolume()
    {
        // Sets initial volume based off saved data
        if (!PlayerPrefs.HasKey("TD_SBF_MusicVolume"))
            currentMusicLevel = maxVolumeLevel;
        else
        {
            currentMusicLevel = PlayerPrefs.GetFloat("TD_SBF_MusicVolume");

            // Adjusts the slider to the saved volume and voids error
            if (musicSlider)
                musicSlider.value = currentMusicLevel;
        }

        // Sets all music objects to the current / saved volume
        LoopThruMusic(currentMusicLevel);
    }

    public void AdjustSliders()
    {
        if (volumeSlider)
            volumeSlider.value = currentVolumeLevel;

        if (musicSlider)
            musicSlider.value = currentMusicLevel;
    }

    public void OnVolumeSliderChange()
    {
        currentVolumeLevel = volumeSlider.value;
        currentMusicLevel = musicSlider.value;

        LoopThruMusic(currentVolumeLevel);
        LoopThruEffects(currentVolumeLevel);
    }

    public void OnMusicSliderChange()
    {
        //currentMusicLevel = musicSlider.value * currentVolumeLevel;
        currentMusicLevel = musicSlider.value;
        // DC TODO

        LoopThruMusic(currentMusicLevel);
    }

    public void LoopThruMusic(float _volumeLevel)
    {
        for (int i = 0; i < mMan.musicTracks.Length; i++)
            mMan.musicTracks[i].GetComponent<TD_SBF_VolumeController>()
                .SetAudioLevel(_volumeLevel * currentMusicLevel);
    }

    public void LoopThruEffects(float _volumeLevel)
    {
        for (int i = 0; i < sfxMan.effects.Length; i++)
            sfxMan.effects[i].GetComponent<TD_SBF_VolumeController>()
                .SetAudioLevel(_volumeLevel);
    }
}
