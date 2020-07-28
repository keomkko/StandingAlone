using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : AggressiveAnimal
{
    // Start is called before the first frame update
    private void Start()
    {
        hp = 200;
        itemID = 10002;
        a = 85;
        atkDamage = 8;
    }
}
