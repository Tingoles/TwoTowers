using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBulldozer : MonoBehaviour
{
    [SerializeField]
    float moveTime = 7;
    [SerializeField]
    float moveDistance = 80;

    [SerializeField]
    private ParticleSystem shovedFX;
    [SerializeField]
    private ParticleSystem driveFX;

    public List<GameObject> bulldozers = new List<GameObject>();

    private Vector3 startPos;

    private void Start()
    {
        startPos = this.transform.position;
        StartCoroutine(WipeStage());
    }

    public IEnumerator WipeStage() 
    {
        yield return new WaitForSeconds(14);
        Vector3 targetPos = this.transform.position + new Vector3(moveDistance, 0);

        driveFX.Play();
        for (float t = 0; t < moveTime; t += Time.deltaTime)
        {
            float tl = t / moveTime;
            transform.position = Vector3.Lerp(startPos, targetPos, tl);
            yield return null;
        }
        driveFX.Stop();

        shovedFX.Play();
        foreach(GameObject bully in bulldozers)
        {
            bully.SetActive(true);

            bully.GetComponent<Bulldozer>().CreateCubes();
        }
    }
}
