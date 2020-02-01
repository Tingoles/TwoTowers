using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDayNight : MonoBehaviour
{
    public float rotate_speed;
    public Vector3 dir;

    void Update()
    {
        transform.Rotate(dir * Time.deltaTime * rotate_speed, Space.World);
    }
}
