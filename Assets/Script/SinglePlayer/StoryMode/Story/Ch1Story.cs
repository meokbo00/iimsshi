using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ch1Story : MonoBehaviour
{
    public GameObject Clearhere;
    private ShowText showText;
    private StageGameManager stageGameManager;

    void Start()
    {
        stageGameManager = FindObjectOfType<StageGameManager>();
        TextManager textManager = FindObjectOfType<TextManager>();

        showText = FindObjectOfType<ShowText>();
        if (showText != null)
        {
            showText.OnChatComplete.AddListener(OnChatCompleteHandler);
        }

        if (stageGameManager.StageClearID == 1)
        {
            textManager.GiveMeTextId(1);
        }
        if (stageGameManager.StageClearID == 2)
        {
            textManager.GiveMeTextId(2);
        }
    }

    void Update()
    {
        showText = FindObjectOfType<ShowText>();

        if (showText != null && stageGameManager.StageClearID == 1)
        {
            // logTextIndex 값이 25일 때 로그 출력
            if (showText.logTextIndex == 26 || showText.logTextIndex == 27 || showText.logTextIndex == 28)
            {
                Clearhere.gameObject.SetActive(true);
            }
            if(showText.logTextIndex >= 27)
            {
                Clearhere.gameObject.SetActive(false);
            }
        }
    }

    private void OnChatCompleteHandler(int chatId)
    {
        if (chatId == 1)
        {
            Debug.Log("ID 1의 채팅이 완료되었습니다.");
        }
    }
}