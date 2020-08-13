using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocileAnimal : Animal
{
    private void Start()
    {
        current_interMWT = inter_MoveWaitTime;
    }

    private void Update()
    {
        if (hp <= 0)
        {
            Death();
        }

        current_interMWT -= Time.deltaTime;

        if (current_interMWT <= 0)
        {
            current_interMWT = inter_MoveWaitTime;

            RandomDirection();

            Move(direction);
        }

        
    }
    public void Death()
    {
        //SpawnManager._instance.curCount--;
        //SpawnManager._instance.isSpawn[int.Parse(transform.parent.name) - 1] = false;
        Inventory.instance.GetAnItem(itemID, _count);
        Destroy(this.gameObject);
    }
}
