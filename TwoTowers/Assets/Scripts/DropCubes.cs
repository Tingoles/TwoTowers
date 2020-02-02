using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCubes : MonoBehaviour
{

    public float timeTillSpawn = 30;

    public List<GameObject> leftBlocks = new List<GameObject>();
    public List<GameObject> rightBlocks = new List<GameObject>();

    private int numBlocks = 5;
    public List<GameObject> BlocksToSpawn = new List<GameObject>();
    Vector3 locationToSpawn;
    public GameObject blockPoolManager;

    public bool leftSpawner = false;

    public GameObject leftBulldozer;
    public GameObject rightBulldozer;


    private void Update()
    {
        foreach (GameObject block in blockPoolManager.GetComponent<BlockManager>().activeBlocks)
        {
            if (!leftSpawner && block.transform.position.x > 0)
            {
                rightBlocks.Add(block);
            }
            if (leftSpawner && block.transform.position.x < 0)
            {
                leftBlocks.Add(block);
            }
        }
        timeTillSpawn -= Time.deltaTime;


        if (rightBlocks.Count < 10 && timeTillSpawn < 0 && !leftSpawner)
        {
            rightBulldozer.GetComponent<Bulldozer>().CreateCubes(5);
            timeTillSpawn = 15;
        }
        if (leftBlocks.Count < 10 && timeTillSpawn < 0 && leftSpawner)
        {
            leftBulldozer.GetComponent<Bulldozer>().CreateCubes(5);
            timeTillSpawn = 15;

        }
        rightBlocks.Clear();
        leftBlocks.Clear();
    }

}
