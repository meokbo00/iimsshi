using UnityEngine;

public class StageState : MonoBehaviour
{
    public GameObject StageStart;
    public GameObject StartButton;
    public int stagenum;
    public static int chooseStage;
    private bool isclear;
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
            isclear = false;
            spriteRenderer.color = new Color32(73, 73, 73, 255);
        }
        else if (stageClearID == this.stagenum)
        {
            isclear = true;
            spriteRenderer.color = new Color32(255, 61, 61, 255);
        }
        else
        {
            isclear = true;
            spriteRenderer.color = new Color32(69, 155, 255, 255);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "StageBall")
        {
            StageStart.gameObject.SetActive(true);
            if(!isclear)
            {
                StartButton.gameObject.SetActive(false);
            }
            else if(isclear)
            {
                StartButton.gameObject.SetActive(true) ;
            }
        }
        chooseStage = stagenum;
        Debug.Log(chooseStage);
        FindObjectOfType<ShowStageBox>().UpdateStageInfo(chooseStage);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "StageBall")
            StageStart.gameObject.SetActive(false);
    }
}
