using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuButton
{
    Start,
    Quit
}

public class Menu : MonoBehaviour
{
    public List<SpriteRenderer> icons;
    public GameObject bomb;
    public GameObject bulldozer;

    private int buttonHovered = 0;
    private bool doOnceLeft = true;
    private bool doOnceRight = true;
    private bool buttonPushed = false;

    // Update is called once per frame
    void Update()
    {
        if (!buttonPushed)
        {
            Controls();
        }

        for (int i = 0; i < icons.Count; i++)
        {
            if (i == buttonHovered)
            {
                icons[i].color = Color.green;
            }
            else
            {
                icons[i].color = Color.white;
            }
        }
    }

    private void Controls()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            if (doOnceRight)
            {
                buttonHovered++;
                doOnceRight = false;
            }
        }
        else
        {
            doOnceRight = true;
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            if (doOnceLeft)
            {
                buttonHovered++;
                doOnceLeft = false;
            }
        }
        else
        {
            doOnceLeft = true;
        }


        if (buttonHovered > icons.Count - 1)
        {
            buttonHovered = 0;
        }

        if (Input.GetButtonDown("Grab1"))
        {
            buttonPushed = true;
            if(buttonHovered == 1)
            {
                StartCoroutine(QuitGame());
            }
            else
            {
                StartCoroutine(StartTruck());
            }
        }
    }

    IEnumerator QuitGame()
    {
        Instantiate<GameObject>(bomb);
        yield return new WaitForSeconds(4.0f);
        Application.Quit();
    }

    IEnumerator StartTruck()
    {
        //transition
        bulldozer.GetComponent<LerpTowards>().start = true;
        yield return new WaitForSeconds(3.0f);
        FindObjectOfType<Transition>().LoadScene("Menu");

    }
}
