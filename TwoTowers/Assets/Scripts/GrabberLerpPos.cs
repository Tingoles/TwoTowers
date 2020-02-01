using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberLerpPos : MonoBehaviour
{
    float m_time = 3;
    float m_distance = 10;
    Vector3 m_startPos;
    Vector3 m_endPos;
    float t = 0;
    // Start is called before the first frame update
    void Start()
    {
        m_startPos = transform.position;
        m_endPos = m_startPos;
        m_endPos.y -= m_distance;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(m_startPos, m_endPos, t);
        t += Time.deltaTime / m_time;
        if (t >= 1)
        {
            GetComponentInChildren<GrabberController>().m_active = true;
            Destroy(this);
        }
    }
}
