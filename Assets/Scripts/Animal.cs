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
    private Vector3 vector;

    protected int random_int;
    protected string direction;

    protected float speed = 0.1f;
    protected int walkCount = 1;
    protected int currentWalkCount;

    protected bool mobCanMove = true;

    public Queue<string> queue;
    protected float current_interMWT;
    protected float inter_MoveWaitTime = 0.1f; //대기 시간

    public int probability;
    protected int a;

    void Start()
    {
        queue = new Queue<string>();
        current_interMWT = inter_MoveWaitTime;
    }

    void Update()
    {
        current_interMWT -= Time.deltaTime;

        if (current_interMWT <= 0)
        {
            current_interMWT = inter_MoveWaitTime;

            RandomDirection();

            Move(direction);
        }

    }

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

        if (hp <= 0)
        {
            Inventory.instance.GetAnItem(itemID, _count);
            Destroy(this.gameObject);
        }
    }
    protected void Move(string _dir)
    {
        StartCoroutine(MoveCoroutine(_dir));
    }

    IEnumerator MoveCoroutine(string _dir)
    {
        mobCanMove = false;
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
                vector.x = 1f;
                break;
            case "LEFT":
                vector.x = -1f;
                break;
        }
        while (currentWalkCount < walkCount)
        {
            transform.Translate(vector.x * speed, vector.y * speed, 0);

            currentWalkCount++;
            yield return null;
        }
        currentWalkCount = 0;
        mobCanMove = true;
    }
    private void RandomDirection()
    {
        vector.Set(0, 0, vector.z);
        random_int = Random.Range(0, 4);
        switch (random_int)
        {
            case 0:
                vector.y = 1f;
                direction = "UP";
                break;
            case 1:
                vector.y = -1f;
                direction = "DOWN";
                break;
            case 2:
                vector.x = 1f;
                direction = "RIGHT";
                break;
            case 3:
                vector.x = -1f;
                direction = "LEFT";
                break;
        }
    }

}
