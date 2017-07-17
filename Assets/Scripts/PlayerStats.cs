// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/16/2017
// Last:  07/16/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manage the Player's Levels & Stats
public class PlayerStats : MonoBehaviour
{
    public PlayerBrioManager thePlayerBrio;

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
        // Initializers
        thePlayerBrio = FindObjectOfType<PlayerBrioManager>();

        currentBrio = brioLevels[0];
        currentCog = cogLevels[0];
        currentPhys = physLevels[0];
	}
	
	void Update ()
    {
		if (currentExp >= toLevelUp[currentLevel])
        {
            LevelUp();
        }
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
        thePlayerBrio.playerMaxBrio = currentBrio;
        thePlayerBrio.playerCurrentBrio += currentBrio - brioLevels[currentLevel - 1];
    }
}
