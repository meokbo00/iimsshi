using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SPGameManager : MonoBehaviour
{
    public GameObject P1ballPrefab;
    public GameObject[] FireItemPrefab;

    public GameObject P1firezone;
    public GameObject P1Itemsave;

    public GameObject fireitem;

    private Vector3 clickPosition;
    private StageGameManager gameManager;
    public bool isDragging = false;
    public static float shotDistance;
    public static Vector3 shotDirection;

    private void Start()
    {
        P1firezone.gameObject.SetActive(true);
        Debug.Log(StageState.chooseStage);
    }
    public void PrintDestroyedicontag(string icontag)
    {
        this.fireitem = null;
        switch (icontag)
        {
            case "Item_Big": fireitem = FireItemPrefab[0]; break;
            case "Item_Small": fireitem = FireItemPrefab[1]; break;
            case "Item_Twice": fireitem = FireItemPrefab[2]; break;
            case "Item_Endless": fireitem = FireItemPrefab[3]; break;
            case "Item_Invincible": fireitem = FireItemPrefab[4]; break;
        }
    }
    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPosition.z = 0f;
            Collider2D[] colliders = Physics2D.OverlapPointAll(clickPosition);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject == P1firezone)
                {
                    if (fireitem != null)
                    {
                        Instantiate(fireitem, clickPosition, Quaternion.identity);
                        Debug.Log("P1이 아이템을 사용하였습니다");
                        Debug.Log("아이템의 이름은 " + fireitem.gameObject.name + "입니다");
                        isDragging = true;
                        fireitem = null;
                        break;
                    }
                    else
                    {
                        Instantiate(P1ballPrefab, clickPosition, Quaternion.identity);
                        Debug.Log("P1이 기본구체를 날렸습니다");
                        isDragging = true;
                        break;
                    }
                }
            }
        }

        if (isDragging && Input.GetMouseButtonUp(0))
        {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = 0f;
            GameManager.shotDistance = Vector3.Distance(clickPosition, currentPosition);
            Vector3 dragDirection = (currentPosition - clickPosition).normalized;
            GameManager.shotDirection = -dragDirection;
            isDragging = false;
        }

        int totalBalls = GameObject.FindGameObjectsWithTag("EnemyBall").Length +
                       GameObject.FindGameObjectsWithTag("P1ball").Length;
        if (totalBalls > 12)
        {
            SceneManager.LoadScene("Fail");
        }

        int totalenemy = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (totalenemy <= 0)
        {
            gameManager = FindObjectOfType<StageGameManager>();
            if (gameManager.StageClearID == StageState.chooseStage)
            {
                gameManager.StageClearID += 1;
            }
            SceneManager.LoadScene("Clear");
        }
    }

}