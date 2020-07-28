using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    private ItemDatabase itemDatabase;
    public bool InventoryActivated = false;

    public GameObject go_Inventory;

    private List<Item> inventoryItemList;

    private void Start()
    {
        itemDatabase = FindObjectOfType<ItemDatabase>();
        instance = this;
    }
    private void Update()
    {
        TryOpenInventory();

        if (Input.GetKeyDown(KeyCode.E)) //아이템 사용
        {
            if (InventoryActivated == true)
            {
                for (int i = 0; i < ItemDatabase.instance.itemList.Count; i++)
                {
                    if (itemDatabase.itemList[i].itemType == Item.ItemType.Used)
                    {
                        if (itemDatabase.itemList[i].itemCount > 0)
                        {
                            ItemDatabase.instance.UseItem(itemDatabase.itemList[i].itemID);
                            itemDatabase.itemList[i].itemCount--;
                            print(itemDatabase.itemList[i].itemName + " 사용");
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
        }
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (InventoryActivated == false)
            {
                InventoryActivated = true;
                go_Inventory.SetActive(true);
                print("인벤토리 열림");
            }
            else if (InventoryActivated == true)
            {
                InventoryActivated = false;
                go_Inventory.SetActive(false);
                print("인벤토리 닫힘");
            }
        }
    }

    public void GetAnItem(int _itemID, int _count)
    {
        for (int i = 0; i < ItemDatabase.instance.itemList.Count; i++) //데이터베이스 아이템 검색
        {
            if (_itemID == itemDatabase.itemList[i].itemID)
            {
                print(itemDatabase.itemList[i].itemName + " 획득");
                itemDatabase.itemList[i].itemCount += _count;
            }
        }
        return;
    }
}
