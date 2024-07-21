using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 요소를 사용하기 위해 추가

public class ShowTutorial : MonoBehaviour
{
    public GameObject[] FirstTutorial;
    public GameObject[] SecondTutorial;
    public Button nextButton; // UI 버튼을 참조
    private GameObject[] currentTutorial;
    private int currentIndex;

    void Start()
    {
        StageGameManager stageGameManager = FindObjectOfType<StageGameManager>();

        if (stageGameManager != null)
        {
            if (stageGameManager.StageClearID == 1)
            {
                currentTutorial = FirstTutorial;
            }
            else if (stageGameManager.StageClearID == 7)
            {
                currentTutorial = SecondTutorial;
            }

            if (currentTutorial != null && currentTutorial.Length > 0)
            {
                ActivateTutorialElement(0); // 첫 번째 요소를 활성화
                currentIndex = 0;
                nextButton.onClick.AddListener(OnNextButtonClicked); // 버튼 클릭 이벤트 추가
            }
        }
    }

    void ActivateTutorialElement(int index)
    {
        for (int i = 0; i < currentTutorial.Length; i++)
        {
            currentTutorial[i].SetActive(i == index); // 해당 인덱스만 활성화
        }
    }

    void OnNextButtonClicked()
    {
        currentIndex++;

        if (currentIndex >= currentTutorial.Length)
        {
            // 모든 요소 비활성화
            foreach (GameObject obj in currentTutorial)
            {
                obj.SetActive(false);
            }
            nextButton.onClick.RemoveListener(OnNextButtonClicked); // 버튼 클릭 이벤트 제거
        }
        else
        {
            ActivateTutorialElement(currentIndex);
        }
    }
}
