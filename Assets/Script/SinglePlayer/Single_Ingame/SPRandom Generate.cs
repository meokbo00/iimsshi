using UnityEngine;

public class SPRandomGenerate : MonoBehaviour
{
    public GameObject[] spherePrefabs;
    public GameObject background;
    public float minSpawnTime = 7f;
    public float maxSpawnTime = 12f;

    private float nextSpawnTime;
    private Collider2D backgroundCollider;
    private StageGameManager stageGameManager;

    void Start()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
        backgroundCollider = background.GetComponent<Collider2D>();

        stageGameManager = FindObjectOfType<StageGameManager>();
        if (stageGameManager == null)
        {
            Debug.LogError("StageGameManager�� ã�� �� �����ϴ�.");
        }
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnSphere();
            nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
        }
    }

    void SpawnSphere()
    {
        Vector2 min = backgroundCollider.bounds.min;
        Vector2 max = backgroundCollider.bounds.max;
        Vector3 randomPosition = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), 0f);

        int maxIndex;

        // StageClearID�� ���� ������ �ε��� ����
        if (stageGameManager.StageClearID <= 5)
        {
            maxIndex = 1; // ù ��° �����ո� ���
        }
        else if (stageGameManager.StageClearID <= 10)
        {
            maxIndex = 2; // ù ��°�� �� ��° ������ ���
        }
        else if (stageGameManager.StageClearID <= 15)
        {
            maxIndex = 3; // ù ��°, �� ��°, �� ��° ������ ���
        }
        else
        {
            maxIndex = spherePrefabs.Length; // ��� ������ ���
        }

        int prefabIndex = Random.Range(0, maxIndex);
        Instantiate(spherePrefabs[prefabIndex], randomPosition, Quaternion.identity);
    }
}