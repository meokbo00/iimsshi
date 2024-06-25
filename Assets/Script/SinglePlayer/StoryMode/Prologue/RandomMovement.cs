using UnityEngine;

public class ContinuousRandomMovement : MonoBehaviour
{

    private Vector2 moveDirection; // 이동 방향

    void Start()
    {
        gameObject.transform.position = new Vector2(Random.Range(-400, 225), Random.Range(-300, 300));
        float randomAngle = Random.Range(0f, 360f);
        moveDirection = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));

        moveDirection.Normalize();
    }

    void Update()
    {
        transform.Translate(moveDirection * Random.Range(2, 17) * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        StageGameManager gameManager = FindObjectOfType<StageGameManager>();

        switch (collision.gameObject.name)
        {
            case "Bottom":
                if (gameManager.StageClearID < 6)
                {
                    transform.Translate(0, 470, 0);
                }
                transform.Translate(0, 170, 0);
                break;
            case "Top":
                if (gameManager.StageClearID < 6)
                {
                    transform.Translate(0, -470, 0);
                }
                transform.Translate(0, -170, 0);
                break;
            case "Left":
                if (gameManager.StageClearID < 6)
                {
                    transform.Translate(460, 0, 0);
                }
                transform.Translate(165, 0, 0);
                break;
            case "Right":
                if (gameManager.StageClearID < 6)
                {
                    transform.Translate(-460, 0, 0);
                }
                transform.Translate(-165, 0, 0);
                break;
        }
    }
}