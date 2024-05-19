using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy1Fire : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    public static float ShotPower;
    public static Vector3 ShotDirection;
    void Start()
    {
        StartCoroutine(SpawnBullets());
    }
    private IEnumerator SpawnBullets()
    {
        while (true)
        {
            float waitTime = Random.Range(4f, 7f);
            yield return new WaitForSeconds(waitTime);
            Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);

            CalculateRandomShot();
        }
    }
    private void CalculateRandomShot()
    {
        ShotPower = Random.Range(4f, 8f);
        float x = Random.Range(0f, 360f);
        float y = Random.Range(0f, 360f);
        ShotDirection = new Vector3(0, -1, 0).normalized;
    }
}
