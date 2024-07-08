using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingButtonManager : MonoBehaviour
{
    public Button BGM_ON;
    public Button BGM_OFF;
    public Button Sound_Effect_ON;
    public Button Sound_Effect_OFF;
    public Button Credit;
    public Button Back;

    public AudioSource ButtonAudio;

    public bool isBGM = true;
    public bool isSoundEffect = true;

    private void Start()
    {
        this.BGM_ON.onClick.AddListener(() =>
        {
            isBGM = true;
            ButtonAudio.Play();
            BGM_ON.gameObject.SetActive(false);
            BGM_OFF.gameObject.SetActive(true);
        });
        this.BGM_OFF.onClick.AddListener(() =>
        {
            isBGM = true;
            ButtonAudio.Play();
            BGM_OFF.gameObject.SetActive(false);
            BGM_ON.gameObject.SetActive(true);
        });
        this.Sound_Effect_ON.onClick.AddListener(() =>
        {
            isSoundEffect = true;
            ButtonAudio.Play();
            Sound_Effect_ON.gameObject.SetActive(false);
            Sound_Effect_OFF.gameObject.SetActive(true);
        });
        this.Sound_Effect_OFF.onClick.AddListener(() =>
        {
            isSoundEffect = true;
            ButtonAudio.Play();
            Sound_Effect_OFF.gameObject.SetActive(false);
            Sound_Effect_ON.gameObject.SetActive(true);
        });
        this.Credit.onClick.AddListener(() =>
        {
            ButtonAudio.Play();
            SceneManager.LoadScene("Credit Scene");
        });
        this.Back.onClick.AddListener(() =>
        {
            ButtonAudio.Play();
            SceneManager.LoadScene("Start Scene");
        });
    }
}