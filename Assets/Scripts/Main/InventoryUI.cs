// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjorn Thirslund (Brackeys)
// Contributors: David W. Corso
// Start: 01/18/2018
// Last:  08/18/2019

using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryUI;
    public Inventory inventory;
    public InventorySlot[] slots;
    public Transform itemsParent;
    public Transform itemMenu;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        itemMenu.gameObject.GetComponent<CanvasGroup>().alpha = 0;

        UpdateUI();
    }

    //void Update()
    //{
    //    if (Input.GetButton("Inventory"))
    //    {
    //        // Short-cut (b & i) to show hide inventory
    //        // Need to do more w/ Pause Screen logic
    //        //pauseScreen.SetActive(!pauseScreen.activeSelf);
    //        //inventoryUI.SetActive(!inventoryUI.activeSelf);
    //    }
    //}

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }

            slots[i].itemId = i;
        }
    }
}
