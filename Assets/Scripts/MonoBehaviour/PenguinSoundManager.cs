using System.Collections.Generic;
using UnityEngine;
using System;

public class PenguinSoundManager : Singleton<PenguinSoundManager>
{
    private AudioSource _audSource;
    [SerializeField]
    private AudioClip _flyAudioClip;

    override protected void Awake()
    {
        base.Awake();
        _audSource = GetComponent<AudioSource>();
    }

    internal void Start()
    {
        PenguinFSM.Instance.StateChanged += OnPenguinStateChanged;
    }

    override protected void OnDestroy()
    {
        if (PenguinFSM.Instance != null)
        {
            PenguinFSM.Instance.StateChanged -= OnPenguinStateChanged;
        }
        base.OnDestroy();
    }

    private void OnPenguinStateChanged(PenguinStates state)
    {
        StopAudio();

        switch (state)
        {
            case PenguinStates.Sliding:
                break;
            case PenguinStates.Ascend:
                break;
            case PenguinStates.Descend:
                break;
            case PenguinStates.Fly:
                PlayAudio(_flyAudioClip, true);
                break;
            case PenguinStates.Land:
                break;
        }
    }

    private void PlayAudio(AudioClip audioClip, bool loop = false)
    {
        StopAudio();
        _audSource.clip = _flyAudioClip;
        _audSource.loop = loop;
        _audSource.Play();
    }

    private void StopAudio()
    {
        _audSource.Stop();
    }
}
