using UnityEngine;

public class ContinuousRandomMovement : MonoBehaviour
{

    private Vector2 moveDirection; // 이동 방향

    void Start()
    {
        gameObject.transform.position = new Vector2(Random.Range(-199, 199), Random.Range(-190, 190));
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
                if (gameManager.StageClearID < 7)
                {
                    transform.Translate(0, 210, 0);
                }
                transform.Translate(0, 170, 0);
                break;
            case "Top":
                if (gameManager.StageClearID < 7)
                {
                    transform.Translate(0, -210, 0);
                }
                transform.Translate(0, -170, 0);
                break;
            case "Left":
                if (gameManager.StageClearID < 7)
                {
                    transform.Translate(200, 0, 0);
                }
                transform.Translate(165, 0, 0);
                break;
            case "Right":
                if (gameManager.StageClearID < 7)
                {
                    transform.Translate(-200, 0, 0);
                }
                transform.Translate(-165, 0, 0);
                break;
        }
    }
}