using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDebris : MonoBehaviour
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
        if (collision.gameObject.GetComponent<BombDebris>())
        {
            blockManager.activeBlocks.Remove(collision.gameObject);
            bombPlacement = collision.gameObject.transform.position;
            Destroy(collision.gameObject);
            GameObject temp = Instantiate(bomb);
            temp.transform.position = bombPlacement;
            Destroy(gameObject);
        }
    }
}