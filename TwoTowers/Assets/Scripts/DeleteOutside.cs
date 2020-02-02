using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOutside : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == 8)
        {
            BlockManager bm = GameObject.FindGameObjectWithTag("BlockManager").GetComponent<BlockManager>();
            bm.activeBlocks.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
