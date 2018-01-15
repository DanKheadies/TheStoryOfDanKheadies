// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: Austin (AwfulMedia / GameGrind)
// Contributors: David W. Corso
// Start: 01/14/2018
// Last:  01/15/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    GameObject stuff;
    GameObject itemsContainer;
    public GameObject itemShell;
    public GameObject item;

    ItemDatabase itemDatabase;

    int totalItems;

    public List<Item> items = new List<Item>();
    public List<GameObject> itemShells = new List<GameObject>();

    void Awake()
    {
        // Initializers
        itemDatabase = GetComponent<ItemDatabase>();

        totalItems = 20;
        stuff = GameObject.Find("StuffMenu");
        itemsContainer = stuff.transform.FindChild("Items").gameObject;

        for (int i = 0; i < totalItems; i++)
        {
            items.Add(new Item());
            itemShells.Add(Instantiate(itemShell));
            itemShells[i].GetComponent<InventoryItemShell>().id = i;
            itemShells[i].transform.SetParent(itemsContainer.transform);
            itemShells[i].transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void AddItem(int id)
    {
        Item itemToAdd = itemDatabase.FetchItemByID(id);

        if (itemToAdd.bStackable && CheckIfItemInInventory(itemToAdd))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == id)
                {
                    ItemData data = itemShells[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == -1)
                {
                    items[i] = itemToAdd;
                    GameObject itemObj = Instantiate(item);
                    itemObj.transform.SetParent(itemShells[i].transform);
                    itemObj.GetComponent<ItemData>().item = itemToAdd;
                    itemObj.GetComponent<ItemData>().amount = 1;
                    itemObj.GetComponent<ItemData>().itemShell = i;
                    itemObj.transform.localPosition = Vector2.zero;
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                    itemObj.name = itemToAdd.Title;
                    itemObj.transform.localScale = new Vector3(1, 1, 1);
                    break;
                }
            }
        }
    }

    bool CheckIfItemInInventory(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == item.ID)
            {
                return true;
            }
        }

        return false;
    }
}
