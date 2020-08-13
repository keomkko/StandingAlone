using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    private void Awake()
    {
        instance = this;
    }
    public List<Item> itemList = new List<Item>();


    public void UseItem(int _itemID)
    {
        switch (_itemID)
        {
            case 10001:
                PlayerStat.instance.hunger += 65;
                break;
            case 10002:
                PlayerStat.instance.hunger += 50;
                break;
            case 10003:
                PlayerStat.instance.hunger += 40;
                break;
            case 10004:
                PlayerStat.instance.hunger += 35;
                break;
            case 10005:
                PlayerStat.instance.hunger += 25;
                break;
            case 10006:
                PlayerStat.instance.hunger += 10;
                PlayerStat.instance.thirst += 30;
                break;
            case 10007:
                PlayerStat.instance.hunger += 15;
                PlayerStat.instance.thirst += 5;
                break;
        }
    }
    
void Start()
    {
        itemList.Add(new Item(10001, "돼지고기", "허기를 65 채워 준다.", Item.ItemType.Used, 0));
        itemList.Add(new Item(10002, "사슴고기", "허기를 50 채워 준다.", Item.ItemType.Used, 0));
        itemList.Add(new Item(10003, "갈매기고기", "허기를 40 채워 준다.", Item.ItemType.Used, 0));
        itemList.Add(new Item(10004, "닭고기", "허기를 35 채워 준다.", Item.ItemType.Used, 0));
        itemList.Add(new Item(10005, "생선", "허기를 25 채워 준다.", Item.ItemType.Used, 0));
        itemList.Add(new Item(10006, "코코넛", "허기를 10, 갈증을 30 채워준다.", Item.ItemType.Used, 0));
        itemList.Add(new Item(10007, "채소", "허기를 15, 갈증을 5 채워준다.", Item.ItemType.Used, 0));
        itemList.Add(new Item(20001, "목재", "제작 아이템으로 쓰인다.", Item.ItemType.Ingredient, 0));
        itemList.Add(new Item(20003, "실", "제작 아이템으로 쓰인다.", Item.ItemType.Ingredient, 0));
        itemList.Add(new Item(20004, "돌", "제작 아이템으로 쓰인다.", Item.ItemType.Ingredient, 1));
        itemList.Add(new Item(30001, "돌", "공격력 35, 공격속도 0.8", Item.ItemType.Equipment, 1));
        itemList.Add(new Item(30002, "곡괭이", "공격력 60, 공격속도 1", Item.ItemType.Equipment, 0));
        itemList.Add(new Item(30003, "도끼", "공격력 75, 공격속도 1.2", Item.ItemType.Equipment, 0));
        itemList.Add(new Item(30004, "낚싯대", "공격력 10, 공격속도 0.6", Item.ItemType.Equipment, 0));
        itemList.Add(new Item(30005, "배", "제작 시 게임 클리어", Item.ItemType.Etc, 0));
    }
}
