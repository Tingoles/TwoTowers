using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairDebris : MonoBehaviour
{
    BlockManager blockManager;

    public ParticleSystem combineFX;

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
