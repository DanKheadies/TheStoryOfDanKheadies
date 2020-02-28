// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjorn Thirslund (Brackeys)
// Contributors: David W. Corso
// Start: 01/18/2018
// Last:  08/23/2019

using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public Item selectedItem;
    public List<Item> items;
    public ScriptManager scriptMan;

    public delegate void onItemChanged();
    public onItemChanged onItemChangedCallback;

    public int selectedItemId;
    public int totalItems;

    public bool bUpdateItemCount;

    void Awake()
    {
        if (instance)
        {
            Debug.LogWarning("More than one instance of inventory found. Error.");
            return;
        }

        instance = this;

        // Initializers
        items = new List<Item>();
        totalItems = 20;
    }

    public void Add(Item item)
    {
        if (item.bInteractable)
        {
            if (items.Count >= totalItems)
            {
                Debug.Log("Not enough room.");
                return;
            }
            else
            {
                items.Add(item);
                scriptMan.InventoryUpdate();
            }

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
    }

    //public void PickUpDestory(Item item)
    //{
    //    bool wasPickedUp = instance.Add(item);

    //    // Remove item if needed
    //    if (wasPickedUp)
    //        Destroy(gameObject);
        
    //    scriptMan.InventoryUpdate();
    //}

    public void Remove(Item item)
    {
        items.Remove(item);

        // TODO -- Drops an instance of item on the ground

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
        
        scriptMan.InventoryUpdate();
    }

    public Item GetSelectedItemById (int itemId)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (itemId == i)
                selectedItem = items[i];
        }
        
        return selectedItem;
    }

    // To be a Save Game function?
    public void LoadInventory(string type)
    {
        if (type == "transfer")
        {
            for (int i = 0; i < PlayerPrefs.GetInt("TransferItemTotal"); i++)
            {
                string savedItem = PlayerPrefs.GetString("TransferItem" + i);
                savedItem = savedItem.Substring(0, savedItem.Length - 7);
                Item tempItem = (Item)Resources.Load("Items/" + savedItem);
                Add(tempItem);
            }
        }
        else if (type == "saved")
        {
            for (int i = 0; i < PlayerPrefs.GetInt("ItemTotal"); i++)
            {
                string savedItem = PlayerPrefs.GetString("Item" + i);
                savedItem = savedItem.Substring(0, savedItem.Length - 7);
                Item tempItem = (Item)Resources.Load("Items/" + savedItem);
                Add(tempItem);
            }
        }
    }
}
