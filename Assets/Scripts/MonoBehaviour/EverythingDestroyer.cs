using System.Collections.Generic;
using UnityEngine;
using System;

public class EverythingDestroyer : MonoBehaviour
{
    internal void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Platform>() is Platform platform)
        {
            platform.Destroy();
        }
    }
}
