using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameClock : MonoBehaviour
{
    [SerializeField]
    private float startingTime;

    public float timeRemaining;

    private bool countdown = true;

    private TextMeshProUGUI textMesh;

    public DelayCountdown delayCountdown;


    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        timeRemaining = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0)
            {
                timeRemaining = 0;
                countdown = false;
                float delay = 8.4f;
               GameObject.FindGameObjectWithTag("BlockManager").GetComponent<BlockManager>().MeasureHeight(delay);
                delayCountdown.timeRemaining = delay;
                delayCountdown.countdown = true;
            }
            textMesh.text = "Time Remaining: " + Mathf.RoundToInt(timeRemaining) + "s";
        }
    }
}
