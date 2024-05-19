using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy1center : MonoBehaviour
{
    Rigidbody2D rigid;
    public float increase = 4f;
    public bool hasExpanded = false;
    private int randomNumber;
    private TextMeshPro textMesh;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        GameObject textObject = new GameObject("TextMeshPro");
        textObject.transform.parent = transform;
        textMesh = textObject.AddComponent<TextMeshPro>();
        randomNumber = Random.Range(1, 6);
        textMesh.text = randomNumber.ToString();
        textMesh.fontSize = 6;
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.autoSizeTextContainer = true;
        textMesh.rectTransform.localPosition = Vector3.zero;
        textMesh.sortingOrder = 3;
    }

    private void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "P1ball" || coll.gameObject.tag == "P2ball" || coll.gameObject.tag == "P1Item" || coll.gameObject.tag == "P2Item")
        {
            if (randomNumber > 0)
            {
                randomNumber--;
                textMesh.text = randomNumber.ToString();
            }
            if (randomNumber <= 0)
            {
                Destroy(transform.parent.gameObject); // 부모 오브젝트 삭제
            }
        }
    }
}