using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayBGM : MonoBehaviour
{
    public AudioSource BGM;
    private BGMControl bGMControl;

    void Start()
    {
        // BGMControl 객체를 찾아 할당
        bGMControl = FindObjectOfType<BGMControl>();

        if (bGMControl != null)
        {
            if (bGMControl.BGMSwitch)
            {
                BGM.Play();
            }
            else
            {
                BGM.Stop();
            }
        }
        else
        {
            Debug.LogError("BGMControl object not found in the scene.");
        }
    }
}
