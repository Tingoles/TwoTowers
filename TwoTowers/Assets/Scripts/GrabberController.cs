using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberController : MonoBehaviour
{
    public float m_moving = 0;
    public bool m_active = false;
    public float m_speed = 2f;
    public Transform m_grabberPos;
    public GameObject m_endSection;
    public bool m_grabbed = false;
    GameObject m_grabbedObj;
    GrabberInput m_input;
    public Animator m_grabAnim;
    public Transform m_minPos;
    public Transform m_maxPos;

    public float m_movingTransitionSpeed = 1.0f;
    public Vector2 m_chainAngularDrag;
    public Vector2 m_chainDrag;
    public Vector2 m_chainMass;
    public Vector2 m_grabberAngularDrag;
    public Vector2 m_grabberDrag;
    public Vector2 m_grabberMass;

    public List<Rigidbody> m_chainRigids = new List<Rigidbody>();
    public List<Rigidbody> m_grabberRigids = new List<Rigidbody>();

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



        foreach (Rigidbody r in m_chainRigids)
        {
            r.angularDrag = Mathf.Lerp(m_chainAngularDrag.x, m_chainAngularDrag.y, m_moving);
            r.drag = Mathf.Lerp(m_chainDrag.x, m_chainDrag.y, m_moving);
            r.mass = Mathf.Lerp(m_chainMass.x, m_chainMass.y, m_moving);
        }
        foreach (Rigidbody r in m_grabberRigids)
        {
            r.angularDrag = Mathf.Lerp(m_grabberAngularDrag.x, m_grabberAngularDrag.y, m_moving);
            r.drag = Mathf.Lerp(m_grabberDrag.x, m_grabberDrag.y, m_moving);
            r.mass = Mathf.Lerp(m_grabberMass.x, m_grabberMass.y, m_moving);
        }
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
        Vector3 pre = transform.position;
        transform.position += m_input.m_inputVec * m_speed * Time.deltaTime;
        m_moving = Mathf.MoveTowards(m_moving, Mathf.Abs(pre.x - transform.position.x) > 0 ? 1 : 0, Time.deltaTime * m_movingTransitionSpeed);
        Restrict();
    }

    void Restrict()
    {
        Vector3 newPos = transform.position;
        if (transform.position.x > m_maxPos.position.x)
        {
            newPos.x = m_maxPos.position.x;
        }
        if (transform.position.x < m_minPos.position.x)
        {
            newPos.x = m_minPos.position.x;
        }
        if (transform.position.y > m_maxPos.position.y)
        {
            newPos.y = m_maxPos.position.y;
        }
        if (transform.position.y < m_minPos.position.y)
        {
            newPos.y = m_minPos.position.y;
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
            cols[0].gameObject.GetComponent<Collider>().enabled = false;

            m_grabbedObj = cols[0].gameObject;
        }
    }

    void Drop()
    {
        m_grabbed = false;
        if (m_grabbedObj)
        {
            Destroy(m_grabbedObj.GetComponent<FixedJoint>());
            m_grabbedObj.GetComponent<Collider>().enabled = true;

        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(m_grabberPos.position, .5f);
        Gizmos.DrawSphere(m_maxPos.position, .5f);
        Gizmos.DrawSphere(m_minPos.position, .5f);



    }
}
