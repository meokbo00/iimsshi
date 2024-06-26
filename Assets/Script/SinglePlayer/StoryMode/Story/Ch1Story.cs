using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ch1Story : MonoBehaviour
{

    void Start()
    {
        StageGameManager stageGameManager = FindObjectOfType<StageGameManager>();
        TextManager textManager = FindObjectOfType<TextManager>();
        if (stageGameManager.StageClearID == 1)
        {
            textManager.GiveMeTextId(1);
        }
    }

    void Update()
    {
    }
}