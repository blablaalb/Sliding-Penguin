using UnityEngine;
using System;

[Serializable]
public class OnGapState : PenguinState
{
    private Gap _gap;

    public void SetGap(Gap gap)
    {
        _gap = gap;
    }

    /// <summary>
    /// Before entering the state you should set the gap!
    /// </summary>
    public override void Enter()
    {
        _gap.DisableCollider();
        Penguin.Instance.Stop();
    }

    public override void Exit()
    {

    }

    public override void OnUpdate()
    {

    }
}