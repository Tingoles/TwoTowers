using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem smokePuff;
    FMODUnity.StudioEventEmitter emitter;

    private void Start()
    {
        emitter = GetComponent<FMODUnity.StudioEventEmitter>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            if (GetComponent<Rigidbody>().velocity.magnitude > 0.1)
            {
                ParticleSystem ps = Instantiate(smokePuff, transform);
                ps.Play();
                Destroy(ps.gameObject, ps.main.duration);
            }
        }

        float velocity = GetComponent<Rigidbody>().velocity.magnitude/30.0f;
        if (velocity > 0.05)
        {
            print(velocity);
            emitter.SetParameter("Velocity", velocity);
            emitter.Play();
        }
    }
}
