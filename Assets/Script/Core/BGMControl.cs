using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMControl : MonoBehaviour
{
    public AudioSource[] BGM;
    public AudioSource[] SoundEffect;
    public bool BGMSwitch;
    public bool SoundEffectSwitch;

    void Start()
    {
        UpdateAudioSources();
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
}
