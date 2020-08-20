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
            if (!isDead)
            {
                Death();
            }
            speed = 0;
        }

        current_interMWT -= Time.deltaTime;

        if (current_interMWT <= 0)
        {
            current_interMWT = inter_MoveWaitTime;

            RandomDirection();

            Move(direction);
        }
    }
}
