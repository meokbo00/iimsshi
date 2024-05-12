using System.Collections;
using TMPro;
using UnityEngine;

public class StageBallController : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 lastVelocity;
    float deceleration = 2f;
    private bool isStopped = false;
    private bool hasBeenReleased = false;


    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && rigid != null && !hasBeenReleased)
        {
            rigid.velocity = StageGameManager.shotDirection * StageGameManager.shotDistance; // GameManager에서 값 가져와서 구체 발사
            hasBeenReleased = true; // 최초 클릭이 되었음을 표시
        }
        Move();
    }

    void Move()
    {
        if (rigid == null || isStopped) return;

        lastVelocity = rigid.velocity;
        rigid.velocity -= rigid.velocity.normalized * deceleration * Time.deltaTime;

    }
   
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if ((coll.gameObject.tag == "P1ball" || coll.gameObject.tag == "P2ball" || coll.gameObject.tag == "P1Item" || coll.gameObject.tag == "P2Item") && rigid == null)
        {

        }
        if (coll.contacts != null && coll.contacts.Length > 0)
        {
            Vector2 dir = Vector2.Reflect(lastVelocity.normalized, coll.contacts[0].normal);
            if (rigid != null)
                rigid.velocity = dir * Mathf.Max(lastVelocity.magnitude, 0f); // 감속하지 않고 반사만 진행
        }
    }
}

