using UnityEngine;
using System;

public class ColliderWithCallback : MonoBehaviour
{
    public Action<Collision> Collided;

    internal void Awake()
    {
        if (!gameObject.GetComponent<Collider>())
        {
            Debug.LogError("This game object doesn't have a collider attached", gameObject);
        }
    }

    internal void OnCollisionEnter(Collision collision)
    {
        Collided?.Invoke(collision);
    }
}