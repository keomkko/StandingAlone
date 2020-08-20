using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocks : MonoBehaviour
{
    Animator animator;
    public int hp = 175;

    public int itemID = 20004;
    public int _count;
    private BoxCollider2D boxCol;

    public int probability;
    public int a = 55;

    public string AttckSound;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (hp <= 0)
        {
            Death();
        }
    }
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
        animator.SetTrigger("Hit");

        audioManager.Play(AttckSound);
    }

    private void Death()
    {
        for (int i = 0; i < SpawnManager._instance.objectSpawn.Count; i++)
        {
            if (SpawnManager._instance.objectSpawn[i].ObjectName == transform.parent.parent.name)
            {
                SpawnManager._instance.objectSpawn[i].curCount--;
                SpawnManager._instance.objectSpawn[i].IsSpawn[int.Parse(transform.parent.name) - 1] = false;
            }
        }
        Inventory.instance.GetAnItem(itemID, _count);
        Destroy(this.gameObject);
    }
}
