using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightScore : MonoBehaviour
{
    public float topHeight = 0;

    BlockManager blockManager;

    private void Start()
    {
        blockManager = GameObject.FindGameObjectWithTag("BlockManager").GetComponent<BlockManager>();
    }

    // Update is called once per frame
    void MeasureHeight()
    {
        foreach (GameObject block in blockManager.activeBlocks)
        {
            float blockY = block.transform.position.y;
            if (blockY > topHeight)
            {
                topHeight = blockY;
            }
        }
    }
}