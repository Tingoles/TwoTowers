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

    private Vector3 originalScale;
    private Color colour;

    public Gradient colourGradient;

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
        colour = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceFuseStart += Time.deltaTime;

        transform.localScale = Vector3.Lerp(originalScale, (originalScale * 1.2f), timeSinceFuseStart / fuseTimer);

        Color newColour = colourGradient.Evaluate(timeSinceFuseStart / fuseTimer);
        GetComponent<Renderer>().material.color = newColour;
        GetComponent<Renderer>().material.SetColor("_EmissionColor", newColour * (timeSinceFuseStart / fuseTimer));

        transform.position += Random.insideUnitSphere * (timeSinceFuseStart / fuseTimer)*0.08f;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

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
