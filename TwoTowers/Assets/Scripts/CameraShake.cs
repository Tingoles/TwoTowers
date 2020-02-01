using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 original_pos;
    private bool shake_camera = false;
    private float shake_strength = 0;

    private float time_passed = 0;

    void Start()
    {
        original_pos = transform.position;
    }

    void Update()
    {
        if (shake_camera)
        {
            transform.position = original_pos + Random.insideUnitSphere * shake_strength;
            time_passed += time_passed + Time.deltaTime;
            if (time_passed > 1.0f)
            {
                shake_camera = false;
                transform.position = original_pos;
                time_passed = 0;
            }
        }
    }

    public void ShakeCamera(float strength)
    {
        shake_strength = strength;
        shake_camera = true;
    }
}
