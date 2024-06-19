using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class BootingManager : MonoBehaviour
{
    public GameObject videoPlayerObject; // Video Player가 부착된 게임 오브젝트를 인스펙터에서 설정

    private VideoPlayer videoPlayer;

    void Start()
    {
        if (videoPlayerObject != null)
        {
            videoPlayer = videoPlayerObject.GetComponent<VideoPlayer>();
            if (videoPlayer != null)
            {
                videoPlayer.loopPointReached += OnVideoEnd;
            }
            else
            {
                Debug.LogError("Video Player component not found on the specified GameObject.");
            }
        }
        else
        {
            Debug.LogError("Video Player GameObject is not assigned.");
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // 영상이 끝나면 호출되는 콜백
        StartCoroutine(LoadPrologueAfterDelay(3f));
    }

    IEnumerator LoadPrologueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Prologue");
    }

    void OnDestroy()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }
}