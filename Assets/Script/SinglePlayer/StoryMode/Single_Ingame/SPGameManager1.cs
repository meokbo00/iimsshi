using UnityEngine;
using UnityEngine.SceneManagement;

public class SPGameManager : MonoBehaviour
{
    public GameObject P1firezone;
    public GameObject P1Itemsave;
    public string fireItemTag;

    private Vector3 clickPosition;
    private StageGameManager gameManager;
    public bool isDragging = false;
    public static float shotDistance;
    public static Vector3 shotDirection;
    public int chooseStagenum;

    private void Start()
    {
        P1firezone.gameObject.SetActive(true);
        Debug.Log(StageState.chooseStage);
    }

    public void PrintDestroyedicontag(string icontag)
    {
        fireItemTag = icontag; // 태그를 직접 사용하여 해당 아이템을 식별
    }

    private void Update()
    {
        // 마우스 클릭 시 오브젝트 생성
        if (Input.GetMouseButtonDown(0))
        {
            clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPosition.z = 0f;
            Collider2D[] colliders = Physics2D.OverlapPointAll(clickPosition);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject == P1firezone)
                {
                    if (!string.IsNullOrEmpty(fireItemTag))
                    {
                        GameObject fireItem = ObjectPooler.Instance.GetPooledObject(fireItemTag);
                        if (fireItem != null)
                        {
                            fireItem.transform.position = clickPosition;
                            fireItem.SetActive(true);
                            Debug.Log("P1이 아이템을 사용하였습니다");
                            Debug.Log("아이템의 이름은 " + fireItem.name + "입니다");
                            isDragging = true;
                            fireItemTag = null; // 아이템 태그 초기화
                            break;
                        }
                    }
                    else
                    {
                        GameObject ball = ObjectPooler.Instance.GetPooledObject("P1ball");
                        if (ball != null)
                        {
                            ball.transform.position = clickPosition;
                            ball.SetActive(true);
                            Debug.Log("P1이 기본구체를 날렸습니다");
                            isDragging = true;
                            break;
                        }
                    }
                }
            }
        }

        // 드래그 및 발사 처리
        if (isDragging && Input.GetMouseButtonUp(0))
        {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = 0f;
            SPGameManager.shotDistance = Vector3.Distance(clickPosition, currentPosition) * 2;
            Vector3 dragDirection = (currentPosition - clickPosition).normalized;
            SPGameManager.shotDirection = dragDirection;
            isDragging = false;
        }

        // 씬 전환 조건 체크
        int totalBalls = GameObject.FindGameObjectsWithTag("EnemyBall").Length +
                         GameObject.FindGameObjectsWithTag("P1ball").Length;
        if (totalBalls > 16)
        {
            SceneManager.LoadScene("Fail");
        }

        int totalEnemy = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (totalEnemy <= 0)
        {
            gameManager = FindObjectOfType<StageGameManager>();
            if (gameManager.StageClearID == StageState.chooseStage && gameManager.StageClearID != 5)
            {
                gameManager.StageClearID += 1;
                gameManager.SaveStageClearID();
            }
            if (gameManager.StageClearID == 5)
            {
                gameManager.StageClearID += 0.5f;
                gameManager.SaveStageClearID();
            }
            SceneManager.LoadScene("Clear");
        }
    }
}
