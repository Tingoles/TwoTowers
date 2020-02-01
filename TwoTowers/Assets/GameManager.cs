using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> bulldozers = new List<GameObject>();

    public GameObject bomb;

    public GameClock gameClock;
    public BlockManager blockManager;

    public float timeTillBomb;

    private void Start()
    {
        StartCoroutine(blockBulldozerMechanics());
        timeTillBomb = Random.Range(20.0f, 30.0f);
    }

    private void Update()
    {
        timeTillBomb -= Time.deltaTime;
        if(timeTillBomb < 0)
        {
            GameObject obj = Instantiate(bomb);
            obj.transform.position = new Vector3(Random.Range(-8.0f, 8.0f), 12.0f);
            obj.GetComponent<BombScript>().fuseTimer = 12.0f;
            timeTillBomb = Random.Range(20.0f, 30.0f);
        }
    }

    IEnumerator blockBulldozerMechanics()
    {
        yield return new WaitForSeconds (13);
        foreach (GameObject bully in bulldozers)
        {
            bully.SetActive(true);

            bully.GetComponent<Bulldozer>().CreateCubes();
        }
        gameClock.countdown = true;
        blockManager.calcHeights = true;
    }

}
