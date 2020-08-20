using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class Crafting : MonoBehaviour
{
    public static Crafting instance;
    ItemDatabase itemdatabase;
    public GameObject go_Crafting;
    public Button[] Select = new Button[5]; // 0 : 돌, 1 : 곡괭이, 2 : 도끼, 3 : 낚싯대, 4 : 배
    public Button CraftButton;

    public bool CraftOn = false;
    public bool CraftingActivated = false;
    private int Stone;
    private int Thread;
    private int Wood;

    public int selectNum;
    private int[] craftItem = new int[5];
    public int[] WeaponDurability = new int[4];

    public Image[] Ingredient = new Image[3]; // 0 : 돌, 1 : 목재, 2 : 실
    [SerializeField]
    private Text[] CountText = new Text[3];

    public string CraftSound;
    AudioManager audioManager;

    void Start()
    {
        for (int i = 0; i < Ingredient.Length; i++)
        {
            CountText[i] = Ingredient[i].GetComponentInChildren<Text>();
        }
        instance = this;
        audioManager = FindObjectOfType<AudioManager>();
        itemdatabase = FindObjectOfType<ItemDatabase>();
        go_Crafting.SetActive(false);
        CraftButton.gameObject.SetActive(false);
        CraftButton.interactable = false;

        for (int i = 0; i < Ingredient.Length; i++)
        {
            Ingredient[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        TryOpenCrafting();
        CraftBtnOn();
    }

    private void TryOpenCrafting()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!CraftingActivated)
            {
                PlayerAction.instance.speed = 0;
                CraftingActivated = true;
                go_Crafting.SetActive(true);
                selectNum = -1;
                if (!CraftOn)
                {
                    itemInfo();
                }

            }
            else if (CraftingActivated)
            {
                PlayerAction.instance.speed = 125f;
                CraftingActivated = false;
                go_Crafting.SetActive(false);
                CraftOn = false;
                for (int i = 0; i < Ingredient.Length; i++)
                {
                    Ingredient[i].gameObject.SetActive(false);
                }
                CraftButton.gameObject.SetActive(false);
            }
        }
    }
    public void SelectEquip(Button SelectButton)
    {
        CraftButton.gameObject.SetActive(true);
        for (int i = 0; i < Select.Length; i++)
        {
            if (SelectButton.name == Select[i].name)
            {
                selectNum = i;
                IngredientCount(i);
            }
        }
        switch (selectNum)
        {
            case 0:
                for (int k = 0; k < Ingredient.Length; k++)
                {
                    if (k == 0)
                    {
                        Ingredient[k].gameObject.SetActive(true);
                    }
                    else
                    {
                        Ingredient[k].gameObject.SetActive(false);
                    }
                }
                break;
            case 1:
                for (int k = 0; k < Ingredient.Length; k++)
                {
                    if (k != 2)
                    {
                        Ingredient[k].gameObject.SetActive(true);
                    }
                    else
                    {
                        Ingredient[k].gameObject.SetActive(false);
                    }
                }
                break;
            case 2:
                for (int k = 0; k < Ingredient.Length; k++)
                {
                    Ingredient[k].gameObject.SetActive(true);
                }
                break;
            case 3:
                for (int k = 0; k < Ingredient.Length; k++)
                {
                    Ingredient[k].gameObject.SetActive(true);
                }
                break;
            case 4:
                for (int k = 0; k < Ingredient.Length; k++)
                {
                    Ingredient[k].gameObject.SetActive(true);
                }
                break;
        }
    }
    private void CraftBtnOn()
    {
        for (int j = 0; j < ItemDatabase.instance.itemList.Count; j++)
        {
            switch (selectNum)
            {
                case 0:
                    if (itemdatabase.itemList[j].itemID == 30001)
                    {
                        CraftButton.image.sprite = itemdatabase.itemList[j].itemImage;
                        if (itemdatabase.itemList[j].itemCount == 0)
                        {
                            CraftButton.interactable = true;
                        }
                        else
                        {
                            CraftButton.interactable = false;
                        }
                    }
                    break;
                case 1:
                    if (itemdatabase.itemList[j].itemID == 30002)
                    {
                        CraftButton.image.sprite = itemdatabase.itemList[j].itemImage;
                        if (itemdatabase.itemList[j].itemCount == 0)
                        {
                            CraftButton.interactable = true;
                        }
                        else
                        {
                            CraftButton.interactable = false;
                        }
                    }
                    break;
                case 2:
                    if (itemdatabase.itemList[j].itemID == 30003)
                    {
                        CraftButton.image.sprite = itemdatabase.itemList[j].itemImage;
                        if (itemdatabase.itemList[j].itemCount == 0)
                        {
                            CraftButton.interactable = true;
                        }
                        else
                        {
                            CraftButton.interactable = false;
                        }
                    }
                    break;
                case 3:
                    if (itemdatabase.itemList[j].itemID == 30004)
                    {
                        CraftButton.image.sprite = itemdatabase.itemList[j].itemImage;
                        if (itemdatabase.itemList[j].itemCount == 0)
                        {
                            CraftButton.interactable = true;
                        }
                        else
                        {
                            CraftButton.interactable = false;
                        }
                    }
                    break;
                case 4:
                    if (itemdatabase.itemList[j].itemID == 30005)
                    {
                        CraftButton.image.sprite = itemdatabase.itemList[j].itemImage;

                        if (itemdatabase.itemList[j].itemCount == 0)
                        {
                            if (craftItem[0] >= 1 && craftItem[1] >= 1 && craftItem[2] >= 1 && craftItem[3] >= 1)
                            {
                                CraftButton.interactable = true;
                            }
                            else
                            {
                                CraftButton.interactable = false;
                            }
                        }
                        else
                        {
                            CraftButton.interactable = false;
                        }

                    }
                    break;
            }
        }
    }
    public void Craft()
    {
        switch (selectNum)
        {
            case 0:
                if (Stone >= 1)
                {
                    craftItem[selectNum] = 1;
                    Stone--;
                    WeaponDurability[selectNum] = 100;
                    audioManager.Play(CraftSound);
                }
                break;
            case 1:
                if (Stone >= 10 && Wood >= 10)
                {
                    craftItem[selectNum] = 1;
                    Stone -= 10;
                    Wood -= 10;
                    WeaponDurability[selectNum] = 15;
                    audioManager.Play(CraftSound);
                }
                break;
            case 2:
                if (Stone >= 8 && Thread >= 2 && Wood >= 10)
                {
                    craftItem[selectNum] = 1;
                    Stone -= 8;
                    Thread -= 2;
                    Wood -= 10;
                    WeaponDurability[selectNum] = 15;
                    audioManager.Play(CraftSound);
                }
                break;
            case 3:
                if (Stone >= 2 && Thread >= 4 && Wood >= 6)
                {
                    craftItem[selectNum] = 1;
                    Stone -= 2;
                    Thread -= 4;
                    Wood -= 6;
                    WeaponDurability[selectNum] = 20;
                    audioManager.Play(CraftSound);
                }
                break;
            case 4:
                if (Stone >= 14 && Thread >= 4 && Wood >= 20)
                {
                    craftItem[selectNum] = 1;
                    Stone -= 14;
                    Thread -= 4;
                    Wood -= 20;
                    audioManager.Play(CraftSound);
                }
                break;
        }
        itemReturn();
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
                case 30001:
                    craftItem[0] = itemdatabase.itemList[i].itemCount;
                    break;
                case 30002:
                    craftItem[1] = itemdatabase.itemList[i].itemCount;
                    break;
                case 30003:
                    craftItem[2] = itemdatabase.itemList[i].itemCount;
                    break;
                case 30004:
                    craftItem[3] = itemdatabase.itemList[i].itemCount;
                    break;
                case 30005:
                    craftItem[4] = itemdatabase.itemList[i].itemCount;
                    break;
            }
        }
        CraftOn = true;
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
                    itemdatabase.itemList[i].itemCount = craftItem[0];
                    break;
                case 30002:
                    itemdatabase.itemList[i].itemCount = craftItem[1];
                    break;
                case 30003:
                    itemdatabase.itemList[i].itemCount = craftItem[2];
                    break;
                case 30004:
                    itemdatabase.itemList[i].itemCount = craftItem[3];
                    break;
                case 30005:
                    itemdatabase.itemList[i].itemCount = craftItem[4];
                    break;
            }
        }
        CraftOn = false;
    }

    private void IngredientCount(int select)
    {
        for (int i = 0; i < CountText.Length; i++)
        {
            switch (select)
            {
                case 0:
                    CountText[0].text = "x1";
                    break;
                case 1:
                    CountText[0].text = "x10";
                    CountText[1].text = "x10";
                    break;
                case 2:
                    CountText[0].text = "x8";
                    CountText[1].text = "x10";
                    CountText[2].text = "x2";
                    break;
                case 3:
                    CountText[0].text = "x2";
                    CountText[1].text = "x6";
                    CountText[2].text = "x4";
                    break;
                case 4:
                    CountText[0].text = "x14";
                    CountText[1].text = "x20";
                    CountText[2].text = "x4";
                    break;
            }
        }
    }
}