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
            Debug.LogError("StageGameManager를 찾을 수 없습니다.");
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

        // StageClearID에 따른 프리팹 인덱스 결정
        if (stageGameManager.StageClearID <= 5)
        {
            maxIndex = 1; // 첫 번째 프리팹만 사용
        }
        else if (stageGameManager.StageClearID <= 10)
        {
            maxIndex = 2; // 첫 번째와 두 번째 프리팹 사용
        }
        else if (stageGameManager.StageClearID <= 15)
        {
            maxIndex = 3; // 첫 번째, 두 번째, 세 번째 프리팹 사용
        }
        else
        {
            maxIndex = spherePrefabs.Length; // 모든 프리팹 사용
        }

        int prefabIndex = Random.Range(0, maxIndex);
        Instantiate(spherePrefabs[prefabIndex], randomPosition, Quaternion.identity);
    }
}