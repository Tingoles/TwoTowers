using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberController : MonoBehaviour
{
    public float m_speed = 2f;
    public Transform m_grabberPos;
    public GameObject m_endSection;
    public bool m_grabbed = false;
    GameObject m_grabbedObj;
    GrabberInput m_input;
    public FMODUnity.StudioEventEmitter emitter;
    // Start is called before the first frame update
    void Start()
    {
        m_input = GetComponent<GrabberInput>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (m_input.m_grabInput)
        {
            Debug.Log("grab");
            if (!m_grabbed)
            {
                Grab();
            }
            else
            {
                Drop();
            }
        }
        Vector3 velocity = m_input.m_inputVec * m_speed * Time.deltaTime;
        transform.position += velocity;
        updateFmodParams(velocity);
    }

    void Grab()
    {
        Collider[] cols = Physics.OverlapSphere(m_grabberPos.position, .5f, LayerMask.GetMask("Pickable"));
        if (cols.Length == 0) return;
        if (cols[0])
        {
            cols[0].gameObject.AddComponent<FixedJoint>();
            cols[0].gameObject.GetComponent<FixedJoint>().connectedBody = m_endSection.GetComponent<Rigidbody>();
            m_grabbedObj = cols[0].gameObject;
            m_grabbed = true;
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

    void updateFmodParams(Vector3 velocity)
    {
        float xVelocity = velocity.x;
        if(xVelocity > -0.005f && xVelocity < 0.005f)
        {
            emitter.Stop();
        }
        else if (xVelocity < 0) 
        {
            if (!emitter.IsPlaying()) emitter.Play();
            xVelocity *= -1.0f;
        }
        else
        {
            if (!emitter.IsPlaying()) emitter.Play();
        }
            
        emitter.SetParameter("Velocity", xVelocity);
    }
}
