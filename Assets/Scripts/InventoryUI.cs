// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjorn Thirslund (Brackeys)
// Contributors: David W. Corso
// Start: 01/18/2018
// Last:  01/19/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;
    GameObject pauseScreen;

    Inventory inventory;

    InventorySlot[] slots;

	void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        pauseScreen = GameObject.Find("PauseScreen");

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
	}

    void Update()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            // Short-cut (b & i) to show hide inventory
            // Need to do more w/ Pause Screen logic
            //pauseScreen.SetActive(!pauseScreen.activeSelf);
            //inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    void UpdateUI()
    {
        Debug.Log("Updating UI");
        
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items2.Count)
            {
                slots[i].AddItem(inventory.items2[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
