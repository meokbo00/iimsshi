using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMControl : MonoBehaviour
{
    public AudioSource[] BGM;
    public AudioSource[] SoundEffect;
    public bool BGMSwitch = true;
    public bool SoundEffectSwitch = true;

    private string currentSceneName;
    private bool isBGMPaused;

    private void Awake()
    {
        // 씬이 로드될 때마다 OnSceneLoaded 메서드를 호출
        SceneManager.sceneLoaded += OnSceneLoaded;

        // 게임 시작 시 저장된 상태를 불러오기
        LoadAudioSettings();
    }

    void Update()
    {
        UpdateAudioSources();
    }

    void UpdateAudioSources()
    {
        ToggleAudioSources(BGM, BGMSwitch);
        ToggleAudioSources(SoundEffect, SoundEffectSwitch);
    }

    void ToggleAudioSources(AudioSource[] sources, bool isEnabled)
    {
        foreach (var source in sources)
        {
            if (!isEnabled && source.isPlaying)
            {
                source.Stop();
            }
            source.enabled = isEnabled;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 이전 씬이 BGM을 재생 중이었고, 현재 씬에서도 BGM을 재생해야 하는 경우
        if (isBGMPaused && BGMSwitch)
        {
            PlayBGM();
        }
        // 현재 씬이 "Start Scene", "Setting Scene", "Credit Scene" 중 하나라면 BGM 재생
        else if (scene.name == "Start Scene" || scene.name == "Setting Scene" || scene.name == "Credit Scene")
        {
            PlayBGM();
        }
        else
        {
            StopAllAudio(BGM);
        }

        // 현재 씬 이름 업데이트
        currentSceneName = scene.name;
    }

    private void PlayBGM()
    {
        if (BGM.Length > 0 && !BGM[0].isPlaying)
        {
            BGM[0].Play();
        }
    }

    private void StopAllAudio(AudioSource[] sources)
    {
        foreach (var source in sources)
        {
            if (source.isPlaying)
            {
                source.Stop();
            }
        }
    }

    private void OnDestroy()
    {
        // 씬 매니저에서 이벤트를 해제하여 메모리 누수를 방지
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 게임 시작 시 저장된 상태를 불러오기
    private void LoadAudioSettings()
    {
        BGMSwitch = PlayerPrefs.GetInt("BGMSwitch", 1) == 1; // 기본값은 true
        SoundEffectSwitch = PlayerPrefs.GetInt("SoundEffectSwitch", 1) == 1; // 기본값은 true
    }

    // 상태가 변경될 때 저장하기
    public void SaveAudioSettings()
    {
        PlayerPrefs.SetInt("BGMSwitch", BGMSwitch ? 1 : 0);
        PlayerPrefs.SetInt("SoundEffectSwitch", SoundEffectSwitch ? 1 : 0);
        PlayerPrefs.Save();
    }
}
