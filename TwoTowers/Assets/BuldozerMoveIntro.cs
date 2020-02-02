using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuldozerMoveIntro : MonoBehaviour
{
    public bool m_move = false;
    public float m_speed = 1.0f;
    public float m_delay = 1.0f;
    public float m_stopDelay = 2.0f;

    void Start()
    {
        Invoke("Move", m_delay);
        Invoke("Move", m_delay + m_stopDelay);
    }

    private void Move()
    {
        m_move = !m_move;
    }

    void Update()
    {
        if(m_move)
        {
            transform.Translate(Vector3.right * m_speed * Time.deltaTime);
        }
    }
}
