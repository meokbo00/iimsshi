using System;
using UnityEngine;

public class StageGameManager : MonoBehaviour
{
    private Vector3 clickPosition;
<<<<<<< HEAD
=======
    private GameObject clickedObject;
>>>>>>> 6fcf64348e0c59a6ac983e94112182f58387eb06
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
<<<<<<< HEAD
=======
                    clickedObject = collider.gameObject;
>>>>>>> 6fcf64348e0c59a6ac983e94112182f58387eb06
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
<<<<<<< HEAD
            Debug.Log(shotDistance);
            Debug.Log(shotDirection);
=======
>>>>>>> 6fcf64348e0c59a6ac983e94112182f58387eb06
        }
    }
}