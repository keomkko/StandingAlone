using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<ObjectSpawn> objectSpawn = new List<ObjectSpawn>();
    private float spawnTime = 40f;
    public float curTime;

    public static SpawnManager _instance;

    private void Start()
    {
        ObjectListInfo();
        SearchObject();
        _instance = this;
        curTime = spawnTime;
        for (int i = 0; i < objectSpawn.Count; i++)
        {
            if (objectSpawn[i].objectType == ObjectSpawn.ObjectType.StuckObject)
            {
                for (int x = 0; x < objectSpawn[i].spawnPoints.Length; x++)
                {
                    Instantiate(objectSpawn[i].Object, objectSpawn[i].spawnPoints[x]);
                }
            }
        }
        
    }

    private void Update()
    {

        if (curTime >= spawnTime)
        {
            for (int i = 0; i < objectSpawn.Count; i++)
            {
                if (objectSpawn[i].curCount < objectSpawn[i].maxCount)
                {
                    int x = Random.Range(0, objectSpawn[i].spawnPoints.Length);
                    if (!objectSpawn[i].IsSpawn[x])
                    {
                        Spawn(x, i);
                    }
                }
                else
                {
                    curTime = 0;
                }
            }
        }
        curTime += Time.deltaTime;
    }

    public void Spawn(int _x, int index)
    {
        curTime = 0;
        objectSpawn[index].curCount++;
        Instantiate(objectSpawn[index].Object, objectSpawn[index].spawnPoints[_x]);
        objectSpawn[index].IsSpawn[_x] = true;
    }

    public void ObjectListInfo()
    {
        objectSpawn.Add(new ObjectSpawn(01, "Boar", ObjectSpawn.ObjectType.AnimalObject, 0, 2));
        objectSpawn.Add(new ObjectSpawn(02, "Deer", ObjectSpawn.ObjectType.AnimalObject, 0, 3));
        objectSpawn.Add(new ObjectSpawn(03, "Gull", ObjectSpawn.ObjectType.AnimalObject, 0, 6));
        objectSpawn.Add(new ObjectSpawn(04, "Hen", ObjectSpawn.ObjectType.AnimalObject, 0, 4));
        objectSpawn.Add(new ObjectSpawn(05, "Palm", ObjectSpawn.ObjectType.StuckObject, 10, 10));
        objectSpawn.Add(new ObjectSpawn(06, "Tree", ObjectSpawn.ObjectType.StuckObject, 10, 10));
        objectSpawn.Add(new ObjectSpawn(07, "Grass", ObjectSpawn.ObjectType.StuckObject, 10, 10));
        objectSpawn.Add(new ObjectSpawn(08, "Rocks", ObjectSpawn.ObjectType.StuckObject, 10, 10));
    }

    public void SearchObject()
    {
        for (int i = 0; i < objectSpawn.Count; i++)
        {
            if (objectSpawn[i].ObjectName == transform.GetChild(i).name)
            {
                objectSpawn[i].SpawnObject = transform.GetChild(i).gameObject;

                objectSpawn[i].spawnPoints = new Transform[transform.GetChild(i).childCount];
                for (int j = 0; j < transform.GetChild(i).childCount; j++)
                {
                    objectSpawn[i].spawnPoints[j] = transform.GetChild(i).GetChild(j).transform;
                    objectSpawn[i].IsSpawn = new bool[objectSpawn[i].spawnPoints.Length];
                    objectSpawn[i].IsSpawn[j] = false;
                }
            }
        }

    }
}

[System.Serializable]
public class ObjectSpawn
{
    public int ObjectID;
    public string ObjectName;
    public GameObject SpawnObject;
    public GameObject Object;
    public ObjectType objectType;
    public Transform[] spawnPoints;
    public bool[] IsSpawn;
    public int curCount;
    public int maxCount;

    public enum ObjectType
    {
        AnimalObject,
        StuckObject
    }

    public ObjectSpawn(int _ObjectID, string _ObjectName, ObjectType _objectType, int _curCount, int _maxCount)
    {
        ObjectID = _ObjectID;
        ObjectName = _ObjectName;
        objectType = _objectType;
        Object = Resources.Load("Prefabs/" + _ObjectName) as GameObject;
        curCount = _curCount;
        maxCount = _maxCount;
    }
}