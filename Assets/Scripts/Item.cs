using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int itemID;
    public ItemType itemType;
    public string itemName;
    public string itemExplain;
    public int itemCount;
    public Sprite itemImage;

    public enum ItemType
    {
        Equipment,
        Used,
        Ingredient,
        Etc
    }

    public Item(int _itemID, string _itemName, string _itemExplain, ItemType _itemType, int _itemCount = 1)
    {
        itemID = _itemID;
        itemName = _itemName;
        itemExplain = _itemExplain;
        itemType = _itemType;
        itemCount = _itemCount;
        itemImage = Resources.Load("Item/" + _itemID.ToString(), typeof(Sprite)) as Sprite;
    }
}

