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
    public ParticleSystem Explosion;
    public ParticleSystem ExplosionSmoke;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceFuseStart += Time.deltaTime;

        if (timeSinceFuseStart >= fuseTimer)
        {
            Ray hit;
            hitObjCol = Physics.OverlapSphere(this.transform.position, explosionRadius);
            foreach (Collider col in hitObjCol)
            {
                hitObjects.Add(col.gameObject);
            }
            foreach (GameObject explodedObj in hitObjects)
            {
                if (explodedObj.GetComponent<Rigidbody>())
                {
                    explodedObj.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius);
                    explodedObj.GetComponent<Rigidbody>().angularVelocity = explodedObj.GetComponent<Rigidbody>().transform.right * Random.Range(-15, 15);

                }
            }
            //Bomb fx
            ParticleSystem ps1 = Instantiate(Explosion);
            ps1.transform.position = transform.position;
            ps1.Play();
            Destroy(ps1.gameObject, ps1.main.duration);

            ParticleSystem ps2 = Instantiate(ExplosionSmoke);
            ps2.transform.position = transform.position;
            ps2.Play();
            Destroy(ps2.gameObject, ps2.main.duration);

            Camera.main.GetComponent<CameraShake>().ShakeCamera(1.0f);
            //

            Destroy(this.gameObject);
        }
    }
}
