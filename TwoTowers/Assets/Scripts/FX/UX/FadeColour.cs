using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Fade
{
    In,
    Out
}

public class FadeColour : MonoBehaviour
{
    public Fade fade;
    public float speed = 1;

    private Image bg;

    private void Start()
    {
        bg = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        float target = 0;
        switch(fade)
        {
            case Fade.In:
                {
                    target = 1;
                    break;
                }
            case Fade.Out:
                {
                    target = 0;
                    break;
                }
        }

        bg.color = new Color(bg.color.r, bg.color.g, bg.color.b, Mathf.Lerp(bg.color.a, target, Time.deltaTime * speed));
    }
}
