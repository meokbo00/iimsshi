using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 씬 전환을 위해 추가

public class Ch1_2Story : MonoBehaviour
{
    StageGameManager stageGameManager;
    private ShowText showText;
    public GameObject Clock;
    public GameObject Fadeinout;
    public GameObject creation;
    RemainTime remainTime;
    public Image fadeImage;

    void Start()
    {
        showText = FindObjectOfType<ShowText>();
        fadeImage = Fadeinout.GetComponent<Image>();
        stageGameManager = FindObjectOfType<StageGameManager>();
        TextManager textManager = FindObjectOfType<TextManager>();

        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        if (stageGameManager.StageClearID == 6)
        {
            Fadeinout.SetActive(true);
            textManager.GiveMeTextId(4);
            Clock.SetActive(true);
            remainTime = FindObjectOfType<RemainTime>();
            remainTime.years = 0;
            remainTime.days = 0;
            remainTime.hours = 0;
            remainTime.minutes = 0;
            remainTime.seconds = 0;

            SpriteRenderer[] spriteRenderers = creation.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sr in spriteRenderers)
            {
                sr.color = Color.black;
            }

            ContinuousRandomMovement[] randomMovements = creation.GetComponentsInChildren<ContinuousRandomMovement>();
            foreach (ContinuousRandomMovement rm in randomMovements)
            {
                rm.enabled = false;
            }
        }
    }

    void Update()
    {
        showText = FindObjectOfType<ShowText>();

        if (showText != null && stageGameManager.StageClearID == 6)
        {
            if (showText.logTextIndex > 38)
            {
                Fadeinout.SetActive(false);
            }
            if (showText.logTextIndex == 48)
            {
                StartCoroutine(FadeIn());
            }
        }
    }

    IEnumerator FadeIn()
    {
        Fadeinout.SetActive(true);
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;

        while (color.a < 1f)
        {
            color.a += Time.deltaTime / 5; // 2초에 걸쳐 페이드 인
            fadeImage.color = color;
            yield return null;
        }

        // 알파값이 1이 되면 3초 대기
        yield return new WaitForSeconds(3f);

        // "Main Stage"로 씬 전환
        SceneManager.LoadScene("Main Stage");
    }
}
