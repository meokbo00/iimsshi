using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StageGameManager : MonoBehaviour
{
    public static StageGameManager instance = null;
    public int StageClearID = 1;

    private Vector3 clickPosition;
    public bool isDragging = false;
    public static float shotDistance;
    public static Vector3 shotDirection;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this) 
                Destroy(this.gameObject);
        }
    }

    //private void Start()
    //{
    //    StageGameManager gameManager = FindObjectOfType<StageGameManager>();
    //    if (gameManager != null && gameManager.StageClearID >= 6 && SceneManager.GetActiveScene().name == "Stage")
    //    {
    //        Debug.Log("5번 스테이지까지 클리어했으므로 Main Scene으로 넘어갑니다");
    //        SceneManager.LoadScene("Main Stage");
    //    }
    //}

    private void Update()
    {
        if (IsPointerOverUIObject())
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPosition.z = 0f;
            Collider2D[] colliders = Physics2D.OverlapPointAll(clickPosition);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("StageBall"))
                {
                    isDragging = true;
                    break;
                }
            }
        }

        if (isDragging && Input.GetMouseButtonUp(0))
        {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = 0f;
            shotDistance = Vector3.Distance(clickPosition, currentPosition);
            Vector3 dragDirection = (currentPosition - clickPosition).normalized;
            shotDirection = -dragDirection;
            isDragging = false;
            Debug.Log("���� ������ �����մϴ�");
        }
    }
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        {
            position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}