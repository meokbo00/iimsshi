using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEnemyInfo : MonoBehaviour
{
    public GameObject[] EnemyInfos;
    public Transform canvasTransform;  // 캔버스의 Transform을 여기서 참조

    void Start()
    {
        StageGameManager stageGameManager = FindObjectOfType<StageGameManager>();

        switch (stageGameManager.StageClearID)
        {
            case 1:
                InstantiateEnemyInfo(EnemyInfos[0]);
                break;
            case 2:
                InstantiateEnemyInfo(EnemyInfos[1]);
                break;
            case 16:
                InstantiateEnemyInfo(EnemyInfos[2]);
                break;
        }
    }

    void InstantiateEnemyInfo(GameObject enemyInfo)
    {
        GameObject instantiatedInfo = Instantiate(enemyInfo, Vector3.zero, Quaternion.identity);

        instantiatedInfo.transform.SetParent(canvasTransform, false);

        RectTransform rectTransform = instantiatedInfo.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = Vector2.zero; // Use anchoredPosition for UI elements
        }
    }
}