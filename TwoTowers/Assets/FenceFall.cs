using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FenceFall : MonoBehaviour
{
    bool playedOnce = false;
    public float yPositionToggle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(transform.position.y < yPositionToggle && !playedOnce)
        {
            playedOnce = true;
            GetComponent<StudioEventEmitter>().Play();
        }
    }
}
