﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FMODUnity;

public class GameClock : MonoBehaviour
{
    [SerializeField]
    private float startingTime;

    public float timeRemaining;

    public bool countdown = true;

    private TextMeshProUGUI textMesh;

    public DelayCountdown delayCountdown;


    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        timeRemaining = startingTime;
        textMesh.text = "Time Remaining: " + Mathf.RoundToInt(timeRemaining);
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0)
            {
                DisableControls();
                timeRemaining = 0;
                countdown = false;
                float delay = 8.4f;
               GameObject.FindGameObjectWithTag("BlockManager").GetComponent<BlockManager>().MeasureHeight(delay);
                delayCountdown.timeRemaining = delay;
                delayCountdown.countdown = true;
            }
            else if (timeRemaining < 2)
            {
                Camera.main.GetComponent<StudioEventEmitter>().SetParameter("GameOver", 1.0f);
            }
            textMesh.text = "Time Remaining: " + Mathf.RoundToInt(timeRemaining);
        }
    }

    void DisableControls()
    {
        foreach (GrabberController control in FindObjectsOfType<GrabberController>())
        {
            control.stopMotors();
            control.Drop();
            control.m_active = false;
        }
    }
}
