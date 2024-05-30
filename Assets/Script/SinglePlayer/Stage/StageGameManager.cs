using System;
using UnityEngine;

public class StageGameManager : MonoBehaviour
{
    private Vector3 clickPosition;
<<<<<<< HEAD:Assets/Script/SinglePlay/Stage/StageGameManager.cs
<<<<<<< HEAD
=======
    private GameObject clickedObject;
>>>>>>> 6fcf64348e0c59a6ac983e94112182f58387eb06
=======
>>>>>>> b685f8518e130fb019b884e08be9052cb5c494b8:Assets/Script/SinglePlayer/Stage/StageGameManager.cs
    public bool isDragging = false;
    public static float shotDistance;
    public static Vector3 shotDirection;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPosition.z = 0f;
            Collider2D[] colliders = Physics2D.OverlapPointAll(clickPosition);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("StageBall"))
                {
<<<<<<< HEAD:Assets/Script/SinglePlay/Stage/StageGameManager.cs
<<<<<<< HEAD
=======
                    clickedObject = collider.gameObject;
>>>>>>> 6fcf64348e0c59a6ac983e94112182f58387eb06
=======
>>>>>>> b685f8518e130fb019b884e08be9052cb5c494b8:Assets/Script/SinglePlayer/Stage/StageGameManager.cs
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
<<<<<<< HEAD:Assets/Script/SinglePlay/Stage/StageGameManager.cs
<<<<<<< HEAD
            Debug.Log(shotDistance);
            Debug.Log(shotDirection);
=======
>>>>>>> 6fcf64348e0c59a6ac983e94112182f58387eb06
=======
            Debug.Log("���� ������ �����մϴ�");
>>>>>>> b685f8518e130fb019b884e08be9052cb5c494b8:Assets/Script/SinglePlayer/Stage/StageGameManager.cs
        }
    }
}