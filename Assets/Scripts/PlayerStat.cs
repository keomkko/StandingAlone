using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    public static PlayerStat instance;
    private Animator anim;

    public float hp = 150f;
    public float hunger = 100f;
    public float thirst = 100f;
    public int player_atk;

    private IEnumerator coroutine;
    public bool isLive = true;

    public Slider hpSlider;
    public Slider hungerSlider;
    public Slider thirstSlider;

    public GameObject boat;

    void Start()
    {
        boat.SetActive(false);
        anim = GetComponent<Animator>();
        instance = this;
        coroutine = StatCoroutine();
        StartCoroutine(coroutine);
        player_atk = 10;

        hpSlider.maxValue = 150f;
        hungerSlider.maxValue = 100f;
        thirstSlider.maxValue = 100f;
    }

    IEnumerator StatCoroutine()
    {
        while (hp > 0f)
        {
            if (hp >= 150f)
            {
                hp = 150f;
            }
            
            if (PlayerAction.instance.canDash == true)
            {
                hunger -= 0.08f;
            }
            else
            {
                hunger -= 0.05f;
            }

            if (hunger >= 50f)
            {
                if (hunger >= 100f)
                {
                    hunger = 100f;
                }
                hp += 0.05f;
                
            }
            else if (hunger <= 0f)
            {
                hunger = 0f;
                hp -= 0.2f;
            }

            if (PlayerAction.instance.canDash == true)
            {
                thirst -= 0.05f;
            }
            else
            {
                thirst -= 0.04f;
            }

            if (thirst >= 50f)
            {
                if (thirst >= 100f)
                {
                    thirst = 100f;
                }
                hp += 0.05f;
            }
            else if (thirst <= 0f)
            {
                thirst = 0f;
                hp -= 0.1f;
            }
            
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Hit(int enemyAttack)
    {
        hp -= enemyAttack;
    }

    void Update()
    {
        hpSlider.value = hp;
        hungerSlider.value = hunger;
        thirstSlider.value = thirst;

        if (hp <= 0f)
        {
            Debug.Log("사망");
            StopCoroutine(coroutine);
            PlayerAction.instance.speed = 0f;
            Death();
        }
        else if (DN.instance.dayCount >= 8)
        {
            StopCoroutine(coroutine);
            anim.SetTrigger("Death");
            PlayerAction.instance.speed = 0f;
        }

        if(Inventory.instance.IsClear)
        {
            StopCoroutine(coroutine);
            PlayerAction.instance.speed = 0f;
            gameObject.transform.position = new Vector3(0, -39, 0);
            boat.SetActive(true);
        }
    }

    private void Death()
    {
        if(isLive)
        {
            isLive = false;
            anim.SetTrigger("Death");
        }
        
    }
}
