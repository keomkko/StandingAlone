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
        //for (int i = 0; i < SpawnManager._instance.objectSpawn.Count; i++)
        //{
        //    if (SpawnManager._instance.objectSpawn[i].ObjectName == "Boar")
        //    {
        //        Debug.Log(Vector2.Distance(player.transform.position, transform.position));
        //    }
        //}
        
        if (hp <= 0)
        {
            Death();
        }

        current_interMWT -= Time.deltaTime;
        if(hit == false)
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

    private void Follow()
    {
        if(Vector2.Distance(player.transform.position, transform.position)> 4f)
        {
            hit = false;
        }
        else if(Vector2.Distance(player.transform.position, transform.position) > 1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed/3);
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetTrigger("Attack");
            Attack();
        }
    }

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
        for (int i = 0; i < SpawnManager._instance.objectSpawn.Count; i++)
        {
            Debug.Log(transform.parent.parent.name);
            if (SpawnManager._instance.objectSpawn[i].ObjectName == transform.parent.parent.name)
            {
                Debug.Log("No Bug..");
                SpawnManager._instance.objectSpawn[i].curCount--;
                SpawnManager._instance.objectSpawn[i].IsSpawn[int.Parse(transform.parent.name)-1] = false;
            }
        }
        Inventory.instance.GetAnItem(itemID, _count);
        Destroy(this.gameObject);
    }
}

