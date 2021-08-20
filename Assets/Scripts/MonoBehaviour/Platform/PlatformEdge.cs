using System.Collections.Generic;
using UnityEngine;
using System;

public class PlatformEdge : MonoBehaviour
{
    // In order to jump from the edge second time penguin should exit the collider and then enter it again.
    public bool CanJump { get; private set; }

    internal void Awake()
    {
        gameObject.AddComponent<JumpArea>();
        CanJump = true;
    }

    internal void Start()
    {
        // PenguinJumpManager.Instance.Jumped += OnPenguinJumped;
    }

    internal void OnDestroy()
    {
        // PenguinJumpManager.Instance.Jumped -= OnPenguinJumped;
    }

    private void OnPenguinJumped(JumpArea jumpArea)
    {
        if (jumpArea == this)
        {
            CanJump = false;
        }
    }

    internal void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Penguin>() is Penguin penguin)
        {
            if (CanJump)
            {
                CanJump = false;
            }
        }
    }

    internal void OnTriggerExit(Collider collider)
    {
        CanJump = true;
    }
}
