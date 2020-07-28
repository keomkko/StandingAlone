using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bore : AggressiveAnimal
{
    private void Start()
    { 
        hp = 250;
        itemID = 10001;
        a = 85;
        atkDamage = 12;
    }
}
