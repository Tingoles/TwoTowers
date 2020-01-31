using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bulldozer : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 targetPos;

    [SerializeField]
    private bool moveLeft;
    [SerializeField]
    private bool moveRight;

    [SerializeField]
    private float moveDis;

    [SerializeField]
    private float moveForwardTime = 3.0f;
    [SerializeField]
    private float reverseDelayTime = 0.75f;
    [SerializeField]
    private float moveBackTime = 2.25f;

    [SerializeField]
    Transform BlockSpawner;

    [SerializeField]
    private List<GameObject> debries;

    private bool spawning = false;
    private bool inProgress = false;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        if(moveLeft)
        {
            targetPos = startPos + new Vector3(-moveDis, 0);
        }
        else if(moveRight)
        {
            targetPos = startPos + new Vector3(moveDis, 0);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown("space") && inProgress == false)
        {
            StartCoroutine(SpawnDebries(5));
            StartCoroutine(Move(moveForwardTime, reverseDelayTime, moveBackTime));
        }
    }

    IEnumerator Move(float forwardTime, float reverseDelay, float reverseTime)
    {
        while(spawning == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        for(float t = 0; t < forwardTime; t += Time.deltaTime)
        {
            float tl = t / forwardTime;
            transform.position = Vector3.Lerp(startPos, targetPos, tl);
            yield return null;
        }

        yield return new WaitForSeconds(reverseDelay);


        for (float t = 0; t < reverseTime; t += Time.deltaTime)
        {
            float tl = t / reverseTime;
            transform.position = Vector3.Lerp(targetPos, startPos, tl);
            yield return null;
        }
        inProgress = false;
    }

    public IEnumerator SpawnDebries(int num)
    {
        inProgress = true;
        spawning = true;
        for (int i = 0; i < num; i++)
        {
            GameObject newDebrie = Instantiate(debries[Random.Range(0, debries.Count)]);
            newDebrie.transform.position = BlockSpawner.position + new Vector3(Random.Range(-0.1f, 0.1f), 0);
            yield return new WaitForSeconds(0.3f);
        }
        spawning = false;
    }
}
