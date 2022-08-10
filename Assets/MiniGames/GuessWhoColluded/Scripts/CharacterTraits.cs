// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 03/31/2019
// Last:  03/31/2019

using System;

// Contains info on traits and characters
public class CharacterTraits : IComparable<CharacterTraits>
{

    public int charId;
    public int charEyeWear;
    public int charFacialHair;
    public int charGender;

    public string charName;
    public string charCountry;
    public string charDirection;
    public string charEyeColor;
    public string charGameName;
    public string charHairColor;
    public string charHairLength;
    public string charSkinColor;

    public string[] charClothingColor;
    public string[] charIcons;

    public CharacterTraits(
        int _charId,
        string _charName,
        string _charGameName,
        string[] _charClothingColor,
        string _charCountry,
        string _charDirection,
        string _charEyeColor,
        int _charEyeWear,
        int _charFacialHair,
        int _charGender,
        string _charHairColor,
        string _charHairLength,
        string[] _charIcons,
        string _charSkinColor)
    {
        charId = _charId;
        charName = _charName;
        charGameName = _charGameName;
        charClothingColor = _charClothingColor;
        charCountry = _charCountry;
        charDirection = _charDirection;
        charEyeColor = _charEyeColor;
        charEyeWear = _charEyeWear;
        charFacialHair = _charFacialHair;
        charGender = _charGender;
        charHairColor = _charHairColor;
        charHairLength = _charHairLength;
        charIcons = _charIcons;
        charSkinColor = _charSkinColor;
    }

    //This method is required by the IComparable
    //interface. 
    public int CompareTo(CharacterTraits other)
    {
        if (other == null)
        {
            return 1;
        }

        //Return the difference in power.
        return charId - other.charId;
    }
}
