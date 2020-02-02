using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockManager : MonoBehaviour
{
    public List<GameObject> activeBlocks = new List<GameObject>();

    public bool calcHeights = false;

    public float topHeight;

    public float leftTopHeight = 0;
    public float rightTopHeight = 0;

    public TextMeshProUGUI winnerText;

    public GameObject leftWorldUI;
    public GameObject rightWorldUI;

    public GameObject leftBulldozer;
    public GameObject rightBulldozer;

    private void Update()
    {
        if (calcHeights)
        {
            leftTopHeight = 0;
            rightTopHeight = 0;

            foreach (GameObject block in activeBlocks)
            {
                Vector3 blockPos = block.transform.position;
                if (blockPos.x < 0 && blockPos.x > -19.5)
                {
                    if (blockPos.y > leftTopHeight)
                    {
                        leftTopHeight = blockPos.y;
                        leftWorldUI.transform.position = block.transform.position;
                        //leftWorldUI.transform.localPosition = Vector3.zero;
                        float h = Mathf.Round(leftTopHeight * 100) / 10;
                        leftWorldUI.GetComponentInChildren<TextMeshProUGUI>().text = (h == Mathf.Round(h) ? h + ".0" : h + "") + "m";
                    }
                }
                else if (blockPos.x < 19.5)
                {
                    if (blockPos.y > rightTopHeight)
                    {
                        rightTopHeight = blockPos.y;
                        rightWorldUI.transform.position = block.transform.position;
                        //rightWorldUI.transform.localPosition = Vector3.zero;
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

        if (leftTopHeight < rightTopHeight)
        {
            winnerText.text = "Blue Winner!\nWinning Height: " + Mathf.Round(topHeight * 100) / 10 + "m";
            winnerText.color = Color.blue;
        }
        else
        {
            winnerText.text = "Red Winner!\nWinning Height: " + Mathf.Round(topHeight * 100) / 10 + "m";
            winnerText.color = Color.red;
        }
        yield return new WaitForSeconds(10.0f);
        FindObjectOfType<Transition>().LoadScene("Menu");
    }
}
