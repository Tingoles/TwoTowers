using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberController : MonoBehaviour
{
    public bool m_active = false;
    public float m_speed = 2f;
    public Transform m_grabberPos;
    public GameObject m_endSection;
    public bool m_grabbed = false;
    GameObject m_grabbedObj;
    GrabberInput m_input;
    public Animator m_grabAnim;
    public Transform m_leftPos;
    public Transform m_rightPos;


    // Start is called before the first frame update
    void Start()
    {
        m_input = GetComponent<GrabberInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_active) return;
        Movement();
        m_grabAnim.SetBool("Grabbed", m_grabbed);
    }

    void Movement()
    {
        if (m_input.m_grabInput)
        {
            if (!m_grabbed)
            {
                Grab();
            }
            else
            {
                Drop();
            }
        }
        transform.position += m_input.m_inputVec * m_speed * Time.deltaTime;
        RestrictX();
    }

    void RestrictX()
    {
        Vector3 newPos = transform.position;
        if (transform.position.x > m_rightPos.position.x)
        {
            newPos.x = m_rightPos.position.x;
        }
        if (transform.position.x < m_leftPos.position.x)
        {
            newPos.x = m_leftPos.position.x;
        }
        transform.position = newPos;
    }

    void Grab()
    {
        m_grabbed = true;
        Collider[] cols = Physics.OverlapSphere(m_grabberPos.position, .5f, LayerMask.GetMask("Pickable"));
        if (cols.Length == 0) return;
        if (cols[0])
        {
            cols[0].gameObject.AddComponent<FixedJoint>();
            cols[0].gameObject.GetComponent<FixedJoint>().connectedBody = m_endSection.GetComponent<Rigidbody>();
            m_grabbedObj = cols[0].gameObject;
        }
    }

    void Drop()
    {
        m_grabbed = false;
        Destroy(m_grabbedObj.GetComponent<FixedJoint>());
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(m_grabberPos.position, .5f);
    }
}
