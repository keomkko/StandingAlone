using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    public static PlayerAction instance;
    public float speed = 125f;
    float dash;

    public bool canDash = false;
    private bool attacking = false;

    public LayerMask layerMask;
    private Rigidbody2D rb;
    private Animator anim;

    private float curTime;
    private float coolTime = 1f;
    public Transform pos;
    public Vector2 boxpos;

    public Image selectedEquip;
    public Image Stone;
    public Image Pickaxe;
    public Image Axe;
    public Image fishingRod;

    private bool[] currentWeapon = new bool[4]; // 0 : 돌, 1 : 곡괭이, 2 : 도끼, 3 : 낚싯대

    public string walkSound_1;
    public string walkSound_2;

    private float walkTime = 0.6f;
    private float currentwalk;
    private AudioManager theAudio;

    private void Awake()
    {
        theAudio = FindObjectOfType<AudioManager>();
        instance = this;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        EquipInfo();
        for (int i = 0; i < currentWeapon.Length; i++)
        {
            if (i != 0)
            {
                currentWeapon[i] = false;
            }
            else
            {
                currentWeapon[i] = true;
            }
        }
        selectedEquip.gameObject.SetActive(false);
    }

    private void Update()
    {
        if ((Input.GetAxisRaw("Horizontal") != 0) || (Input.GetAxisRaw("Vertical") != 0))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                canDash = true;
            }

            if(!canDash)
            {
                walkTime = 0.6f;
            }
            else
            {
                walkTime = 0.4f;
            }
            if(currentwalk <= 0)
            {
                currentwalk = walkTime;
                int temp = Random.Range(1, 2);
                switch (temp)
                {
                    case 1:
                        theAudio.Play(walkSound_1);
                        break;
                    case 2:
                        theAudio.Play(walkSound_2);
                        break;
                }
            }
            else
            {
                currentwalk -= Time.deltaTime;
            }
            
        }

        if (attacking == false)
        {
            if (Input.GetAxisRaw("Horizontal") == -1)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (Input.GetAxisRaw("Horizontal") == 1)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }

        SelectEquip(); //장비 선택
        PlayerAttack(); //공격
        DurabilityCount(); //내구도

        //animation
        if ((Input.GetAxisRaw("Horizontal") == 0) && (Input.GetAxisRaw("Vertical") == 0))
        {
            anim.SetBool("run", false);
        }
        else
        {
            anim.SetBool("run", true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxpos);
    }

    private void FixedUpdate()
    {
        if (canDash == true)
        {
            dash = speed * 1.5f;
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * Time.deltaTime, Input.GetAxisRaw("Vertical") * Time.deltaTime) * dash;

            canDash = false;
        }
        else
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * Time.deltaTime, Input.GetAxisRaw("Vertical") * Time.deltaTime) * speed;
        }
    }

    private void EquipInfo()
    {
        for (int i = 0; i < ItemDatabase.instance.itemList.Count; i++)
        {
            switch (ItemDatabase.instance.itemList[i].itemID)
            {
                case 30001:
                    Stone.sprite = ItemDatabase.instance.itemList[i].itemImage;
                    break;
                case 30002:
                    Pickaxe.sprite = ItemDatabase.instance.itemList[i].itemImage;
                    break;
                case 30003:
                    Axe.sprite = ItemDatabase.instance.itemList[i].itemImage;
                    break;
                case 30004:
                    fishingRod.sprite = ItemDatabase.instance.itemList[i].itemImage;
                    break;
            }
        }
    }

    public void PlayerAttack()
    {
        if (curTime <= 0)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                attacking = true;
                rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxpos, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                    if (collider.tag == "Animal")
                    {
                        collider.GetComponent<Animal>().TakeDamage(PlayerStat.instance.player_atk);
                        for (int i = 0; i < currentWeapon.Length; i++)
                        {
                            if (currentWeapon[i])
                            {
                                Crafting.instance.WeaponDurability[i]--;
                            }
                        }
                    }
                    else if (collider.tag == "Plant")
                    {
                        collider.GetComponent<Plant>().TakeDamage(PlayerStat.instance.player_atk);
                        for (int i = 0; i < currentWeapon.Length; i++)
                        {
                            if (currentWeapon[i])
                            {
                                Crafting.instance.WeaponDurability[i]--;
                            }
                        }
                    }
                    else if (collider.tag == "Rocks")
                    {
                        collider.GetComponent<Rocks>().TakeDamage(PlayerStat.instance.player_atk);
                        for (int i = 0; i < currentWeapon.Length; i++)
                        {
                            if (currentWeapon[i])
                            {
                                Crafting.instance.WeaponDurability[i]--;
                            }
                        }
                    }
                }
                anim.SetTrigger("atk");
                curTime = coolTime;
            }
        }
        else
        {
            curTime -= Time.deltaTime;
            if ((curTime <= 0.3f) && (curTime > 0) && (attacking == true))
            {
                rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
                attacking = false;
            }
        }
    }

    private void SelectEquip()
    {
        if (Input.GetKeyDown("1"))
        {
            for (int i = 0; i < ItemDatabase.instance.itemList.Count; i++)
            {
                if (ItemDatabase.instance.itemList[i].itemID == 30001 && ItemDatabase.instance.itemList[i].itemCount >= 1)
                {
                    PlayerStat.instance.player_atk = 35;
                    coolTime = 0.8f;
                    selectedEquip.gameObject.SetActive(true);
                    selectedEquip.sprite = ItemDatabase.instance.itemList[i].itemImage;
                    for (int j = 0; j < currentWeapon.Length; j++)
                    {
                        if (j == 0)
                        {
                            currentWeapon[0] = true;
                        }
                        else
                        {
                            currentWeapon[j] = false;
                        }
                    }
                }
            }
        }
        else if (Input.GetKeyDown("2"))
        {
            for (int i = 0; i < ItemDatabase.instance.itemList.Count; i++)
            {
                if (ItemDatabase.instance.itemList[i].itemID == 30002 && ItemDatabase.instance.itemList[i].itemCount >= 1)
                {
                    PlayerStat.instance.player_atk = 60;
                    coolTime = 1f;
                    selectedEquip.gameObject.SetActive(true);
                    selectedEquip.sprite = ItemDatabase.instance.itemList[i].itemImage;
                    for (int j = 0; j < currentWeapon.Length; j++)
                    {
                        if (j == 1)
                        {
                            currentWeapon[1] = true;
                        }
                        else
                        {
                            currentWeapon[j] = false;
                        }
                    }
                }
            }
        }
        else if (Input.GetKeyDown("3"))
        {
            for (int i = 0; i < ItemDatabase.instance.itemList.Count; i++)
            {
                if (ItemDatabase.instance.itemList[i].itemID == 30003 && ItemDatabase.instance.itemList[i].itemCount >= 1)
                {
                    PlayerStat.instance.player_atk = 75;
                    coolTime = 1.2f;
                    selectedEquip.gameObject.SetActive(true);
                    selectedEquip.sprite = ItemDatabase.instance.itemList[i].itemImage;
                    for (int j = 0; j < currentWeapon.Length; j++)
                    {
                        if (j == 2)
                        {
                            currentWeapon[2] = true;
                        }
                        else
                        {
                            currentWeapon[j] = false;
                        }
                    }
                }
            }
        }
        else if (Input.GetKeyDown("4"))
        {
            for (int i = 0; i < ItemDatabase.instance.itemList.Count; i++)
            {
                if (ItemDatabase.instance.itemList[i].itemID == 30004 && ItemDatabase.instance.itemList[i].itemCount >= 1)
                {
                    PlayerStat.instance.player_atk = 10;
                    coolTime = 0.8f;
                    selectedEquip.gameObject.SetActive(true);
                    selectedEquip.sprite = ItemDatabase.instance.itemList[i].itemImage;
                    for (int j = 0; j < currentWeapon.Length; j++)
                    {
                        if (j == 3)
                        {
                            currentWeapon[3] = true;
                        }
                        else
                        {
                            currentWeapon[j] = false;
                        }
                    }
                }
            }
        }
    }

    private void DurabilityCount()
    {
        for (int i = 0; i < Crafting.instance.WeaponDurability.Length; i++)
        {
            for (int j = 0; j < ItemDatabase.instance.itemList.Count; j++)
            {
                switch (ItemDatabase.instance.itemList[j].itemID)
                {
                    case 30001:
                        if (Crafting.instance.WeaponDurability[0] <= 0 && ItemDatabase.instance.itemList[j].itemCount >= 1)
                        {
                            ItemDatabase.instance.itemList[j].itemCount = 0;
                            selectedEquip.gameObject.SetActive(false);
                        }
                        break;
                    case 30002:
                        if (Crafting.instance.WeaponDurability[1] <= 0 && ItemDatabase.instance.itemList[j].itemCount >= 1)
                        {
                            ItemDatabase.instance.itemList[j].itemCount = 0;
                            selectedEquip.gameObject.SetActive(false);
                        }
                        break;
                    case 30003:
                        if (Crafting.instance.WeaponDurability[2] <= 0 && ItemDatabase.instance.itemList[j].itemCount >= 1)
                        {
                            ItemDatabase.instance.itemList[j].itemCount = 0;
                            selectedEquip.gameObject.SetActive(false);
                        }
                        break;
                    case 30004:
                        if (Crafting.instance.WeaponDurability[3] <= 0 && ItemDatabase.instance.itemList[j].itemCount >= 1)
                        {
                            ItemDatabase.instance.itemList[j].itemCount = 0;
                            selectedEquip.gameObject.SetActive(false);
                        }
                        break;
                }

            }
        }
    }

}
