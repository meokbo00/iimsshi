using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Scene Management ���ӽ����̽� �߰�

public class FinalPrologueManager : MonoBehaviour
{
    public GameObject[] images;

    void Start()
    {
        // ��� �̹����� CanvasGroup�� �����մϴ�.
        foreach (GameObject image in images)
        {
            CanvasGroup canvasGroup = image.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = image.AddComponent<CanvasGroup>();
            }
            canvasGroup.alpha = 0f;  // ó���� ��� �����ϰ� �����մϴ�.
        }

        StartCoroutine(ShowImagesSequentially());
    }

    IEnumerator ShowImagesSequentially()
    {
        foreach (GameObject image in images)
        {
            yield return StartCoroutine(FadeIn(image));
            yield return new WaitForSeconds(4.5f);  // 7�� - 1�� (���̵� �ƿ� �ð�)
            yield return StartCoroutine(FadeOut(image));
        }

        yield return new WaitForSeconds(2f);
        // ������ �̹��� ���̵� �ƿ��� ���� �� "Stage" ������ ��ȯ
        SceneManager.LoadScene("Stage");
    }

    IEnumerator FadeIn(GameObject image)
    {
        CanvasGroup canvasGroup = image.GetComponent<CanvasGroup>();
        float duration = 2f;
        float elapsed = 0f;

        image.SetActive(true);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }

    IEnumerator FadeOut(GameObject image)
    {
        CanvasGroup canvasGroup = image.GetComponent<CanvasGroup>();
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(1f - (elapsed / duration));
            yield return null;
        }
        canvasGroup.alpha = 0f;

        image.SetActive(false);
    }
}
