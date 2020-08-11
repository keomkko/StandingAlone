using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public static PlayerStat instance;

    public float hp = 150f;
    public float hunger = 100f;
    public float thirst = 100f;
    public int player_atk = 40;

    private IEnumerator coroutine;

    void Start()
    {
        coroutine = StatCoroutine();
        instance = this;


        StartCoroutine(coroutine);
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
        if (hp <= 0f)
        {
            Debug.Log("사망");
            StopCoroutine(coroutine);
            hp = 150;
        }
    }
}
