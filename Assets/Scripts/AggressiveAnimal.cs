using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveAnimal : Animal
{
    public int atkDamage;
    public float attackDelay; //공격력
    public float attackCooltime = 2; //공격 쿨타임

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
        if (hp <= 0)
        {
            Death();
        }

        else
        {
            current_interMWT -= Time.deltaTime;
            if (current_interMWT <= 0)
            {
                current_interMWT = inter_MoveWaitTime;
                RandomDirection();
                Move(direction);
            }
        }

        if (attackDelay >= 0)
        {
            attackDelay += Time.deltaTime;
        }
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

    //void FollowTarget()
    //{
    //    if (Vector2.Distance(transform.position, player.transform.position) > contactDistance && follow)
    //        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed / 2);
    //    else
    //        Attack();
    //}
    public void Attack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(boxpos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Player")
            {
                animator.SetTrigger("Attack");
                Debug.Log("damage");
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxpos.position, boxSize);
    }

    public void Death()
    {
        SpawnManager._instance.curCount--;
        SpawnManager._instance.isSpawn[int.Parse(transform.parent.name) - 1] = false;
        Inventory.instance.GetAnItem(itemID, _count);
        Destroy(this.gameObject);
    }
}

