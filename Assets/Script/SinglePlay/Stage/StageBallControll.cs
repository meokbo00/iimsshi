using System.Collections;
using TMPro;
<<<<<<< HEAD
using Unity.VisualScripting;
=======
>>>>>>> 6fcf64348e0c59a6ac983e94112182f58387eb06
using UnityEngine;

public class StageBallController : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 lastVelocity;
    float deceleration = 2f;
<<<<<<< HEAD
    public GameObject StageStart;
    private StageGameManager stageGameManager;
=======
    private bool isStopped = false;
    private bool hasBeenReleased = false;

>>>>>>> 6fcf64348e0c59a6ac983e94112182f58387eb06

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
<<<<<<< HEAD
        stageGameManager = FindObjectOfType<StageGameManager>();
=======
>>>>>>> 6fcf64348e0c59a6ac983e94112182f58387eb06
    }

    private void Update()
    {
<<<<<<< HEAD
        if (!stageGameManager.isDragging)
        {
            Debug.Log("StageBallContrller에서 클릭 뗀것을 인지");
            rigid.velocity = StageGameManager.shotDirection * StageGameManager.shotDistance; // GameManager에서 값 가져와서 구체 발사
            stageGameManager.isDragging = true;
=======
        if (Input.GetMouseButtonUp(0) && rigid != null && !hasBeenReleased)
        {
            rigid.velocity = StageGameManager.shotDirection * StageGameManager.shotDistance; // GameManager에서 값 가져와서 구체 발사
            hasBeenReleased = true; // 최초 클릭이 되었음을 표시
>>>>>>> 6fcf64348e0c59a6ac983e94112182f58387eb06
        }
        Move();
    }

    void Move()
    {
<<<<<<< HEAD
        lastVelocity = rigid.velocity;
        rigid.velocity -= rigid.velocity.normalized * deceleration * Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stage")
        {
            StageStart.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        StageStart.gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
=======
        if (rigid == null || isStopped) return;

        lastVelocity = rigid.velocity;
        rigid.velocity -= rigid.velocity.normalized * deceleration * Time.deltaTime;

    }
   
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if ((coll.gameObject.tag == "P1ball" || coll.gameObject.tag == "P2ball" || coll.gameObject.tag == "P1Item" || coll.gameObject.tag == "P2Item") && rigid == null)
        {

        }
>>>>>>> 6fcf64348e0c59a6ac983e94112182f58387eb06
        if (coll.contacts != null && coll.contacts.Length > 0)
        {
            Vector2 dir = Vector2.Reflect(lastVelocity.normalized, coll.contacts[0].normal);
            if (rigid != null)
                rigid.velocity = dir * Mathf.Max(lastVelocity.magnitude, 0f); // 감속하지 않고 반사만 진행
        }
    }
}

