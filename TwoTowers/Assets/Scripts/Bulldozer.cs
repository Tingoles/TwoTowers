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

    public GameObject blockPoolManager;

    [SerializeField]
    private List<GameObject> debris;

    private bool spawning = false;
    private bool inProgress = false;

    public int numBlocksToSpawn = 4;

    [SerializeField]
    private ParticleSystem shovedFX;
    [SerializeField]
    private ParticleSystem driveFX;

    private float blockSpawnDelay = 10;
    private float blockSpawnTimer = 0;

    private Animator animator;

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

        animator = GetComponent<Animator>();

        animator.SetFloat("Speed", 0.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (blockSpawnTimer >= blockSpawnDelay && blockPoolManager.GetComponent<BlockManager>().activeBlocks.Count < 30)
        {
            StartCoroutine(SpawnDebris(numBlocksToSpawn));
            StartCoroutine(Move(moveForwardTime, reverseDelayTime, moveBackTime));
            blockSpawnTimer = 0; 
        }
        blockSpawnTimer += Time.deltaTime;

    }

    public void CreateCubes()
    {
        StartCoroutine(SpawnDebris(5));
        StartCoroutine(Move(moveForwardTime, reverseDelayTime, moveBackTime));
    }

    IEnumerator Move(float forwardTime, float reverseDelay, float reverseTime)
    {
        while(spawning == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.5f);
        driveFX.Play();
        animator.SetFloat("Speed", 1.0f);
        for (float t = 0; t < forwardTime; t += Time.deltaTime)
        {
            float tl = (t / forwardTime)*0.2f;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, tl);
            yield return null;
        }
        animator.SetFloat("Speed", 0.0f);

        driveFX.Stop();
        shovedFX.Play();

        yield return new WaitForSeconds(reverseDelay);

        animator.SetFloat("Speed", -1.0f);
        for (float t = 0; t < reverseTime; t += Time.deltaTime)
        {
            float tl = t / reverseTime;
            transform.position = Vector3.Lerp(targetPos, startPos, tl);
            yield return null;
        }
        animator.SetFloat("Speed", 0.0f);
        inProgress = false;
    }

    public IEnumerator SpawnDebris(int num)
    {
        inProgress = true;
        spawning = true;
        int spawnOffset = -2;
        for (int i = 0; i < num; i++)
        {
            if (spawnOffset == 2)
            {
                spawnOffset = -2;
            }
            GameObject newDebris = Instantiate(debris[Random.Range(0, debris.Count)]);
            newDebris.transform.position = BlockSpawner.position + new Vector3(Random.Range(-0.1f, 0.1f) + spawnOffset, newDebris.transform.position.y +( (i*2) +2));
            newDebris.transform.parent = blockPoolManager.transform;
            blockPoolManager.GetComponent<BlockManager>().activeBlocks.Add(newDebris);

            yield return new WaitForSeconds(0.3f);
            spawnOffset += 2;
        }
        spawning = false;
    }
}
