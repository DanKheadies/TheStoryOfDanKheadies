// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: valyard (https://github.com/valyard/UnityWebGLOpenLink)
// Contributors: David W. Corso
// Start: --/--/----
// Last:  10/21/2018

using UnityEngine;
using System.Runtime.InteropServices;

public class Link : MonoBehaviour 
{
    public string ColluminacLink;
    //public string IconsLink;

    private void Start()
    {
        ColluminacLink = "https://docs.google.com/document/d/1Q8-YiK7TAVkGBsrL_3F9a92JjTFYVCyLcg-RQNNKYkM/edit?usp=sharing";
        //IconsLink = "https://docs.google.com/document/d/1Q8-YiK7TAVkGBsrL_3F9a92JjTFYVCyLcg-RQNNKYkM/edit?usp=sharing#bookmark=id.2ukwna434o1k";
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

    //public void Open2ndLinkJSPlugin()
    //{
    //    openWindow(IconsLink);
    //}

    [DllImport("__Internal")]
	private static extern void openWindow(string url);

}