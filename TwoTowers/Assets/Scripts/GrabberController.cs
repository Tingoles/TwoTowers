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
    public Transform m_minPos;
    public Transform m_maxPos;


    private FMODUnity.StudioEventEmitter startMotor;
    private FMODUnity.StudioEventEmitter motor;
    private FMODUnity.StudioEventEmitter stopMotor;
    private FMODUnity.StudioEventEmitter extensionMotor;

    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponentsInParent<FMODUnity.StudioEventEmitter>()[0];
        stopMotor = GetComponentsInParent<FMODUnity.StudioEventEmitter>()[1];
        startMotor = GetComponentsInParent<FMODUnity.StudioEventEmitter>()[2];
        extensionMotor = GetComponentsInParent<FMODUnity.StudioEventEmitter>()[3];

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
        Vector3 velocity = m_input.m_inputVec * m_speed * Time.deltaTime;
        transform.position += velocity;
        updateFmodParams(velocity);
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

    void updateFmodParams(Vector3 velocity)
    {
        updateXVelocity(velocity);
        updateYVelocity(velocity);
    }

    void updateXVelocity(Vector3 velocity)
    {
        float xVelocity = velocity.x * 6.0f;
        if (xVelocity > -0.005f && xVelocity < 0.005f)
        {
            if (motor.IsPlaying())
            {
                stopMotor.Play();
                motor.Stop();
            }
        }
        else if (xVelocity < 0)
        {
            if (!motor.IsPlaying())
            {
                startMotor.Play();
                motor.Play();
            }

            xVelocity *= -1.0f;
        }
        else
        {
            if (!motor.IsPlaying())
            {
                startMotor.Play();
                motor.Play();
            }
        }

        motor.SetParameter("Velocity", xVelocity);
    }

    void updateYVelocity(Vector3 velocity)
    {
        float yVelocity = velocity.y * 6.0f;
        if (yVelocity > -0.005f && yVelocity < 0.005f)
        {
            if (extensionMotor.IsPlaying())
            {
                extensionMotor.Stop();
            }
        }
        else if (yVelocity < 0)
        {
            if (!extensionMotor.IsPlaying())
            {
                extensionMotor.Play();
            }

            yVelocity *= -1.0f;
        }
        else
        {
            if (!extensionMotor.IsPlaying())
            {
                extensionMotor.Play();
            }
        }

        extensionMotor.SetParameter("Velocity", yVelocity);
    }
}
