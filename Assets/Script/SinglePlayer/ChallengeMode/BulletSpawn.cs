using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    public GameObject[] bullets; // Bullet 프리팹 배열
    public float spawnInterval = 10f; // 초기 스폰 간격 (초)
    public float minimumSpawnInterval = 1f; // 최소 스폰 간격 (초)
    public float minForce = 1.7f; // 최소 힘의 세기
    public float maxForce = 7f; // 최대 힘의 세기

    private BoxCollider2D backgroundCollider;
    private float timer;

    void Start()
    {
        // BackGround 오브젝트의 BoxCollider2D를 찾습니다.
        GameObject background = GameObject.Find("BackGround");
        if (background != null)
        {
            backgroundCollider = background.GetComponent<BoxCollider2D>();
        }
        else
        {
            Debug.LogError("BackGround 오브젝트를 찾을 수 없습니다.");
        }

        // 타이머 초기화
        timer = spawnInterval;
    }

    void Update()
    {
        // 타이머를 감소시킵니다.
        timer -= Time.deltaTime;

        // 타이머가 0 이하가 되면 총알을 생성합니다.
        if (timer <= 0f)
        {
            SpawnBullet();
            // 생성 간격을 줄입니다.
            spawnInterval = Mathf.Max(minimumSpawnInterval, spawnInterval - 0.5f);
            timer = spawnInterval; // 타이머 리셋
        }
    }

    void SpawnBullet()
    {
        if (bullets.Length == 0 || backgroundCollider == null)
        {
            Debug.LogWarning("스폰할 총알 프리팹이 없거나 BackGround Collider가 없습니다.");
            return;
        }

        // BackGround의 콜라이더 영역 내에서 랜덤 위치를 생성합니다.
        float x = Random.Range(backgroundCollider.bounds.min.x, backgroundCollider.bounds.max.x);
        float y = Random.Range(backgroundCollider.bounds.min.y, backgroundCollider.bounds.max.y);
        Vector2 spawnPosition = new Vector2(x, y);

        // 랜덤하게 총알 프리팹을 선택합니다.
        int randomIndex = Random.Range(0, bullets.Length);
        GameObject bulletPrefab = bullets[randomIndex];

        // 총알을 생성합니다.
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        // 생성된 총알에 랜덤한 방향과 세기로 힘을 가합니다.
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 forceDirection = Random.insideUnitCircle.normalized;
            float forceMagnitude = Random.Range(minForce, maxForce);
            rb.AddForce(forceDirection * forceMagnitude, ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogWarning("생성된 총알에 Rigidbody2D가 없습니다.");
        }
    }
}