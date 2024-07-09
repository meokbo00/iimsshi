using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Scene Management 네임스페이스 추가

public class FinalPrologueManager : MonoBehaviour
{
    public GameObject[] images;

    void Start()
    {
        // 모든 이미지의 CanvasGroup을 설정합니다.
        foreach (GameObject image in images)
        {
            CanvasGroup canvasGroup = image.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = image.AddComponent<CanvasGroup>();
            }
            canvasGroup.alpha = 0f;  // 처음엔 모두 투명하게 설정합니다.
        }

        StartCoroutine(ShowImagesSequentially());
    }

    IEnumerator ShowImagesSequentially()
    {
        foreach (GameObject image in images)
        {
            yield return StartCoroutine(FadeIn(image));
            yield return new WaitForSeconds(4.5f);  // 7초 - 1초 (페이드 아웃 시간)
            yield return StartCoroutine(FadeOut(image));
        }

        yield return new WaitForSeconds(2f);
        // 마지막 이미지 페이드 아웃이 끝난 후 "Stage" 씬으로 전환
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
