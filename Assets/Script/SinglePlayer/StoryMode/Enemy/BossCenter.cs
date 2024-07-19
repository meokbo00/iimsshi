using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossCenter : MonoBehaviour
{
    BGMControl bGMControl;
    Rigidbody2D rigid;
    public float increase = 4f;
    public bool hasExpanded = false;
    public int randomNumber;
    public int initialRandomNumber; // 초기 randomNumber 값을 저장할 변수
    public TextMeshPro textMesh;
    public BossFire[] bossfires; // 여러 Enemy1Fire 참조를 위한 배열
    public bool isShowHP;

    public int MaxHP;
    public int MinHP;
    public float MaxFireTime;
    public float MinFireTime;
    public float MaxAngle;
    public float MinAngle;
    public float fontsize;

    public GameObject[] Enemy;

    private void Start()
    {
        bGMControl = FindObjectOfType<BGMControl>();
        rigid = GetComponent<Rigidbody2D>();
        GameObject textObject = new GameObject("TextMeshPro");
        textObject.transform.parent = transform;
        textMesh = textObject.AddComponent<TextMeshPro>();
        randomNumber = Random.Range(MinHP, MaxHP);
        initialRandomNumber = randomNumber; // 초기 randomNumber 값을 저장
        if (isShowHP)
        {
            textMesh.text = randomNumber.ToString();
        }
        textMesh.fontSize = fontsize;
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.autoSizeTextContainer = true;
        textMesh.rectTransform.localPosition = Vector3.zero;
        textMesh.sortingOrder = 3;

        bossfires = GetComponentsInChildren<BossFire>(); // Enemy1Fire 컴포넌트 배열 참조

        StartCoroutine(RandomAttack());
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "P1ball" || coll.gameObject.tag == "P2ball" || coll.gameObject.tag == "P1Item" || coll.gameObject.tag == "P2Item"
            || (coll.gameObject.tag == "Item" && coll.gameObject.name != "SPEndlessF(Clone)"))
        {
            if (randomNumber > 0)
            {
                randomNumber--;
                if (isShowHP)
                {
                    textMesh.text = randomNumber.ToString();
                }
            }
            if (randomNumber <= 0)
            {
                bGMControl.SoundEffectPlay(4);
                Destroy(transform.parent.gameObject); // 부모 오브젝트 삭제
            }
        }
        if (coll.gameObject.name == "SPTwiceF(Clone)")
        {
            randomNumber -= 1;
            if (randomNumber > 0)
            {
                textMesh.text = randomNumber.ToString();
            }
            if (randomNumber <= 0)
            {
                bGMControl.SoundEffectPlay(4);
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator Attack1()
    {
        // 5초 동안 정지
        yield return new WaitForSeconds(Random.Range(MinFireTime, MaxFireTime));

        // 회전할 각도 설정
        float targetAngle = Random.Range(MinAngle, MaxAngle);
        float currentAngle = transform.eulerAngles.z;
        float rotationTime = 1f; // 회전하는 데 걸리는 시간
        float elapsedTime = 0f;

        // 회전하기
        while (elapsedTime < rotationTime)
        {
            elapsedTime += Time.deltaTime;
            float angle = Mathf.LerpAngle(currentAngle, targetAngle, elapsedTime / rotationTime);
            transform.eulerAngles = new Vector3(0, 0, angle);
            yield return null;
        }

        // 회전이 끝난 후 1초 뒤에 총알 발사
        yield return new WaitForSeconds(1f);

        if (bossfires != null)
        {
            foreach (var enemy1Fire in bossfires)
            {
                enemy1Fire.SpawnBullet();
            }
        }
    }

    private IEnumerator Attack2()
    {
        yield return new WaitForSeconds(Random.Range(MinFireTime, MaxFireTime));

        if (Enemy.Length > 0)
        {
            // 랜덤으로 두 개의 적 오브젝트 선택
            GameObject enemy1 = Enemy[Random.Range(0, Enemy.Length)];
            GameObject enemy2 = Enemy[Random.Range(0, Enemy.Length)];
            GameObject enemy3 = Enemy[Random.Range(0, Enemy.Length)];
            GameObject enemy4 = Enemy[Random.Range(0, Enemy.Length)];


            // 보스의 양옆에 생성
            Instantiate(enemy1, transform.position + new Vector3(-1.5f, 0, 0), Quaternion.identity);
            Instantiate(enemy2, transform.position + new Vector3(1.5f, 0, 0), Quaternion.identity);
            Instantiate(enemy3, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
            Instantiate(enemy4, transform.position + new Vector3(0, -1.5f, 0), Quaternion.identity);

        }
    }

    

    private IEnumerator RandomAttack()
    {
        while (true)
        {
            int randomAttack = Random.Range(0, 2);
            switch (randomAttack)
            {
                case 0:
                    yield return StartCoroutine(Attack1());
                    break;
                case 1:
                    yield return StartCoroutine(Attack2());
                    break;
            }

            yield return new WaitForSeconds(4f); // 5초 대기 후 다음 공격 선택
        }
    }
}
