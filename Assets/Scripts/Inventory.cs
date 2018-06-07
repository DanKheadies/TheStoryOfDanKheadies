// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjorn Thirslund (Brackeys)
// Contributors: David W. Corso
// Start: 01/18/2018
// Last:  06/07/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of inventory found. Error.");
            return;
        }

        instance = this;
    }

    #endregion
    
    public Item selectedItem;
    public List<Item> items;

    public delegate void onItemChanged();
    public onItemChanged onItemChangedCallback;

    public int selectedItemId;
    public int totalItems;

    public bool bUpdateItemCount;

    void Start()
    {
        // Initializers
        items = new List<Item>();
        totalItems = 20;

        bUpdateItemCount = false;
    }

    public void RerunStart()
    {
        Start();
    }

    public bool Add(Item item)
    {
        if (!item.isDefault)
        {
            if (items.Count >= totalItems)
            {
                Debug.Log(items.Count);
                Debug.Log("Not enough room.");
                return false;
            }
            
            items.Add(item);

            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
        }

        bUpdateItemCount = true;

        return true;
    }

    public void PickUpDestory(Item item)
    {
        bool wasPickedUp = Inventory.instance.Add(item);

        // Remove item if needed
        if (wasPickedUp)
        {
            Destroy(gameObject);
        }

        bUpdateItemCount = true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        // TODO -- Drops an instance of item on the ground

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }

        bUpdateItemCount = true;
    }

    public Item GetSelectedItemById (int itemId)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (itemId == i)
            {
                selectedItem = items[i];
            }
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
