using UnityEngine;
using System;

[Serializable]
public class BellySlideStandUp : PenguinState
{
    private bool _entered;

    public override void Enter()
    {
        _entered = true;
    }

    public override void OnUpdate()
    {
        if (_entered)
            PenguinFSM.Instance.EnterState(PenguinStates.Sliding);
    }

    public override void Exit()
    {
        _entered = false;
    }
}