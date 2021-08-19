using System.Collections.Generic;
using UnityEngine;
using System;

public class SlideArea : MonoBehaviour
{
    public bool Slided { get; private set; }

    internal void Start()
    {
        PenguinFSM.Instance.Slided += OnSlided;
    }

    internal void OnDestroy()
    {
        if (PenguinFSM.Instance != null)
        {
            PenguinFSM.Instance.Slided -= OnSlided;
        }
    }


    public void OnSlided(SlideArea slideArea)
    {
        if (slideArea == this)
        {
            Slided = true;
        }
    }

}
