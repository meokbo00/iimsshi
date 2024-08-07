using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class StageBallController : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Vector2 lastVelocity;
    private float deceleration = 2f;
    public GameObject StageStart;
    private StageGameManager stageGameManager;
    private StageBallManager stageBallManager;
    private bool hasBeenOut = false;
    public static int chooseStage;

    private float randomX;
    private float randomY;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        stageBallManager = FindObjectOfType<StageBallManager>();
        stageGameManager = FindObjectOfType<StageGameManager>();

        if (stageGameManager.StageClearID <= 6)
        {
            randomX = 0;
            randomY = -5;
        }
        else
        {
            randomX = GlobalData.PlayerPosition.x;
            randomY = GlobalData.PlayerPosition.y;
        }

        transform.position = new Vector3(randomX, randomY, transform.position.z);
    }

    private void Update()
    {
        if (!stageBallManager.isDragging)
        {
            FireBall();
        }
        else
        {
            Move();
        }
    }

    private void FireBall()
    {
        Debug.Log("StageBallController에서 클릭 뗀 것을 인지");
        rigid.velocity = StageBallManager.shotDirection * StageBallManager.shotDistance;
        stageBallManager.isDragging = true;
    }

    private void Move()
    {
        lastVelocity = rigid.velocity;
        rigid.velocity -= rigid.velocity.normalized * deceleration * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasBeenOut) return;

        if (collision.CompareTag("StageDeadZone"))
        {
            FindObjectOfType<TextManager>().GiveMeTextId(1);
            Debug.Log("대화창이 활성화 되었습니다");
            hasBeenOut = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        StageGameManager gameManager = FindObjectOfType<StageGameManager>();
        if (collision.CompareTag("StageDeadZone"))
        {
            rigid.velocity = Vector2.zero;
        }
        else
        {
            AdjustPositionBasedOnCollider(collision, gameManager);
        }
    }

    private void AdjustPositionBasedOnCollider(Collider2D collision, StageGameManager gameManager)
    {
        Vector3 offset = Vector3.zero;
        switch (collision.gameObject.name)
        {
            case "B":
                offset = (gameManager.StageClearID < 6) ? new Vector3(0, 370, 0) : new Vector3(0, 170, 0);
                break;
            case "T":
                offset = (gameManager.StageClearID < 6) ? new Vector3(0, -370, 0) : new Vector3(0, -170, 0);
                break;
            case "L":
                offset = (gameManager.StageClearID < 6) ? new Vector3(360, 0, 0) : new Vector3(165, 0, 0);
                break;
            case "R":
                offset = (gameManager.StageClearID < 6) ? new Vector3(-360, 0, 0) : new Vector3(-165, 0, 0);
                break;
        }

        if (offset != Vector3.zero)
        {
            transform.Translate(offset);
            Debug.Log("빠져나가지 못하도록 위치 조절합니다");
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.contacts != null && coll.contacts.Length > 0)
        {
            Vector2 dir = Vector2.Reflect(lastVelocity.normalized, coll.contacts[0].normal);
            if (rigid != null)
                rigid.velocity = dir * Mathf.Max(lastVelocity.magnitude, 0f);
        }
    }
}
