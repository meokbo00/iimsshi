using UnityEngine;

public class ContinuousRandomMovement : MonoBehaviour
{
    private Vector2 moveDirection;
    private float moveSpeed;
    private StageGameManager gameManager;
    private const float OFFSET = 100f;
    private const float COLLISION_OFFSET = 150f;

    void Start()
    {
        transform.position = new Vector2(Random.Range(-170f, 20f), Random.Range(-110f, 90f));
        float randomAngle = Random.Range(0f, 360f);
        moveDirection = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));
        moveDirection.Normalize();

        moveSpeed = Random.Range(2f, 17f);

        gameManager = FindObjectOfType<StageGameManager>();
    }

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (gameManager == null) return;

        Vector3 offset = Vector3.zero;

        switch (collision.gameObject.name)
        {
            case "Bottom":
                offset = (gameManager.StageClearID < 7) ? new Vector3(0, COLLISION_OFFSET + OFFSET, 0) : new Vector3(0, OFFSET, 0);
                break;
            case "Top":
                offset = (gameManager.StageClearID < 7) ? new Vector3(0, -COLLISION_OFFSET - OFFSET, 0) : new Vector3(0, -OFFSET, 0);
                break;
            case "Left":
                offset = (gameManager.StageClearID < 7) ? new Vector3(COLLISION_OFFSET + OFFSET, 0, 0) : new Vector3(OFFSET, 0, 0);
                break;
            case "Right":
                offset = (gameManager.StageClearID < 7) ? new Vector3(-COLLISION_OFFSET - OFFSET, 0, 0) : new Vector3(-OFFSET, 0, 0);
                break;
        }

        if (offset != Vector3.zero)
        {
            transform.Translate(offset);
        }
    }
}
