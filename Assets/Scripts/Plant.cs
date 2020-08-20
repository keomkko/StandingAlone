using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    Animator animator;

    protected string plantName;
    public int hp;

    public int[] itemID;
    public int[] _count;
    protected BoxCollider2D boxCol;

    public int[] probability;
    protected int a;

    public string AttckSound;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        animator = GetComponent<Animator>();

        itemID = new int[2];
        _count = new int[2];
        probability = new int[2];
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
        for (int i = 0; i < 2; i++)
        {
            Inventory.instance.GetAnItem(itemID[i], _count[i]);
        }
        Destroy(this.gameObject);
    }
}
