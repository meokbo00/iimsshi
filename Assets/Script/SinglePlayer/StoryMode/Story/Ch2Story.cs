using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch2Story : MonoBehaviour
{
    public Camera Camera;
    public GameObject Fadein;
    StageGameManager stageGameManager;
    StageBallController stageBallController;
    TextManager textManager;
    ShowText showText;

    void Start()
    {
        showText = FindObjectOfType<ShowText>();
        textManager = FindObjectOfType<TextManager>();
        stageGameManager = FindObjectOfType<StageGameManager>();
        stageBallController = FindObjectOfType<StageBallController>();

        if (stageGameManager.StageClearID == 6)
        {
            textManager.GiveMeTextId(1);
            Fadein.SetActive(true);
            Camera.orthographicSize = 150; 

            if (stageBallController != null)
            {
                stageBallController.enabled = false;
            }
        }
        if(stageGameManager.StageClearID == 7)
        {
            textManager.GiveMeTextId(2);
        }

        if(stageGameManager.StageClearID == 16)
        {
            textManager.GiveMeTextId(3);
        }
    }

    void Update()
    {
        showText = FindObjectOfType<ShowText>();
        if (showText != null && stageGameManager.StageClearID == 6)
        {
            if (showText.logTextIndex == 3)
            {
                Fadein.SetActive(false);
            }
            if (showText.logTextIndex == 4)
            {
                StartCoroutine(ChangeCameraSize(150, 4, 5f)); // 2초에 걸쳐 카메라 사이즈 변경
            }
            if (showText.logTextIndex == 36)
            {
                stageBallController.enabled = true;
            }
        }
    }

    IEnumerator ChangeCameraSize(float fromSize, float toSize, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            Camera.orthographicSize = Mathf.Lerp(fromSize, toSize, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Camera.orthographicSize = toSize;
    }
}
