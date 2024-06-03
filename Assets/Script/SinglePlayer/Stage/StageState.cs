using UnityEngine;

public class StageState : MonoBehaviour
{
    public GameObject StageStart;
    public int stagenum;
    public static int chooseStage;
    private StageGameManager gameManager;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        this.stagenum = int.Parse(gameObject.name[5].ToString());
        gameManager = StageGameManager.instance;
        int stageClearID = gameManager.StageClearID;

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (stageClearID < this.stagenum)
        {
            Debug.Log("big");
            spriteRenderer.color = new Color32(73, 73, 73, 255);
        }
        else if (stageClearID == this.stagenum)
        {
            Debug.Log("same");
            spriteRenderer.color = new Color32(255, 61, 61, 255);
        }
        else
        {
            Debug.Log("small");
            spriteRenderer.color = new Color32(69, 155, 255, 255);
        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "StageBall")
    //    {
    //        StageStart.gameObject.SetActive(true);
    //    }
    //    chooseStage = stagenum;
    //    Debug.Log($"chooseStage 값이 {chooseStage}로 설정되었습니다.");
    //    FindObjectOfType<ShowStageBox>().UpdateStageInfo(chooseStage);
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "StageBall")
    //        StageStart.gameObject.SetActive(false);
    //}
}
