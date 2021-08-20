using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public abstract class PenguinState
{
    public abstract void Enter();
    public abstract void Exit();
    public abstract void OnUpdate();
}
