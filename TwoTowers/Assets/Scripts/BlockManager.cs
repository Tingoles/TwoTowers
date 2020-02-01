using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockManager : MonoBehaviour
{
    public List<GameObject> activeBlocks = new List<GameObject>();

    private bool calcHeights = false;

    public float topHeight;

    public float leftTopHeight = 0;
    public float rightTopHeight = 0;

    public TextMeshProUGUI winnerText;

    public GameObject leftWorldUI;
    public GameObject rightWorldUI;

    private void Update()
    {
        if (calcHeights)
        {
            leftTopHeight = 0;
            rightTopHeight = 0;

            foreach (GameObject block in activeBlocks)
            {
                Vector3 blockPos = block.transform.position;
                if (blockPos.x < 0)
                {
                    if (blockPos.y > leftTopHeight)
                    {
                        leftTopHeight = blockPos.y;
                        leftWorldUI.transform.parent = block.transform;
                        leftWorldUI.transform.localPosition = Vector3.zero;
                        float h = Mathf.Round(leftTopHeight * 100) / 10;
                        leftWorldUI.GetComponentInChildren<TextMeshProUGUI>().text = (h == Mathf.Round(h) ? h + ".0" : h + "") + "m";
                    }
                }
                else
                {
                    if (blockPos.y > rightTopHeight)
                    {
                        rightTopHeight = blockPos.y;
                        rightWorldUI.transform.parent = block.transform;
                        rightWorldUI.transform.localPosition = Vector3.zero;
                        float h = Mathf.Round(rightTopHeight * 100) / 10;
                        rightWorldUI.GetComponentInChildren<TextMeshProUGUI>().text = (h == Mathf.Round(h) ? h + ".0" : h + "") + "m";
                    }
                }
            }
        }
    }

    public void addBlocksToList(GameObject block)
    {
        activeBlocks.Add(block);
    }


    public void MeasureHeight(float delay)
    {
        calcHeights = true;
        leftWorldUI.SetActive(true);
        rightWorldUI.SetActive(true);
        StartCoroutine(FinalMeasurement(delay));
    }

    public IEnumerator FinalMeasurement(float delay)
    {
        yield return new WaitForSeconds(delay);

        calcHeights = false;

        foreach (GameObject block in activeBlocks)
        {
            float blockY = block.transform.position.y;
            if (blockY > topHeight)
            {
                topHeight = blockY;
            }
        }
        winnerText.text = "Winner! Height: " + Mathf.Round(topHeight * 100) / 10 + "m";
    }
}
