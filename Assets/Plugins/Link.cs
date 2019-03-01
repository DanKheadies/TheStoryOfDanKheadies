// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: valyard (https://github.com/valyard/UnityWebGLOpenLink)
// Contributors: David W. Corso
// Start: --/--/----
// Last:  01/15/2019

using UnityEngine;
using System.Runtime.InteropServices;

public class Link : MonoBehaviour 
{
    public string ColluminacLink;

    private void Start()
    {
        ColluminacLink = "http://guesswhocolluded.com/colluminac.html";
    }

    public void OpenLink()
	{
		Application.OpenURL(ColluminacLink);
	}

	public void OpenLinkJS()
	{
        // DC 02/22/2019 -- Deprecated; needed? Using OpenLink()
		//Application.ExternalEval("window.open('"+ ColluminacLink + "');");
    }

    #if !UNITY_IOS
    // Opens links for non-Unity Editor Apps (i.e. standalone, webgl, etc.)
    public void OpenLinkJSPlugin()
    {
        // DC 01/13/2019 -- Causes an error in Unity if this is active; avoid error by doing nothing here
        // Will still open the link via alt code (above?)
        if (!Application.isEditor)
        {
            openWindow(ColluminacLink);
        }
    }

    [DllImport("__Internal")]
	private static extern void openWindow(string url);
    #endif
}