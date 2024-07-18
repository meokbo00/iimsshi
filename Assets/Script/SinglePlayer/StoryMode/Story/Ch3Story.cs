using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 전환을 위해 추가

public class Ch3Story : MonoBehaviour
{
    public Camera Camera;
    public GameObject Fadein;
    public AudioSource[] AudioSources; // 오디오 소스 배열
    StageGameManager stageGameManager;
    TextManager textManager;
    ShowText showText;
    private bool isZooming = false;

    private bool isAudio1Played = false; // 첫 번째 오디오 재생 여부
    private bool isAudio2Played = false; // 두 번째 오디오 재생 여부

    void Start()
    {
        showText = FindObjectOfType<ShowText>();
        textManager = FindObjectOfType<TextManager>();
        stageGameManager = FindObjectOfType<StageGameManager>();

        if (stageGameManager.StageClearID == 65)
        {
            textManager.GiveMeTextId(1);
        }
    }

    void Update()
    {
        showText = FindObjectOfType<ShowText>();
        if (showText != null && stageGameManager.StageClearID == 65)
        {
            if (showText.logTextIndex == 8 && !isZooming)
            {
                StartCoroutine(SmoothZoom(5f, 1700f));
            }
            if (showText.logTextIndex == 12)
            {
                StartCoroutine(ExecuteAfterDelay(4f));
            }
            if (showText.logTextIndex == 25)
            {
                StartCoroutine(LoadSceneAfterDelay(5f, "Story-InGame"));
            }
        }
    }

    IEnumerator SmoothZoom(float duration, float targetSize)
    {
        isZooming = true;
        float startSize = Camera.orthographicSize;
        float elapsed = 0f;

        // 오디오 소스 배열의 첫 번째 오디오가 재생되지 않은 경우 재생
        if (AudioSources.Length > 0 && !isAudio1Played)
        {
            AudioSources[0].Play();
            isAudio1Played = true; // 첫 번째 오디오 재생 여부 설정
        }

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Camera.orthographicSize = Mathf.Lerp(startSize, targetSize, elapsed / duration);
            yield return null;
        }

        Camera.orthographicSize = targetSize;
        isZooming = false;
    }

    IEnumerator ExecuteAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Fadein.SetActive(true);

        // 오디오 소스 배열의 두 번째 오디오가 재생되지 않은 경우 재생
        if (AudioSources.Length > 1 && !isAudio2Played)
        {
            AudioSources[1].Play();
            isAudio2Played = true; // 두 번째 오디오 재생 여부 설정
        }
    }

    IEnumerator LoadSceneAfterDelay(float delay, string sceneName)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
