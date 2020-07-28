using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocks : MonoBehaviour
{
    public int hp = 175;

    public int itemID = 20004;
    public int _count;
    private BoxCollider2D boxCol;

    public int probability;
    public int a = 55;

    public void TakeDamage(int player_dmg)
    {
        probability = Random.Range(1, 100);

        if (probability <= a)
        {
            _count = 1;
        }
        else if (probability > a)
        {
            _count = 2;
        }

        hp -= player_dmg;

        if (hp <= 0)
        {
            Inventory.instance.GetAnItem(itemID, _count);
            Destroy(this.gameObject);
        }
    }
}
