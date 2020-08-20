using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    private ItemDatabase itemDatabase;
    bool InventoryActivated = false;
    public bool IsClear = false;

    public GameObject go_Inventory;
    public Button[] Slots = new Button[12];

    [SerializeField]
    Text[] SlotText = new Text[12];

    AudioManager audioManager;
    public string ItemSound;

    private void Start()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            SlotText[i] = Slots[i].transform.GetComponentInChildren<Text>();
        }
        audioManager = FindObjectOfType<AudioManager>();
        itemDatabase = FindObjectOfType<ItemDatabase>();
        instance = this;
        go_Inventory.SetActive(false);
    }
    private void Update()
    {
        TryOpenInventory(); //인벤토리 접근
        Slot(); // 아이템 버튼 활성화
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (InventoryActivated == false)
            {
                PlayerAction.instance.speed = 0;
                InventoryActivated = true;
                go_Inventory.SetActive(true);
                print("인벤토리 열림");
            }
            else if (InventoryActivated == true)
            {
                PlayerAction.instance.speed = 125f;
                InventoryActivated = false;
                go_Inventory.SetActive(false);
                print("인벤토리 닫힘");
            }
        }
    }

    public void Slot()
    {
        for (int i = 0; i < ItemDatabase.instance.itemList.Count; i++)
        {
            switch (itemDatabase.itemList[i].itemID)
            {
                case 10001:
                    if (itemDatabase.itemList[i].itemCount >= 1)
                    {
                        Slots[0].interactable = true;
                    }
                    else
                    {
                        Slots[0].interactable = false;
                    }
                    SlotText[0].text = "x" + itemDatabase.itemList[i].itemCount.ToString();
                    break;
                case 10002:
                    if (itemDatabase.itemList[i].itemCount >= 1)
                    {
                        Slots[1].interactable = true;
                    }
                    else
                    {
                        Slots[1].interactable = false;
                    }
                    SlotText[1].text = "x" + itemDatabase.itemList[i].itemCount.ToString();
                    break;
                case 10003:
                    if (itemDatabase.itemList[i].itemCount >= 1)
                    {
                        Slots[2].interactable = true;
                    }
                    else
                    {
                        Slots[2].interactable = false;
                    }
                    SlotText[2].text = "x" + itemDatabase.itemList[i].itemCount.ToString();
                    break;
                case 10004:
                    if (itemDatabase.itemList[i].itemCount >= 1)
                    {
                        Slots[3].interactable = true;
                    }
                    else
                    {
                        Slots[3].interactable = false;
                    }
                    SlotText[3].text = "x" + itemDatabase.itemList[i].itemCount.ToString();
                    break;
                case 10005:
                    if (itemDatabase.itemList[i].itemCount >= 1)
                    {
                        Slots[4].interactable = true;
                    }
                    else
                    {
                        Slots[4].interactable = false;
                    }
                    SlotText[4].text = "x" + itemDatabase.itemList[i].itemCount.ToString();
                    break;
                case 10006:
                    if (itemDatabase.itemList[i].itemCount >= 1)
                    {
                        Slots[5].interactable = true;
                    }
                    else
                    {
                        Slots[5].interactable = false;
                    }
                    SlotText[5].text = "x" + itemDatabase.itemList[i].itemCount.ToString();
                    break;
                case 10007:
                    if (itemDatabase.itemList[i].itemCount >= 1)
                    {
                        Slots[6].interactable = true;
                    }
                    else
                    {
                        Slots[6].interactable = false;
                    }
                    SlotText[6].text = "x" + itemDatabase.itemList[i].itemCount.ToString();
                    break;
                case 10008:
                    if (itemDatabase.itemList[i].itemCount >= 1)
                    {
                        Slots[7].interactable = true;
                    }
                    else
                    {
                        Slots[7].interactable = false;
                    }
                    SlotText[7].text = "x" + itemDatabase.itemList[i].itemCount.ToString();
                    break;
                case 20001:
                    if (itemDatabase.itemList[i].itemCount >= 1)
                    {
                        Slots[8].interactable = true;
                    }
                    else
                    {
                        Slots[8].interactable = false;
                    }
                    SlotText[8].text = "x" + itemDatabase.itemList[i].itemCount.ToString();
                    break;
                case 20003:
                    if (itemDatabase.itemList[i].itemCount >= 1)
                    {
                        Slots[9].interactable = true;
                    }
                    else
                    {
                        Slots[9].interactable = false;
                    }
                    SlotText[9].text = "x" + itemDatabase.itemList[i].itemCount.ToString();
                    break;
                case 20004:
                    if (itemDatabase.itemList[i].itemCount >= 1)
                    {
                        Slots[10].interactable = true;
                    }
                    else
                    {
                        Slots[10].interactable = false;
                    }
                    SlotText[10].text = "x" + itemDatabase.itemList[i].itemCount.ToString();
                    break;
                case 30005:
                    if (itemDatabase.itemList[i].itemCount >= 1)
                    {
                        Slots[11].interactable = true;
                    }
                    else
                    {
                        Slots[11].interactable = false;
                    }
                    SlotText[11].text = "x" + itemDatabase.itemList[i].itemCount.ToString();
                    break;

            }
        }

    }

    public void OnClick(Button slot)
    {
        audioManager.Play(ItemSound);
        for (int i = 0; i < Slots.Length; i++)
        { 
            if(slot.name == Slots[i].name)
            {
                for (int j = 0; j < ItemDatabase.instance.itemList.Count; j++)
                {
                    switch (i)
                    {
                        case 0:
                            if (itemDatabase.itemList[j].itemID == 10001)
                            {
                                ItemDatabase.instance.UseItem(itemDatabase.itemList[j].itemID);
                                itemDatabase.itemList[j].itemCount--;
                            }
                            break;
                        case 1:
                            if (itemDatabase.itemList[j].itemID == 10002)
                            {
                                ItemDatabase.instance.UseItem(itemDatabase.itemList[j].itemID);
                                itemDatabase.itemList[j].itemCount--;
                            }
                            break;
                        case 2:
                            if (itemDatabase.itemList[j].itemID == 10003)
                            {
                                ItemDatabase.instance.UseItem(itemDatabase.itemList[j].itemID);
                                itemDatabase.itemList[j].itemCount--;
                            }
                            break;
                        case 3:
                            if (itemDatabase.itemList[j].itemID == 10004)
                            {
                                ItemDatabase.instance.UseItem(itemDatabase.itemList[j].itemID);
                                itemDatabase.itemList[j].itemCount--;
                            }
                            break;
                        case 4:
                            if (itemDatabase.itemList[j].itemID == 10005)
                            {
                                ItemDatabase.instance.UseItem(itemDatabase.itemList[j].itemID);
                                itemDatabase.itemList[j].itemCount--;
                            }
                            break;
                        case 5:
                            if (itemDatabase.itemList[j].itemID == 10006)
                            {
                                ItemDatabase.instance.UseItem(itemDatabase.itemList[j].itemID);
                                itemDatabase.itemList[j].itemCount--;
                            }
                            break;
                        case 6:
                            if (itemDatabase.itemList[j].itemID == 10007)
                            {
                                ItemDatabase.instance.UseItem(itemDatabase.itemList[j].itemID);
                                itemDatabase.itemList[j].itemCount--;
                            }
                            break;
                        case 7:
                            if (itemDatabase.itemList[j].itemID == 10008)
                            {
                                ItemDatabase.instance.UseItem(itemDatabase.itemList[j].itemID);
                                itemDatabase.itemList[j].itemCount--;
                            }
                            break;
                        case 11:
                            if (itemDatabase.itemList[j].itemID == 30005)
                            {
                                IsClear = true;
                                go_Inventory.SetActive(false);
                            }
                            break;
                        default:
                            
                            break;
                    }
                }
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
