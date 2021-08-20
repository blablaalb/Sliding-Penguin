using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class LandState : PenguinState
{
    public override void Enter()
    {
        // Debug.Log("<color=green>Entering Land</color>");
        if (PenguinFSM.Instance.GetCurrentState() == PenguinStates.Land)
        {
        }
    }

    public override void Exit()
    {
        // Debug.Log("<color=red>Exiting Land</color>");
    }

    override public void OnUpdate()
    {
        if (Penguin.Instance.IsGrounded())
            PenguinFSM.Instance.EnterState(PenguinStates.Sliding);
    }
}
