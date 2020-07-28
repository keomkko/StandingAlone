using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    protected string plantName;
    public int hp;

    public int[] itemID;
    public int[] _count;
    protected BoxCollider2D boxCol;

    public int[] probability;
    protected int a;

    private void Awake()
    {
        itemID = new int[2];
        _count = new int[2];
        probability = new int[2];
    }
    public void TakeDamage(int player_dmg)
    {
        for (int i = 0; i < 2; i++)
        {
            probability[i] = Random.Range(1, 100);

            if (probability[i] <= a)
            {
                _count[i] = 1;
            }
            else if (probability[i] > a)
            {
                _count[i] = 2;
            }
        }

        hp -= player_dmg;

        if (hp <= 0)
        {
            for (int i = 0; i < 2; i++)
            {
                Inventory.instance.GetAnItem(itemID[i], _count[i]);
            }
            Destroy(this.gameObject);
        }
    }
}
