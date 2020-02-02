using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FMODUnity;

public class DelayCountdown : MonoBehaviour
{
    public float timeRemaining;
    public bool countdown = true;
    private TextMeshProUGUI textMesh;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (countdown)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0)
            {
                timeRemaining = 0;
                countdown = false;
                textMesh.text = "";
            }
            else
            {
                textMesh.text = (Mathf.RoundToInt(timeRemaining)).ToString();
            }
        }
    }
}