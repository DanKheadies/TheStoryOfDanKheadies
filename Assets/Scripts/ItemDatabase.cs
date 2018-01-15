// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: Austin (AwfulMedia / GameGrind)
// Contributors: David W. Corso
// Start: 01/13/2018
// Last:  01/15/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

// Stores items
public class ItemDatabase : MonoBehaviour
{
    private List<Item> database = new List<Item>();
    private JsonData itemData;

    void Awake()
    {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
        ConstructItemDatabase();
    }

    public Item FetchItemByID(int id)
    {
        for (int i = 0; i < database.Count; i++)
        {
            if (database[i].ID == id)
            {
                return database[i];
            }
        }
        return null;
    }

    void ConstructItemDatabase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            database.Add(new Item(
                (int)itemData[i]["id"], 
                itemData[i]["title"].ToString(), 
                (int)itemData[i]["value"],
                (int)itemData[i]["stats"]["brio"],
                (int)itemData[i]["stats"]["dankness"],
                (int)itemData[i]["stats"]["efficacy"],
                itemData[i]["description"].ToString(),
                (bool)itemData[i]["bstackable"],
                (int)itemData[i]["rarity"],
                itemData[i]["slug"].ToString()));
        }
    }
}

public class Item
{
    public int ID { get; set; }
    public string Title { get; set; }
    public int Value { get; set; }
    public int Brio { get; set; }
    public int Dankness { get; set; }
    public int Efficacy { get; set; }
    public string Description { get; set; }
    public bool bStackable { get; set; }
    public int Rarity { get; set; }
    public string Slug { get; set; }
    public Sprite Sprite { get; set; }

    public Item(
        int id, 
        string title, 
        int value, 
        int brio, 
        int dankness,
        int efficacy,
        string description,
        bool bstackable,
        int rarity,
        string slug)
    {
        this.ID = id;
        this.Title = title;
        this.Value = value;
        this.Brio = brio;
        this.Dankness = dankness;
        this.Efficacy = efficacy;
        this.Description = description;
        this.bStackable = bstackable;
        this.Rarity = rarity;
        this.Slug = slug;
        this.Sprite = Resources.Load<Sprite>("Items/Cannabis/" + slug);
    }

    public Item()
    {
        this.ID = -1;
    }
}