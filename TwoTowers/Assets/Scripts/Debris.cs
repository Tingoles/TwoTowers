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
                if (smokePuff)
                {
                    ParticleSystem ps = Instantiate(smokePuff, transform);
                    ps.Play();
                    Destroy(ps.gameObject, ps.main.duration);
                }
            }
        }

        float velocity = Mathf.Abs(GetComponent<Rigidbody>().velocity.magnitude) / 30.0f;

        if(emitter != null)
        {
            if (velocity > 0.01f)
            {
                emitter.SetParameter("Velocity", Mathf.Pow(velocity, 2));
                emitter.Play();
            }
        }
    }
}
