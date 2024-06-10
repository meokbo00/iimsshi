using UnityEngine;

public class StageState : MonoBehaviour
{
    public GameObject StageStart;
    public GameObject StartButton;
    public GameObject Clearhere; // Clearhere 오브젝트를 public으로 추가
    public int stagenum;
    public static int chooseStage;
    private bool isclear;
    private StageGameManager gameManager;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        string namePart = gameObject.name.Substring(5, 2); // 6번째와 7번째 문자를 추출
        this.stagenum = int.Parse(namePart);
        gameManager = StageGameManager.instance;
        int stageClearID = gameManager.StageClearID;

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (stageClearID < this.stagenum)
        {
            isclear = false;
            spriteRenderer.color = new Color32(50, 50, 50, 255);
        }
        else if (stageClearID == this.stagenum)
        {
            isclear = true;
            spriteRenderer.color = new Color32(50, 50, 50, 255);

            if (Clearhere != null)
            {
                Instantiate(Clearhere, transform.position, Quaternion.identity, transform);
            }
        }
        else
        {
            isclear = true;
            spriteRenderer.color = new Color32(255, 255, 255, 255);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "StageBall")
        {
            Debug.Log("스테이지 플레이창을 띄웁니다");
            StageStart.gameObject.SetActive(true);
            if (!isclear)
            {
                StartButton.gameObject.SetActive(false);
            }
            else if (isclear)
            {
                StartButton.gameObject.SetActive(true);
            }
        }
        chooseStage = stagenum;
        Debug.Log("chooseStage : " + chooseStage);
        FindObjectOfType<ShowStageBox>().UpdateStageInfo(chooseStage);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "StageBall")
            StageStart.gameObject.SetActive(false);
    }
}