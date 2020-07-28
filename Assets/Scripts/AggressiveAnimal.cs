using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveAnimal : Animal
{
    protected int atkDamage;    //공격력
    public float attackDelay; //공격 유예

    public float inter_MoveWaitTime; //대기시간
    private float current_interMWT;

    private Vector2 PlayerPos;

    private int random_int;
    private string direction;

    private void Start()
    {
         
    }
}
