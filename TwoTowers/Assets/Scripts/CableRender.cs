using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableRender : MonoBehaviour
{
    LineRenderer m_lineRenderer;
    public List<GameObject> m_nodes;
    // Start is called before the first frame update
    void Start()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
        m_lineRenderer.positionCount = m_nodes.Count;
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach (GameObject obj in m_nodes)
        {
            m_lineRenderer.SetPosition(i, m_nodes[i].transform.position);
            i++;
        }
    }
}
