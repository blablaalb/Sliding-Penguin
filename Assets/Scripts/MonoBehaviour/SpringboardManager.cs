using System.Collections.Generic;
using UnityEngine;
using System;

public class SpringboardManager : Singleton<SpringboardManager>
{
    [SerializeField]
    private GameObject _acceleratorPrefab;

    protected override void Awake()
    {
        base.Awake();
    }

    public Springboard GetSpringboard()
    {
        GameObject acceleratorGO = Instantiate(_acceleratorPrefab);
        Springboard accelerator = acceleratorGO.GetComponentInChildren<Springboard>();
        return accelerator;
    }
}
