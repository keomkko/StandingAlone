using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    ItemDatabase itemdatabase;
    PlayerStat playerstat;
    public GameObject go_Crafting;
    public bool CraftingActivated = false;
    private int Stone;
    private int Thread;
    private int Wood;
    private int[] select = new int[5]; // 0 : 돌, 1 : 곡괭이, 2 : 도끼, 3 : 낚싯대, 4 : 배
    private int selectNum = 0;

    void Start()
    {
        itemdatabase = FindObjectOfType<ItemDatabase>();
        playerstat = FindObjectOfType<PlayerStat>();
    }

    // Update is called once per frame
    void Update()
    {
        itemInfo();
        TryOpenCrafting();
        Craft();
        itemReturn();
    }

    private void TryOpenCrafting()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(!CraftingActivated)
            {
                PlayerAction.instance.speed = 0;
                CraftingActivated = true;
                go_Crafting.SetActive(true);
            }
            else if(CraftingActivated)
            {
                PlayerAction.instance.speed = 125f;
                CraftingActivated = false;
                go_Crafting.SetActive(false);
            }
        }
    }
    private void itemInfo()
    {
        for (int i = 0; i < ItemDatabase.instance.itemList.Count; i++)
        {
            switch (itemdatabase.itemList[i].itemID)
            {
                case 20001:
                    Wood = itemdatabase.itemList[i].itemCount;
                    break;
                case 20003:
                    Thread = itemdatabase.itemList[i].itemCount;
                    break;
                case 20004:
                    Stone = itemdatabase.itemList[i].itemCount;
                    break;
            }
        }
    }
    private void Craft()
    {
        if (CraftingActivated)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                selectNum--;
                if (selectNum <= 0)
                {
                    selectNum = 0;
                }
                Debug.Log(selectNum);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                selectNum++;
                if (selectNum >= 4)
                {
                    selectNum = 4;
                }
                Debug.Log(selectNum);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                switch (selectNum)
                {
                    case 0:
                        if (Stone >= 1)
                        {
                            select[selectNum] = 1;
                        }
                        break;
                    case 1:
                        if (Stone >= 10 && Wood >= 10)
                        {
                            select[selectNum] = 1;
                            Stone -= 10;
                            Wood -= 10;
                        }
                        break;
                    case 2:
                        if (Stone >= 8 && Thread >= 2 && Wood >= 10)
                        {
                            select[selectNum] = 1;
                            Stone -= 8;
                            Thread -= 2;
                            Wood -= 10;
                        }
                        break;
                    case 3:
                        if (Stone >= 2 && Thread >= 4 && Wood >= 6)
                        {
                            select[selectNum] = 1;
                            Stone -= 2;
                            Thread -= 4;
                            Wood -= 6;
                        }
                        break;
                    case 4:
                        if (select[0] >= 1 && select[1] >= 1 && select[2] >= 1 && select[3] >= 1)
                        {
                            if (Stone >= 14 && Thread >= 4 && Wood >= 20)
                            {
                                select[selectNum] = 1;
                                Stone -= 14;
                                Thread -= 4;
                                Wood -= 20;
                            }
                        }
                        break;
                }
            }
        }
    }
    private void itemReturn()
    {
        for (int i = 0; i < ItemDatabase.instance.itemList.Count; i++)
        {
            switch (itemdatabase.itemList[i].itemID)
            {
                case 20001:
                    itemdatabase.itemList[i].itemCount = Wood;
                    break;
                case 20003:
                    itemdatabase.itemList[i].itemCount = Thread;
                    break;
                case 20004:
                    itemdatabase.itemList[i].itemCount = Stone;
                    break;
                case 30001:
                    itemdatabase.itemList[i].itemCount = select[0];
                    break;
                case 30002:
                    itemdatabase.itemList[i].itemCount = select[1];
                    break;
                case 30003:
                    itemdatabase.itemList[i].itemCount = select[2];
                    break;
                case 30004:
                    itemdatabase.itemList[i].itemCount = select[3];
                    break;
                case 30005:
                    itemdatabase.itemList[i].itemCount = select[4];
                    break;
            }
        }
    }
}
