using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonPop : MonoBehaviour, IPointerDownHandler
{
    bool wait = false;
    Vector3 scale = Vector3.one;
    Vector3 lerp = Vector3.zero;
    Vector3 lerpTraget = Vector3.zero;

    void Start()
    {
        scale = transform.localScale;
    }

    void Update()
    {
        if (wait)
            transform.localScale = scale + Vector3.Lerp(lerp, lerpTraget, Time.deltaTime * 10);
    }

    //OnPointerDown is also required to receive OnPointerUp callbacks
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!wait)
        {
            scale = transform.localScale;
            StartCoroutine(Pop());
        }
    }

    IEnumerator Pop()
    {
        wait = true;
        lerpTraget = scale * -0.2f;
        yield return new WaitForSeconds(0.2f);
        lerpTraget = Vector3.zero;
        yield return new WaitForSeconds(0.2f);
        wait = false;
    }
}
