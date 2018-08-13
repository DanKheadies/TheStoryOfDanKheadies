// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjorn Thirslund (Brackeys)
// Contributors: David W. Corso
// Start: 01/18/2018
// Last:  08/12/2018

using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName = "New Item";
    public string itemDescription = "Item Description";
    public Sprite icon = null;
    public bool isDefault = false;
    public int dankness = 0;

    public virtual void Use()
    {
        Debug.Log("Using " + itemName);
    }
}
