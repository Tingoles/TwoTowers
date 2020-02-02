using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBuilding : MonoBehaviour
{
    public int numBlocks = 30;
    private Vector3 locationToSpawn;
    public float blockSpawnRate = 0.2f; //time in seconds between each block spawning
    public GameObject blockPoolManager;
    public GameObject bombPrefab;
    public GameObject[] ToDeleteOnStart;

    public List<GameObject> BlocksToSpawn = new List<GameObject>();

    void Start()
    {
        CreateTower();
        //blockPoolManager = GameObject.FindGameObjectWithTag("BlockManager");
        ToDeleteOnStart = GameObject.FindGameObjectsWithTag("TowerWalls");


    }

    void CreateTower()
    {
        StartCoroutine(BlockSpawn());


    }

    IEnumerator BlockSpawn()
    {
        int bombSpawnIndex = Random.Range(0, 5);

        int spawnOffset = -4;
        for (int i = 0; i < numBlocks; i++)
        {
            if(i == bombSpawnIndex)
            {
                GameObject bomb = Instantiate(bombPrefab);
                bomb.GetComponent<BombScript>().fuseTimer = 7;
                bomb.GetComponent<BombScript>().explosionForce = 1500;
                locationToSpawn = this.transform.position;
                locationToSpawn.x = transform.position.x + spawnOffset;
                if (spawnOffset == 4)
                {
                    spawnOffset = -6;
                }

                bomb.transform.position = locationToSpawn;
            }
            else
            {
                GameObject newBlock = Instantiate(BlocksToSpawn[Random.Range(0, BlocksToSpawn.Count)], blockPoolManager.transform);
                locationToSpawn = this.transform.position;
                locationToSpawn.x = transform.position.x + spawnOffset;
                if (spawnOffset == 4)
                {
                    spawnOffset = -6;
                }
                newBlock.transform.position = locationToSpawn;


                blockPoolManager.GetComponent<BlockManager>().addBlocksToList(newBlock);
            }
            spawnOffset += 2;

            yield return new WaitForSeconds(blockSpawnRate);
        }
        yield return new WaitForSeconds(1.5f);
        foreach (GameObject block in blockPoolManager.GetComponent<BlockManager>().activeBlocks)
        {
            block.GetComponent<Rigidbody>().freezeRotation = false;
        }
        foreach(GameObject objToDelete in ToDeleteOnStart)
        {
            Destroy(objToDelete);
        }

    }
}

