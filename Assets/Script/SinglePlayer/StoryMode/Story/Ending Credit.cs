using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCredit : MonoBehaviour
{
    public GameObject endingcredit; // ĵ���� �ȿ� �� ������Ʈ ���
    public float scrollSpeed = 20f; // ��ũ�� �ӵ�

    void Update()
    {
        // �� �������� õõ�� �����̰� ����
        endingcredit.transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
    }
}
