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
        m_inputVec.x = Input.GetAxis("HorizontalController2");
        m_inputVec.y = Input.GetAxis("VerticalController2");
        m_rightStickHorizontal = Input.GetAxis("HorizontalRightStick2");
        m_grabInput = Input.GetButtonDown("Grab2");
    }
}
