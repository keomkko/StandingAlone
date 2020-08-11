using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int curCount;
    public int maxCount;
    public float spawnTime;
    public float curTime;
    public bool[] isSpawn;
    public Transform[] spawnPoints;
    public GameObject animal;

    public static SpawnManager _instance;

    private void Start()
    {
        isSpawn = new bool[spawnPoints.Length];
        for (int i = 0; i < isSpawn.Length; i++)
        {
            isSpawn[i] = false;
        }
        _instance = this;
        curTime = spawnTime;
    }

    private void Update()
    {

        if (curTime >= spawnTime)
        {
            if (curCount < maxCount)
            {
                int x = Random.Range(0, spawnPoints.Length);
                if (!isSpawn[x])
                {
                    Spawn(x);
                }
            }
            else
            {
                curTime = 0;
            }
        }
        curTime += Time.deltaTime;
    }

    public void Spawn(int _x)
    {
        curTime = 0;
        curCount++;
        Instantiate(animal, spawnPoints[_x]);
        isSpawn[_x] = true;
    }
}
