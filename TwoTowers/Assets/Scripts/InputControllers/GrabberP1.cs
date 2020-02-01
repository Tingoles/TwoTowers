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
        m_inputVec.x = Input.GetAxis("HorizontalController") + Input.GetAxis("Horizontal");
        m_inputVec.y = Input.GetAxis("VerticalController") + Input.GetAxis("Vetical");
        m_grabInput = Input.GetButtonDown("Grab1") || Input.GetKeyDown(KeyCode.Space);
    }
}
