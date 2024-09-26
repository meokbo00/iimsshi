using UnityEngine;

public class ContinuousRandomMovement : MonoBehaviour
{
    private Vector2 moveDirection;  
    private StageGameManager gameManager;
    private Rigidbody2D rb;       
    public float moveSpeed;       
    public PhysicsMaterial2D bouncyMaterial;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindAnyObjectByType<StageGameManager>();
        gameObject.transform.position = new Vector2(Random.Range(-199, 199), Random.Range(-190, 190));
        float randomAngle = Random.Range(0f, 360f);
        moveDirection = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));
        moveDirection.Normalize();  // 방향 벡터를 정규화
        moveSpeed = Random.Range(2, 17);

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null && bouncyMaterial != null)
        {
            collider.sharedMaterial = bouncyMaterial;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "Bottom":
                if (gameManager.StageClearID < 7)
                {
                    rb.position += new Vector2(0, 210); // StageClearID 7 미만일 경우 큰 이동
                }
                else
                {
                    rb.position += new Vector2(0, 170); // 그 외는 작은 이동
                }
                break;

            case "Top":
                if (gameManager.StageClearID < 7)
                {
                    rb.position += new Vector2(0, -210);
                }
                else
                {
                    rb.position += new Vector2(0, -170);
                }
                break;

            case "Left":
                if (gameManager.StageClearID < 7)
                {
                    rb.position += new Vector2(200, 0);
                }
                else
                {
                    rb.position += new Vector2(165, 0);
                }
                break;

            case "Right":
                if (gameManager.StageClearID < 7)
                {
                    rb.position += new Vector2(-200, 0);
                }
                else
                {
                    rb.position += new Vector2(-165, 0);
                }
                break;
        }
    }
}
