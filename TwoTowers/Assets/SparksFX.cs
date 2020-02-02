using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparksFX : MonoBehaviour
{
    public ParticleSystem sparksFX;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Claw")
        {
            ParticleSystem ps = Instantiate(sparksFX);
            ps.transform.position = transform.position;
            Destroy(ps.gameObject, ps.main.duration);
        }
    }
}
