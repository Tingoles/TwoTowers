using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDebris : MonoBehaviour
{
    public GameObject bomb;

    BlockManager blockManager;

    Vector3 bombPlacement;

    public ParticleSystem combineFX;

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
            GameObject temp = Instantiate(bomb);
            temp.transform.position = bombPlacement;

            //fx
            ParticleSystem ps = Instantiate(combineFX);
            ps.transform.position = transform.position;
            ps.Play();
            Destroy(ps.gameObject, ps.main.duration);
            //
            
            Destroy(gameObject);
        }
    }
}