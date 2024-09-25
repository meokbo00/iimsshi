using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class StageBallController : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 lastVelocity;
    float deceleration = 2f;
    public GameObject StageStart;
    private StageGameManager stageGameManager;
    private StageBallManager stageBallManager;
    // public GameObject LineBox;
    private bool hasbeenout = false;
    public static int chooseStage;
    StageGameManager gameManager = FindObjectOfType<StageGameManager>();

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
        else if (stageGameManager.StageClearID >= 7)
        {
            // 방금 저장한 Player의 위치값을 불러옴
            randomX = GlobalData.PlayerPosition.x;
            randomY = GlobalData.PlayerPosition.y;
        }

        gameObject.transform.position = new Vector3(randomX, randomY, gameObject.transform.position.z);
    }

    private void Update()
    {
        if (!stageBallManager.isDragging)
        {
            Debug.Log("StageBallController에서 클릭 뗀 것을 인지");
            rigid.velocity = StageBallManager.shotDirection * StageBallManager.shotDistance; // GameManager에서 값 가져와서 구체 발사
            stageBallManager.isDragging = true;
        }
        Move();
    }

    void Move()
    {
        lastVelocity = rigid.velocity;
        rigid.velocity -= rigid.velocity.normalized * deceleration * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TextManager textManager = FindObjectOfType<TextManager>();
        if (hasbeenout) return;
        if (collision.gameObject.tag == "StageDeadZone")
        {
            textManager.GiveMeTextId(1);
            Debug.Log("대화창이 활성화 되었습니다");
            hasbeenout = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "StageDeadZone")
        {
            rigid.velocity = Vector2.zero;
        }

        switch (collision.gameObject.name)
        {
            case "B":
                if (gameManager.StageClearID < 6)
                {
                    transform.Translate(0, 470, 0);
                }
                transform.Translate(0, 170, 0);
                Debug.Log("빠져나가지 못하도록 위치 조절합니다");
                break;
            case "T":
                if (gameManager.StageClearID < 6)
                {
                    transform.Translate(0, -470, 0);
                }
                transform.Translate(0, -170, 0);
                Debug.Log("빠져나가지 못하도록 위치 조절합니다");
                break;
            case "L":
                if (gameManager.StageClearID < 6)
                {
                    transform.Translate(460, 0, 0);
                }
                transform.Translate(165, 0, 0);
                Debug.Log("빠져나가지 못하도록 위치 조절합니다");
                break;
            case "R":
                if (gameManager.StageClearID < 6)
                {
                    transform.Translate(-460, 0, 0);
                }
                transform.Translate(-165, 0, 0);
                Debug.Log("빠져나가지 못하도록 위치 조절합니다");
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.contacts != null && coll.contacts.Length > 0)
        {
            Vector2 dir = Vector2.Reflect(lastVelocity.normalized, coll.contacts[0].normal);
            if (rigid != null)
                rigid.velocity = dir * Mathf.Max(lastVelocity.magnitude, 0f); // 감속하지 않고 반사만 진행
        }
    }
}
