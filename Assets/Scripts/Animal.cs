using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    protected string animalName;
    public int hp;

    public int itemID;
    public int _count;
    protected Animator anim;
    protected Rigidbody2D rigid;
    protected BoxCollider2D boxCol;

    public int probability;
    protected int a;
    
    public void TakeDamage(int player_dmg)
    {
        probability = Random.Range(1, 100);

        hp -= player_dmg;
        if (probability <= a)
        {
            _count = 1;
        }
        else if (probability > a)
        {
            _count = 2;
        }

        if(hp<=0)
        {
           Inventory.instance.GetAnItem(itemID, _count);
           Destroy(this.gameObject);
        }
    }
}
