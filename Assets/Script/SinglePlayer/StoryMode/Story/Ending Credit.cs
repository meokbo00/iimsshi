using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCredit : MonoBehaviour
{
    public GameObject endingcredit; // 캔버스 안에 들어간 오브젝트 요소
    public float scrollSpeed = 20f; // 스크롤 속도

    void Update()
    {
        // 위 방향으로 천천히 움직이게 설정
        endingcredit.transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
    }
}
