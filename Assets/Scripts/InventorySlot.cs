// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso, Asbjorn Thirslund (Brackeys), Austin (AwfulMedia / GameGrind)
// Start: 01/18/2018
// Last:  06/07/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Button removeButton;
    private Button stuffBack;
    public Image icon;
    private Inventory inv;
    public Item item;
    private Transform itemMenu;

    public int itemId;

    private string title;
    private string description;
    private string supplemental;


    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        itemMenu = GameObject.Find("ItemMenu").transform;
        stuffBack = GameObject.Find("StuffBack").GetComponent<Button>();
    }

    public void AddItem (Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        //Debug.Log("Removing " + inv.GetSelectedItemById(inv.selectedItemId).itemName);
        Inventory.instance.Remove(inv.GetSelectedItemById(inv.selectedItemId));
        
        stuffBack.transform.localScale = Vector3.one;
        itemMenu.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        itemMenu.gameObject.GetComponent<CanvasGroup>().interactable = false;
        itemMenu.gameObject.GetComponent<CanvasGroup>().alpha = 0;

        // 06/07/2018 DC -- On Remove, need to update to stop Error when running PlayerPrefs check
        //                  Needs to reupdate the TransferItemTotal & ItemTotal
    }

    public void UseItem()
    {
        //Debug.Log("Using " + inv.GetSelectedItemById(inv.selectedItemId).itemName);
        inv.GetSelectedItemById(inv.selectedItemId);
    }

    public void OpenItemMenu (bool bItemOpen)
    {
        if (item != null)
        {
            if (bItemOpen)
            {
                inv.selectedItemId = this.itemId;

                PopulateItemMenu(item);

                stuffBack.transform.localScale = Vector3.zero;
                itemMenu.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
                itemMenu.gameObject.GetComponent<CanvasGroup>().interactable = true;
                itemMenu.gameObject.GetComponent<CanvasGroup>().alpha = 1;
            }
            else
            {
                stuffBack.transform.localScale = Vector3.one;
                itemMenu.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
                itemMenu.gameObject.GetComponent<CanvasGroup>().interactable = false;
                itemMenu.gameObject.GetComponent<CanvasGroup>().alpha = 0;
            }
        }
    }

    public void PopulateItemMenu (Item item)
    {
        title = item.itemName;
        description = item.itemDescription;
        supplemental = "Dankness: " + item.dankness.ToString();
        itemMenu.transform.GetChild(0).GetComponent<Text>().text = title;
        itemMenu.transform.GetChild(1).GetComponent<Text>().text = description;
        itemMenu.transform.GetChild(2).GetComponent<Text>().text = supplemental;
        itemMenu.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = item.icon;
    }
}
