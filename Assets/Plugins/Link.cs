// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: --/--/----
// Last:  06/29/2019

using UnityEngine;

public class Link : MonoBehaviour 
{
    public string ColluminacLink;

    private void Start()
    {
        ColluminacLink = "http://guesswhocolluded.com/colluminac.html";
    }

    public void OpenLink()
	{
		#if UNITY_WEBGL 
            Application.ExternalEval("window.open(\"http://guesswhocolluded.com/colluminac.html\",\"_blank\")");
        #endif

        #if !UNITY_WEBGL 
            Application.OpenURL(ColluminacLink);
        #endif
	}
}