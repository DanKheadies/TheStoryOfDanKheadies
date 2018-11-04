// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: valyard (https://github.com/valyard/UnityWebGLOpenLink)
// Contributors: David W. Corso
// Start: --/--/----
// Last:  11/03/2018

using UnityEngine;
using System.Runtime.InteropServices;

public class Link : MonoBehaviour 
{
    public string ColluminacLink;

    private void Start()
    {
        ColluminacLink = "https://docs.google.com/document/d/1Q8-YiK7TAVkGBsrL_3F9a92JjTFYVCyLcg-RQNNKYkM/edit?usp=sharing";
    }

    public void OpenLink()
	{
		Application.OpenURL(ColluminacLink);
	}

	public void OpenLinkJS()
	{
		Application.ExternalEval("window.open('"+ ColluminacLink + "');");
	}

	public void OpenLinkJSPlugin()
	{
        openWindow(ColluminacLink);
	}

    [DllImport("__Internal")]
	private static extern void openWindow(string url);

}