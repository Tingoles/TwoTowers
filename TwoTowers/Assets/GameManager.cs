using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> bulldozers = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(blockBulldozerMechanics());
    }

    IEnumerator blockBulldozerMechanics()
    {
        yield return new WaitForSeconds (13);
        foreach (GameObject bully in bulldozers)
        {
            bully.SetActive(true);

            bully.GetComponent<Bulldozer>().CreateCubes();
        }
    }

}
