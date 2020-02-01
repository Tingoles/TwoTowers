using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTowards : MonoBehaviour
{
    public bool start = false;
    public Vector3 target;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * speed);
        }
    }
}
