using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStarMove : MonoBehaviour
{
    public bool isturn = false;
    public float rotationSpeed = 1f;
    public float radius = 5f;
    public float initialAngle;

    private Vector3 initialPosition;
    private float angle = 0f;

    void Start()
    {
        initialPosition = transform.position; // 초기 위치 저장
        initialAngle = Random.Range(0f, 360f); // 초기 각도를 랜덤으로 설정
        angle = initialAngle; // 초기 각도로 설정
    }
    void Update()
    {
        if (isturn)
        {
            // 원을 그리며 이동
            angle += rotationSpeed * Time.deltaTime;
            if (angle > 360f) angle -= 360f; // 각도가 360도를 넘지 않도록 조정

            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            transform.position = initialPosition + new Vector3(x, y, 0f);
        }
    }
}