// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: --/--/----
// Last:  04/26/2021

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