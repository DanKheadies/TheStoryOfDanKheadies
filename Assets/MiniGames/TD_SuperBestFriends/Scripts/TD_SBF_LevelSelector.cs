// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/13/2019
// Last:  09/13/2019

using UnityEngine;
using UnityEngine.UI;

public class TD_SBF_LevelSelector : MonoBehaviour
{
    public Button[] levelButtons;
    public TD_SBF_SceneFader fader;

    void Start()
    {
        int levelReached = PlayerPrefs.GetInt("TD_SBF_LevelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
                levelButtons[i].interactable = false;
        }
    }

    public void SelectLevel(string levelName)
    {
        fader.FadeTo(levelName);
    }
}
