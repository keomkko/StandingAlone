using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;

public class Animal : MonoBehaviour
{
    private SpawnManager spawnManager;

    public int hp;
    public int itemID;
    public int _count;
    protected Rigidbody2D rb;
    public BoxCollider2D boxCol;
    public LayerMask layerMask;
    protected Vector3 vector;
    protected Animator animator;

    protected int random_int;
    public string direction;

    public float speed = 0.025f;
    protected int walkCount = 75; //동물 오브젝트 각각 walkCount 값 설정해주기
    public int currentWalkCount;
    public bool mobCanMove = true;
    public bool hit = false;
    protected bool isDead = false;
    protected bool DeathTrigger = false;

    protected float current_interMWT;
    protected float inter_MoveWaitTime = 2f; //대기 시간
    public int probability;
    protected int a;

    public string DeathSound;
    public string AttackSound;
    protected AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public void TakeDamage(int player_dmg)
    {
        hit = true;
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

        audioManager.Play(AttackSound);
    }
    public void Death()
    {
        audioManager.Play(DeathSound);
        isDead = true;
        StartCoroutine(DeathCoroutine());
    }

    IEnumerator DeathCoroutine()
    {
        if(!DeathTrigger)
        {
            DeathTrigger = true;
            animator.SetTrigger("Death");
        }
        yield return new WaitForSeconds(1f);

        for(int i = 0; i < SpawnManager._instance.objectSpawn.Count; i++)
        {
            if (SpawnManager._instance.objectSpawn[i].ObjectName == transform.parent.parent.name)
            {
                SpawnManager._instance.objectSpawn[i].curCount--;
                SpawnManager._instance.objectSpawn[i].IsSpawn[int.Parse(transform.parent.name) - 1] = false;
            }
            yield return null;
        }
        Inventory.instance.GetAnItem(itemID, _count);
        Destroy(gameObject);
    }

    public void Move(string _dir)
    {
        DirX();
        StartCoroutine(MoveCoroutine(_dir));
    }

    public void Stop(string _dir)
    {
        StopCoroutine(MoveCoroutine(_dir));
    }

    IEnumerator MoveCoroutine(string _dir)
    {
        vector.Set(0, 0, vector.z);
        switch (_dir)
        {
            case "UP":
                vector.y = 1f;
                break;
            case "DOWN":
                vector.y = -1f;
                break;
            case "RIGHT":
                vector.x = -1f;
                break;
            case "LEFT":
                vector.x = -1f;
                break;
            case "STOP":
                vector.x = 0;
                vector.y = 0;
                break;
        }
        while (currentWalkCount < walkCount)
        {
            transform.Translate(vector.x * speed, vector.y * speed, 0);
            currentWalkCount++;
            if (direction != "STOP")
            {
                animator.SetBool("IsRunning", true);
            }
            else
            {
                animator.SetBool("IsRunning", false);
            }
            yield return new WaitForSeconds(0.02f);
        }
        currentWalkCount = 0;
        animator.SetBool("IsRunning", false);
    }
    protected void RandomDirection()
    {
        vector.Set(0, 0, vector.z);
        random_int = Random.Range(0, 5);
        switch (random_int)
        {
            case 0:
                direction = "UP";
                break;
            case 1:
                direction = "DOWN";
                break;
            case 2:
                direction = "RIGHT";
                break;
            case 3:
                direction = "LEFT";
                break;
            case 4:
                direction = "STOP";
                break;
        }
    }

    void DirX()
    {
        if (direction == "RIGHT")
        {
            transform.eulerAngles = new Vector3(0, 180f, 0);
            
        }
        else if (direction == "LEFT")
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

}