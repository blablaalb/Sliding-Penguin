using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleParticleSystem : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private Vector3 _defaultLocalPosition;

    internal void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _defaultLocalPosition = transform.localPosition;
    }

    internal void Update()
    {
        transform.localPosition = _defaultLocalPosition;
    }

    public void Play()
    {
        _particleSystem.Play();
    }
}
