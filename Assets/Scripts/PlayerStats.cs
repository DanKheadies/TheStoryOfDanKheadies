// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/16/2017
// Last:  08/18/2019

using UnityEngine;

// Manage the Player's Levels & Stats
public class PlayerStats : MonoBehaviour
{
    public PlayerBrioManager playerBrio;
    public UIManager uMan;

    public int currentBrio;
    public int currentCog;
    public int currentExp;
    public int currentLevel;
    public int currentPhys;

    public int[] brioLevels;
    public int[] cogLevels;
    public int[] toLevelUp;
    public int[] physLevels;

	void Start ()
    {
        currentBrio = brioLevels[0];
        currentCog = cogLevels[0];
        currentPhys = physLevels[0];
	}

    public void AddExperience(int experienceToAdd)
    {
        currentExp += experienceToAdd; 
    }

    public void LevelUp()
    {
        currentLevel++;

        currentBrio = brioLevels[currentLevel];
        currentCog = cogLevels[currentLevel];
        currentPhys = physLevels[currentLevel];

        // Update Brio Bar & add extra
        playerBrio.playerMaxBrio = currentBrio;
        playerBrio.playerCurrentBrio += currentBrio - brioLevels[currentLevel - 1];
        uMan.bUpdateBrio = true;
    }
}
