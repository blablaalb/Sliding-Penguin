using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleParticleSystem : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] _particleSystems;
    private AudioSource _audSource;
    [SerializeField]
    private AudioClip _epxlosionAudClip;

    internal void Awake()
    {
        // _particleSystems = GetComponentsInChildren<ParticleSystem>();
        _audSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        foreach (var particle in _particleSystems)
            particle.Play();

        _audSource.PlayOneShot(_epxlosionAudClip);
    }
}
