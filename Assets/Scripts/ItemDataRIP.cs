// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: Austin (AwfulMedia / GameGrind)
// Contributors: David W. Corso

// Start: 01/14/2018
// Last:  01/17/2018

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemData : MonoBehaviour/*,
    IPointerDownHandler,
    IPointerUpHandler,
    IDragHandler, 
    IEndDragHandler, 
    IPointerEnterHandler, 
    IPointerExitHandler*/
{
    public Item item;
    public int amount;
    public int itemShell;

    private Inventory inv;
    private Tooltip tooltip;
    private Vector2 offset;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        tooltip = inv.GetComponent<Tooltip>();
    }

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    if (item != null)
    //    {
    //        // Stores position while dragging (below) to align object w/ cursor
    //        offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);

    //        // Bumps item 1 level up until drag is released (below)
    //        this.transform.position = eventData.position - offset;
    //        this.transform.SetParent(this.transform.parent.parent);
    //        GetComponent<CanvasGroup>().blocksRaycasts = false;
    //    }
    //}

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    if (item != null)
    //    {
    //        GetComponent<CanvasGroup>().blocksRaycasts = true;
    //    }
    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    if (item != null)
    //    {
    //        this.transform.position = eventData.position - offset;
    //    }
    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    this.transform.SetParent(inv.itemShells[itemShell].transform);
    //    this.transform.position = inv.itemShells[itemShell].transform.position;
    //    GetComponent<CanvasGroup>().blocksRaycasts = true;
    //}

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    tooltip.Activate(item);
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    tooltip.Deactivate(item);
    //}
}
