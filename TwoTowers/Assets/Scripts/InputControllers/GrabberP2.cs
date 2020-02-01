using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberP2 : GrabberInput
{
    // Update is called once per frame
    void Update()
    {
        GetInputs();
    }

    void GetInputs()
    {
        m_inputVec.x = Input.GetAxis("Horizontal");
        m_inputVec.y = Input.GetAxis("Vertical");
        m_grabInput = Input.GetKeyDown(KeyCode.Space);
    }
}
