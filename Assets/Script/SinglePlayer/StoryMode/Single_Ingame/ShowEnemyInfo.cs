using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEnemyInfo : MonoBehaviour
{
    public GameObject[] EnemyInfos;
    public Transform canvasTransform;  

    void Start()
    {
        StageGameManager stageGameManager = FindObjectOfType<StageGameManager>();
        int stageClearID = stageGameManager.StageClearID;

        foreach (GameObject enemyInfo in EnemyInfos)
        {
            if (enemyInfo != null)
            {
                string enemyInfoName = enemyInfo.name;
                if (enemyInfoName.Length >= 2)
                {
                    // 이름 뒤의 두 자리 숫자를 추출
                    string idString = enemyInfoName.Substring(enemyInfoName.Length - 2);

                    // 숫자로 변환
                    if (int.TryParse(idString, out int enemyID))
                    {
                        if (enemyID == stageClearID)
                        {
                            InstantiateEnemyInfo(enemyInfo);
                            break; // 일치하는 항목을 찾으면 루프 종료
                        }
                    }
                }
            }
        }
    }

    void InstantiateEnemyInfo(GameObject enemyInfo)
    {
        GameObject instantiatedInfo = Instantiate(enemyInfo, Vector3.zero, Quaternion.identity);
        instantiatedInfo.transform.SetParent(canvasTransform, false);

        RectTransform rectTransform = instantiatedInfo.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = Vector2.zero; 
        }
    }
}