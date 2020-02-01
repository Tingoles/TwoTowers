using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public List<GameObject> activeBlocks = new List<GameObject>();

    public void addBlocksToList(GameObject block)
    {
        activeBlocks.Add(block);
    }

}
