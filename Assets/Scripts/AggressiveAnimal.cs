using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveAnimal : Animal
{

    public int atkDamage;//공격력
    public float attackDelay;
    public float attackCooltime = 2; //공격 쿨타임
    Transform player;

    public bool follow = false;

    public Transform boxpos;
    public Vector2 boxSize;

    private void Start()
    {
        animator = GetComponent<Animator>();
        current_interMWT = inter_MoveWaitTime;
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (hp <= 0)
        {
            if(!isDead)
            {
                Death();
            }
            speed = 0;
        }

        current_interMWT -= Time.deltaTime;
        if (hit == false)
        {
            if (current_interMWT <= 0)
            {
                current_interMWT = inter_MoveWaitTime;
                RandomDirection();
                Move(direction);
            }
        }
        else
        {
            Stop(direction);
            Direction(player.position.x, transform.position.x);
            Follow();
        }

        attackDelay -= Time.deltaTime;
    }

    private void Direction(float target, float baseobj)
    {
        if (target < baseobj)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
    }

    private void Follow()
    {
        if (Vector2.Distance(player.transform.position, transform.position) > 4f)
        {
            hit = false;
        }
        else if (Vector2.Distance(player.transform.position, transform.position) > 1f)
        {
            if (attackDelay <= 1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed / 3);
                animator.SetBool("IsRunning", true);
            }
            else
            {
                animator.SetBool("IsRunning", false);
            }
        }
        else
        {
            if (attackDelay <= 0)
            {
                animator.SetTrigger("Attack");
                Attack();
            }
        }
    }

    public void Attack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(boxpos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Player")
            {
                collider.GetComponent<PlayerStat>().Hit(atkDamage);
            }
        }
        attackDelay = attackCooltime;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxpos.position, boxSize);
    }
}

