using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class GrabberController : MonoBehaviour
{
    public bool m_active = false;
    public float m_speed = 2f;
    public Transform m_grabberPos;
    public GameObject m_endSection;
    public bool m_grabbed = false;
    List<GameObject> m_grabbedObj = new List<GameObject>() ;
    GrabberInput m_input;
    public Animator m_grabAnim;
    public Transform m_minPos;
    public Transform m_maxPos;

    [EventRef]
    public string m_motorRef;
    private EventInstance m_motorInstance;
    private PARAMETER_ID m_motorVelocityID;
    [EventRef]
    public string m_motorStartRef;
    private EventInstance m_motorStartInstance;
    [EventRef]
    public string m_motorEndRef;
    private EventInstance m_motorEndInstance;
    [EventRef]
    public string m_extentionRef;
    private EventInstance m_extentionInstance;
    private PARAMETER_ID m_extentionVelocityID;

    // Start is called before the first frame update
    void Start()
    {
        m_motorInstance = RuntimeManager.CreateInstance(m_motorRef);

        {
            EventDescription motorVelEventDesc = RuntimeManager.GetEventDescription(m_motorRef);
            PARAMETER_DESCRIPTION motorParameterDescription;
            motorVelEventDesc.getParameterDescriptionByName("Velocity", out motorParameterDescription);
            m_motorVelocityID = motorParameterDescription.id;
        }

        m_extentionInstance = RuntimeManager.CreateInstance(m_extentionRef);

        {
            EventDescription motorVelEventDesc = RuntimeManager.GetEventDescription(m_extentionRef);
            PARAMETER_DESCRIPTION motorParameterDescription;
            motorVelEventDesc.getParameterDescriptionByName("Velocity", out motorParameterDescription);
            m_extentionVelocityID = motorParameterDescription.id;
        }

        m_input = GetComponent<GrabberInput>();
    }

    private void OnDestroy()
    {
        m_motorInstance.release();
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
        if (velocity != Vector3.zero)
            Debug.Log("Moves");
        transform.position += velocity;
        updateFmodParams(velocity);
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
        Collider[] cols = Physics.OverlapSphere(m_grabberPos.position, 0.8f, LayerMask.GetMask("Pickable"));
       
        if (cols.Length == 0) return;
        foreach (Collider collis in cols)
        {

            collis.gameObject.AddComponent<FixedJoint>();
            collis.gameObject.GetComponent<FixedJoint>().connectedBody = m_endSection.GetComponent<Rigidbody>();
            collis.gameObject.GetComponent<Collider>().enabled = false;

           m_grabbedObj.Add(collis.gameObject);

        }
    }

    void Drop()
    {
        m_grabbed = false;
        foreach (GameObject g in m_grabbedObj)
        {
            Destroy(g.GetComponent<FixedJoint>());
            g.GetComponent<Collider>().enabled = true;

        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(m_grabberPos.position, 0.8f);
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

        PLAYBACK_STATE playbackState;
        m_motorInstance.getPlaybackState(out playbackState);
        bool isPlaying = playbackState != PLAYBACK_STATE.STOPPED;

        if (xVelocity > -0.005f && xVelocity < 0.005f)
        { 
            if (isPlaying)
            {
                m_motorEndInstance = RuntimeManager.CreateInstance(m_motorEndRef);
                m_motorEndInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
                m_motorEndInstance.start();
                m_motorEndInstance.release();
                m_motorInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
        }
        else if (xVelocity < 0)
        {
            if (!isPlaying)
            {
                m_motorStartInstance = RuntimeManager.CreateInstance(m_motorStartRef);
                m_motorStartInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
                m_motorStartInstance.start();
                m_motorStartInstance.release();

                m_motorInstance.start();
            }

            xVelocity *= -1.0f;
        }
        else
        {
            if (!isPlaying)
            {
                m_motorStartInstance = RuntimeManager.CreateInstance(m_motorStartRef);
                m_motorStartInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
                m_motorStartInstance.start();
                m_motorStartInstance.release();

                m_motorInstance.start();
            }
        }

        m_motorInstance.setParameterByID(m_motorVelocityID, xVelocity);
        m_motorInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
    }

    void updateYVelocity(Vector3 velocity)
    {
        float yVelocity = velocity.y * 6.0f;

        PLAYBACK_STATE playbackState;
        m_extentionInstance.getPlaybackState(out playbackState);
        bool isPlaying = playbackState != PLAYBACK_STATE.STOPPED;

        if (yVelocity > -0.005f && yVelocity < 0.005f)
        {
            if (isPlaying)
            {
                m_extentionInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
        }
        else if (yVelocity < 0)
        {
            if (!isPlaying)
            {
                m_extentionInstance.start();
            }

            yVelocity *= -1.0f;
        }
        else
        {
            if (!isPlaying)
            {
                m_extentionInstance.start();
            }
        }

        m_extentionInstance.setParameterByID(m_extentionVelocityID, yVelocity);
        m_extentionInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
    }
}
