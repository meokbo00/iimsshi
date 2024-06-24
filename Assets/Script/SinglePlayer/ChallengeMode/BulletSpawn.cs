using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    public GameObject[] bullets; // Bullet ������ �迭
    public float spawnInterval = 10f; // �ʱ� ���� ���� (��)
    public float minimumSpawnInterval = 1f; // �ּ� ���� ���� (��)
    public float minForce = 1.7f; // �ּ� ���� ����
    public float maxForce = 7f; // �ִ� ���� ����

    private BoxCollider2D backgroundCollider;
    private float timer;

    void Start()
    {
        // BackGround ������Ʈ�� BoxCollider2D�� ã���ϴ�.
        GameObject background = GameObject.Find("BackGround");
        if (background != null)
        {
            backgroundCollider = background.GetComponent<BoxCollider2D>();
        }
        else
        {
            Debug.LogError("BackGround ������Ʈ�� ã�� �� �����ϴ�.");
        }

        // Ÿ�̸� �ʱ�ȭ
        timer = spawnInterval;
    }

    void Update()
    {
        // Ÿ�̸Ӹ� ���ҽ�ŵ�ϴ�.
        timer -= Time.deltaTime;

        // Ÿ�̸Ӱ� 0 ���ϰ� �Ǹ� �Ѿ��� �����մϴ�.
        if (timer <= 0f)
        {
            SpawnBullet();
            // ���� ������ ���Դϴ�.
            spawnInterval = Mathf.Max(minimumSpawnInterval, spawnInterval - 0.5f);
            timer = spawnInterval; // Ÿ�̸� ����
        }
    }

    void SpawnBullet()
    {
        if (bullets.Length == 0 || backgroundCollider == null)
        {
            Debug.LogWarning("������ �Ѿ� �������� ���ų� BackGround Collider�� �����ϴ�.");
            return;
        }

        // BackGround�� �ݶ��̴� ���� ������ ���� ��ġ�� �����մϴ�.
        float x = Random.Range(backgroundCollider.bounds.min.x, backgroundCollider.bounds.max.x);
        float y = Random.Range(backgroundCollider.bounds.min.y, backgroundCollider.bounds.max.y);
        Vector2 spawnPosition = new Vector2(x, y);

        // �����ϰ� �Ѿ� �������� �����մϴ�.
        int randomIndex = Random.Range(0, bullets.Length);
        GameObject bulletPrefab = bullets[randomIndex];

        // �Ѿ��� �����մϴ�.
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        // ������ �Ѿ˿� ������ ����� ����� ���� ���մϴ�.
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 forceDirection = Random.insideUnitCircle.normalized;
            float forceMagnitude = Random.Range(minForce, maxForce);
            rb.AddForce(forceDirection * forceMagnitude, ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogWarning("������ �Ѿ˿� Rigidbody2D�� �����ϴ�.");
        }
    }
}