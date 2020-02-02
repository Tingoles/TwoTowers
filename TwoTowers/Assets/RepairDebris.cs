using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairDebris : MonoBehaviour
{
    public GameObject bomb;

    BlockManager blockManager;

    Vector3 bombPlacement;

    private void Start()
    {
        blockManager = GameObject.FindGameObjectWithTag("BlockManager").GetComponent<BlockManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponent<RepairDebris>())
        {
            blockManager.activeBlocks.Remove(collision.gameObject);
            if(transform.position.x<0)
            {
                blockManager.leftBulldozer.GetComponent<Bulldozer>().CreateCubes(2);
            }
            if (transform.position.x > 0)
            {
                blockManager.rightBulldozer.GetComponent<Bulldozer>().CreateCubes(2);
            }

            Destroy(gameObject);
        }
    }
}
