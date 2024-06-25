using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ch1Story : MonoBehaviour
{
    public Image[] Tutorial;
    private int currentIndex = 0;

    void Start()
    {
       
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ActivateNextTutorial();
        }
    }

    void ActivateNextTutorial()
    {
        if (currentIndex >= 0 && currentIndex < Tutorial.Length)
        {
            Tutorial[currentIndex].gameObject.SetActive(false);
        }

        currentIndex++;

        if (currentIndex < Tutorial.Length)
        {
            Tutorial[currentIndex].gameObject.SetActive(true);
        }
        if (currentIndex >= Tutorial.Length)
        {

        }
    }
}