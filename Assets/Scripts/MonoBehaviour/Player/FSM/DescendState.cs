using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DescendState : PenguinState
{
    private Rigidbody _rigidBody;

    public override void Enter()
    {
        _rigidBody = Penguin.Instance.transform.GetComponent<Rigidbody>();
        _rigidBody.useGravity = true;
        // Debug.Log("<color=green>Entering Descending</color>");
    }

    public override void Exit()
    {
        // Debug.Log("<color=red>Exiting Descending</color>");
    }

    override public void OnUpdate()
    {
        if (Penguin.Instance.IsGrounded())
        {
            PenguinFSM.Instance.EnterState(PenguinStates.Land);
        }
    }
}
