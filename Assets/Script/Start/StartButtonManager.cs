using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartButtonManager : MonoBehaviour
{
    public GameObject SinglePlaySetting;
    public Button SinglePlayer;
    public Button MultiPlayer;
    public Button Setting;
    public Button Quit;

    public AudioSource ButtonAudio;

    private TextMeshProUGUI singlePlayerText;
    private TextMeshProUGUI onlinePlayText;

    void Start()
    {
        singlePlayerText = SinglePlayer.GetComponentInChildren<TextMeshProUGUI>();

        SinglePlayer.onClick.AddListener(OnSinglePlayerClicked);
        MultiPlayer.onClick.AddListener(OnMultiPlayerClicked);
        Setting.onClick.AddListener(OnSettingClicked);
        Quit.onClick.AddListener(OnQuitClicked);
    }

    void OnSinglePlayerClicked()
    {
        ButtonAudio.Play();
        SinglePlaySetting.gameObject.SetActive(true);
    }
    void OnMultiPlayerClicked()
    {
        ButtonAudio.Play();
        SceneManager.LoadScene("Main Scene");
    }


    void OnSettingClicked()
    {
        ButtonAudio.Play();
        SceneManager.LoadScene("Setting Scene");
    }

    void OnQuitClicked()
    {
        ButtonAudio.Play();
        Application.Quit();
        Debug.Log("게임 종료!");
    }
}