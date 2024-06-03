using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//��Ƽ�÷��̿� �ִ� �Ͻ����� ��ư����� ����ϴ� ��ũ��Ʈ�Դϴ�
public class MultiStopButtonManager : MonoBehaviour
{
    public Button Stopbtn;
    public Button Xbtn;
    public Button Resume;
    public Button Restart;
    public Button BacktoStage;
    public Button Main_menu;

    public GameObject Stop_Channel;
    public AudioSource ButtonAudio;

    bool ispause;
    void Start()
    {
        ispause = false;
        this.Stopbtn.onClick.AddListener(() =>
        {
            ButtonAudio.Play();
            this.Stop_Channel.gameObject.SetActive(true);

            Time.timeScale = 0;
            ispause = true;
        });
        this.Xbtn.onClick.AddListener(() =>
        {
            ButtonAudio.Play();
            this.Stop_Channel.gameObject.SetActive(false);

            Time.timeScale = 1;
            ispause = false;
            return;
        });
        this.Resume.onClick.AddListener(() =>
        {
            ButtonAudio.Play();
            this.Stop_Channel.gameObject.SetActive(false);

            Time.timeScale = 1;
            ispause = false;
            return;
        });
        this.Restart.onClick.AddListener(() =>
        {
            ButtonAudio.Play();
            this.Stop_Channel.gameObject.SetActive(false);

            Time.timeScale = 1;
            ispause = false;

            SceneManager.LoadScene("Main Scene");
        });
        this.BacktoStage.onClick.AddListener(() =>
        {
            ButtonAudio.Play();
            this.Stop_Channel.gameObject.SetActive(false);

            Time.timeScale = 1;
            ispause = false;

            SceneManager.LoadScene("Stage");
        });
        this.Main_menu.onClick.AddListener(() =>
        {
            ButtonAudio.Play();
            this.Stop_Channel.gameObject.SetActive(false);

            Time.timeScale = 1;
            ispause = false;

            SceneManager.LoadScene("Start Scene");
        });
    }
}