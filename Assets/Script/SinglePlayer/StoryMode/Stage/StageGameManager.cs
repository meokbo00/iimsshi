using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StageGameManager : MonoBehaviour
{
    public static StageGameManager instance = null;
    public int StageClearID;

  

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadStageClearID();
        }
        else
        {
            if (instance != this) 
                Destroy(this.gameObject);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; // 게임 일시정지
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // 게임 일시정지 해제
    }

    public void SaveStageClearID()
    {
        PlayerPrefs.SetInt("StageClearID", StageClearID);
        PlayerPrefs.Save();
    }

    private void LoadStageClearID()
    {
        if (PlayerPrefs.HasKey("StageClearID"))
        {
            StageClearID = PlayerPrefs.GetInt("StageClearID");
        }
    }
}