using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberP1 : GrabberInput
{
    // Update is called once per frame
    void Update()
    {
        GetInputs();
    }

    void GetInputs()
    {
        m_inputVec.x = Input.GetAxis("HorizontalController");
        m_inputVec.y = Input.GetAxis("VerticalController");
        m_grabInput = Input.GetButtonDown("Grab1");
    }
}
