// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: Austin (AwfulMedia / GameGrind)
// Contributors: David W. Corso
// Start: 01/14/2018
// Last:  01/15/2018

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemShell : MonoBehaviour/*, IDropHandler*/
{
    public int id;
    private Inventory inv;


    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    //public void OnDrop(PointerEventData eventData)
    //{
    //    ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
    //    if (inv.items[id].ID == -1)
    //    {
    //        inv.items[droppedItem.itemShell] = new Item();
    //        inv.items[id] = droppedItem.item;
    //        droppedItem.itemShell = id;
    //    }
    //    else if (droppedItem.itemShell != id)
    //    {
    //        Transform item = this.transform.GetChild(0);
    //        item.GetComponent<ItemData>().itemShell = droppedItem.itemShell;
    //        item.transform.SetParent(inv.itemShells[droppedItem.itemShell].transform);
    //        item.transform.position = inv.itemShells[droppedItem.itemShell].transform.position;

    //        droppedItem.itemShell = id;
    //        droppedItem.transform.SetParent(this.transform);
    //        droppedItem.transform.position = this.transform.position;

    //        inv.items[droppedItem.itemShell] = item.GetComponent<ItemData>().item;
    //        inv.items[id] = droppedItem.item;
    //    }
    //}
}
