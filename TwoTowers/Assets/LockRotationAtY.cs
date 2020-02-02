using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotationAtY : MonoBehaviour
{
    public float m_yPos;
    private Rigidbody m_rigidbody;

    public void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        m_rigidbody.freezeRotation = transform.position.y > m_yPos;
    }
}
