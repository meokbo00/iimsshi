using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExBallController : MonoBehaviour
{
    SPGameManager spgamemanager;
    Rigidbody2D rb;
    bool hasBeenLaunched = false;
    bool isExpanding = false; // 공이 팽창 중인지 여부를 추적합니다.
    bool isStopped = false; // 공이 완전히 멈췄는지 여부
    private float decelerationThreshold = 0.4f;
    private float dragAmount = 1.1f;
    private float expandSpeed = 0.5f; // 팽창 속도
    private Vector3 initialScale; // 초기 공 크기
    private Vector3 targetScale; // 목표 크기
    public PhysicsMaterial2D bouncyMaterial; // 공의 Collider2D에 적용할 물리 재질

    void Start()
    {
        spgamemanager = FindAnyObjectByType<SPGameManager>();
        rb = GetComponent<Rigidbody2D>();

        rb.drag = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        // 물리 재질이 설정된 경우 Collider2D에 적용
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null && bouncyMaterial != null)
        {
            collider.sharedMaterial = bouncyMaterial;
        }

        initialScale = transform.localScale; // 초기 공 크기 저장
    }

    void Update()
    {
        if (!hasBeenLaunched && !spgamemanager.isDragging)
        {
            LaunchBall();
        }

        if (hasBeenLaunched)
        {
            SlowDownBall();
            if (isExpanding)
            {
                // 부드럽게 팽창
                transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * expandSpeed);

                // 팽창 목표 크기에 도달하면 멈춤
                if (Vector3.Distance(transform.localScale, targetScale) < 0.01f)
                {
                    transform.localScale = targetScale;
                    isExpanding = false;
                }
            }
        }
    }

    void LaunchBall()
    {
        Vector2 launchForce = SPGameManager.shotDirection * SPGameManager.shotDistance;
        rb.AddForce(launchForce, ForceMode2D.Impulse);

        rb.drag = dragAmount;
        hasBeenLaunched = true;
    }

    void SlowDownBall()
    {
        if (rb == null) return;
        if (rb.velocity.magnitude <= decelerationThreshold && !isStopped)
        {
            rb.velocity = Vector2.zero; // 속도를 0으로 설정하여 공을 정지시킴
            isStopped = true;

            // 팽창 시작
            targetScale = initialScale * 10f; // 초기 크기의 10배로 설정
            isExpanding = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.isTrigger && isExpanding)
        {
            // 충돌 시 팽창을 멈추고 크기 고정
            isExpanding = false;
            transform.localScale = transform.localScale; // 현재 크기에서 멈춤
            Destroy(rb);
        }
    }
}