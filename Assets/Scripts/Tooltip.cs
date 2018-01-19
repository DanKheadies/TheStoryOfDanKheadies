// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: Austin (AwfulMedia / GameGrind)
// Contributors: David W. Corso
// Start: 01/15/2018
// Last:  01/19/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private Button stuffBack;
    private GameObject tooltip;
    private Item item;
    private string title;
    private string description;
    private Text stuffText;

    void Start()
    {
        stuffBack = GameObject.Find("StuffBack").GetComponent<Button>();
        stuffText = GameObject.Find("StuffText").GetComponent<Text>();
        tooltip = GameObject.Find("Tooltip");
        tooltip.SetActive(false);
    }

    void Update()
    {
        if (tooltip.activeSelf)
        {
            // tooltip.transform.position = Input.mousePosition;
        }
    }

    public void Activate(Item item)
    {
        this.item = item;
        ConstructDataString();
        HideMenuText();
        tooltip.SetActive(true);
    }

    public void Deactivate(Item item)
    {
        DisplayMenuText();
        tooltip.SetActive(false);
    }

    public void ConstructDataString()
    {
        title = item.Title;
        description = item.Description;
        tooltip.transform.GetChild(0).GetComponent<Text>().text = title;
        tooltip.transform.GetChild(1).GetComponent<Text>().text = description;
    }

    public void HideMenuText()
    {
        stuffBack.transform.localScale = Vector3.zero;
        stuffText.transform.localScale = Vector3.zero;
    }

    public void DisplayMenuText()
    {
        stuffBack.transform.localScale = Vector3.one;
        stuffText.transform.localScale = Vector3.one;
    }
}
