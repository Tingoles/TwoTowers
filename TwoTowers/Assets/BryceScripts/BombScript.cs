using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public float fuseTimer = 3;
    float timeSinceFuseStart = 0;          
    public float explosionForce = 10;
    public float explosionRadius = 5;
    List<GameObject> hitObjects = new List<GameObject>();
    Collider[] hitObjCol;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceFuseStart += Time.deltaTime;

            if(timeSinceFuseStart >= fuseTimer)
            {
                Ray hit;
                hitObjCol = Physics.OverlapSphere(this.transform.position, explosionRadius);
                foreach (Collider col in hitObjCol)
                {
                    hitObjects.Add(col.gameObject);
                }
                foreach (GameObject explodedObj in hitObjects)
                {
                    if(explodedObj.GetComponent<Rigidbody>())
                    {
                        explodedObj.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius);
                    }
                }
               
                Destroy(this.gameObject);
            }

    }

}
