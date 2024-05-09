using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Force_Skill : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 lastVelocity;
    float deceleration = 2f;
    public float increase = 4f;
    private bool iscolliding = false;
    public bool hasExpanded = false;
    private bool isStopped = false;
    private float number;
    private TextMeshPro textMesh;
    private bool hasBeenReleased = false;

    public AudioSource HitSound;
    public AudioSource SwellSound;


    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        GameObject textObject = new GameObject("TextMeshPro");
        textObject.transform.parent = transform;
        textMesh = textObject.AddComponent<TextMeshPro>();
        number = Random.Range(10f, 30f);
        textMesh.text = number.ToString();
        textMesh.fontSize = 2.5f;
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.autoSizeTextContainer = true;
        textMesh.rectTransform.localPosition = Vector3.zero;
        textMesh.sortingOrder = 1;
        UpdateText();
    }

    private void Update()
    {
        // 최초 클릭 이후에만 힘이 가해지도록 설정
        if (Input.GetMouseButtonUp(0) && rigid != null && !hasBeenReleased)
        {
            rigid.velocity = GameManager.shotDirection * GameManager.shotDistance; // GameManager에서 값 가져와서 구체 발사
            hasBeenReleased = true; // 최초 클릭이 되었음을 표시
        }
        Move();
        expand();
    }

    void Move()
    {
        if (rigid == null || isStopped) return;

        lastVelocity = rigid.velocity;
        rigid.velocity -= rigid.velocity.normalized * deceleration * Time.deltaTime;

        if (rigid.velocity.magnitude <= 0.01f && hasExpanded)
        {
            isStopped = true;
            StartCoroutine(DestroyRigidbodyDelayed());
        }
    }
    void expand()
    {
        if (rigid == null || iscolliding) return;
        if (rigid.velocity.magnitude > 0.01f) return;
        if (Input.GetMouseButton(0)) return;

        if (!hasExpanded)
        {
            SwellSound.Play();
        }
        transform.localScale += Vector3.one * increase * Time.deltaTime;
        hasExpanded = true;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (!hasExpanded)
        {
            HitSound.Play();
        }

        if ((coll.gameObject.tag == "P1ball" || coll.gameObject.tag == "P2ball" || coll.gameObject.tag == "P1Item" || coll.gameObject.tag == "P2Item") && rigid == null)
        {
            if (number > 0)
            {
                number -= coll.relativeVelocity.magnitude; // 부딪힌 세기만큼 값을 감소시킴
                //textMesh.text = number.ToString();
                UpdateText();
            }
            if (number <= 0)
            {
                Destroy(gameObject);
            }
        }
        if (coll.contacts != null && coll.contacts.Length > 0)
        {
            Vector2 dir = Vector2.Reflect(lastVelocity.normalized, coll.contacts[0].normal);
            if (rigid != null)
                rigid.velocity = dir * Mathf.Max(lastVelocity.magnitude, 0f); // 감속하지 않고 반사만 진행
        }
        this.iscolliding = true;

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        this.iscolliding = false;
    }

    IEnumerator DestroyRigidbodyDelayed()
    {
        yield return new WaitForSeconds(0.8f);
        if (rigid != null)
            Destroy(rigid);
    }
    private void UpdateText()
    {
        // 숫자를 소수점 한 자리까지 문자열로 변환하여 출력
        textMesh.text = number.ToString("F1");
    }
}