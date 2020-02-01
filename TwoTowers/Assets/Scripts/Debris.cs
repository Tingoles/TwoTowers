using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem smokePuff;

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
    }
}
