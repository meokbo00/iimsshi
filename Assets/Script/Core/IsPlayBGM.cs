using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayBGM : MonoBehaviour
{
    public AudioSource[] BGM; // 오디오 소스를 배열로 변경
    private BGMControl bGMControl;

    void Start()
    {
        bGMControl = FindObjectOfType<BGMControl>();

        if (bGMControl != null)
        {
            if (bGMControl.BGMSwitch)
            {
                if (StageState.chooseStage == 25 || StageState.chooseStage == 45)
                {
                    BGM[1].Play();
                }
                else if (StageState.chooseStage == 65)
                {
                    BGM[2].Play();
                }
                else
                {
                    BGM[0].Play();
                }
            }
            else
            {
                foreach (var audioSource in BGM)
                {
                    if (audioSource != null)
                    {
                        audioSource.Stop();
                    }
                }
            }
        }
    }
}
