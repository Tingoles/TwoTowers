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

    public List<GameObject> BlocksToSpawn = new List<GameObject>();

    void Start()
    {
        CreateTower();
        //blockPoolManager = GameObject.FindGameObjectWithTag("BlockManager");
    }

    void CreateTower()
    {
        StartCoroutine(BlockSpawn());


    }

    IEnumerator BlockSpawn()
    {
        for (int i = 0; i < numBlocks; i++)
        {
            if(i == numBlocks-5)
            {
                GameObject bomb = Instantiate(bombPrefab);
                bomb.GetComponent<BombScript>().fuseTimer = 7;
                bomb.transform.position = transform.position;
            }

            GameObject newBlock = Instantiate(BlocksToSpawn[Random.Range(0, BlocksToSpawn.Count)], blockPoolManager.transform);
            locationToSpawn = this.transform.position;
            locationToSpawn.x = Random.Range(transform.position.x-2, transform.position.x +2);
            newBlock.transform.position = locationToSpawn;

            blockPoolManager.GetComponent<BlockManager>().addBlocksToList(newBlock);
            yield return new WaitForSeconds(blockSpawnRate);
        }
        yield return new WaitForSeconds(1);
    }
}

