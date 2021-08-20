using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AscendState : PenguinState
{
    private bool _fly;

    public void SetFly(bool fly)
    {
        _fly = fly;
    }

    public override void Enter()
    {
        // Debug.Log("<color=green>Entering Jump</color>");
        Penguin.Instance.Jump();
    }

    public override void Exit()
    {
        // Debug.Log("<color=red>Exiting Jump</color>");
    }

    override public void OnUpdate()
    {
        if (IsDescending())
        {
            if (_fly)
            {
                PenguinFSM.Instance.EnterState(PenguinStates.Fly);
            }
            else
            {
                PenguinFSM.Instance.EnterState(PenguinStates.Descend);
            }
        }
    }

    private bool IsDescending()
    {
        return Penguin.Instance.Velocity.y < 0.000f;
    }
}
