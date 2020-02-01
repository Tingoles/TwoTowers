using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDayNight : MonoBehaviour
{
    public float rotateSpeed;
    public Vector3 dir;
    public float maxRotateTime;
    private float timePassed;

    void Update()
    {
        if (timePassed < maxRotateTime)
        {
            transform.Rotate(dir * Time.deltaTime * rotateSpeed, Space.World);
            timePassed += Time.deltaTime;
        }
    }
}
