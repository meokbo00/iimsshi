using System.Collections;
using TMPro;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 lastVelocity;
    float deceleration = 2f;
    public float increase = 4f;
    private bool iscolliding = false;
    public bool hasExpanded = false;
    private bool isStopped = false;
    private int randomNumber;
    private TextMeshPro textMesh;
    private bool hasBeenReleased = false;
    public float fontsize;
    BGMControl bGMControl;

    private void Start()
    {
        bGMControl = FindObjectOfType<BGMControl>();
        rigid = GetComponent<Rigidbody2D>();
        GameObject textObject = new GameObject("TextMeshPro");
        textObject.transform.parent = transform;
        textMesh = textObject.AddComponent<TextMeshPro>();
        randomNumber = Random.Range(1, 6);
        textMesh.text = randomNumber.ToString();
        textMesh.fontSize = fontsize;
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.autoSizeTextContainer = true;
        textMesh.rectTransform.localPosition = Vector3.zero;
        textMesh.sortingOrder = 1;
    }

    private void Update()
    {
        if (ShouldApplyForce()) ApplyForce();
    }

    private bool ShouldApplyForce()
    {
        return Input.GetMouseButtonUp(0) && rigid != null && !hasBeenReleased;
    }

    private void ApplyForce()
    {
        rigid.velocity = GameManager.shotDirection * GameManager.shotDistance;
        hasBeenReleased = true;
        StartCoroutine(ManageBallMovement());
    }

    private IEnumerator ManageBallMovement()
    {
        while (!isStopped || !iscolliding)
        {
            Move();
            Expand();
            yield return null;
        }
    }

    private void Move()
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

    private void Expand()
    {
        if (rigid == null) return;
        if (rigid.velocity.magnitude > 0.1f) return;

        if (!hasExpanded)
        {
            bGMControl.SoundEffectPlay(1);
        }
        transform.localScale += Vector3.one * increase * Time.deltaTime;
        hasExpanded = true;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (!hasExpanded)
        {
            bGMControl.SoundEffectPlay(0);
        }
        if ((coll.gameObject.tag == "P1ball" || coll.gameObject.tag == "P2ball" || coll.gameObject.tag == "P1Item" || coll.gameObject.tag == "P2Item"
            || coll.gameObject.tag == "EnemyBall" || coll.gameObject.tag == "Item") && rigid == null)
        {
            if (randomNumber > 0)
            {
                randomNumber--;
                textMesh.text = randomNumber.ToString();
            }
            if (randomNumber <= 0)
            {
                Destroy(gameObject);
            }
        }
        if ((coll.gameObject.name == "SPTwiceF(Clone)" && rigid == null) || (coll.gameObject.name == "TwiceBullet(Clone)" && rigid == null))
        {
            randomNumber -= 1;
            if (randomNumber > 0)
            {
                textMesh.text = randomNumber.ToString();
            }
            if (randomNumber <= 0)
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
        hasExpanded = false; // Reset expansion on collision
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        this.iscolliding = false;
        hasExpanded = false; // Reset expansion on collision exit
        if (isStopped)
        {
            StartCoroutine(ManageBallMovement()); // Restart expansion if stopped
        }
    }

    IEnumerator DestroyRigidbodyDelayed()
    {
        yield return new WaitForSeconds(0.8f);
        if (rigid != null)
            Destroy(rigid);
    }
}
