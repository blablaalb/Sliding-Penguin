using System.Collections.Generic;
using UnityEngine;
using System;
using MEC;

[Serializable]
public class FlyState : PenguinState
{
    [SerializeField]
    private float _flyDuration = 1f;
    private Rigidbody _rigidBody;
    private CoroutineHandle _flyCoroutineHandle;
    private bool _fly;

    override public void Enter()
    {
        if (Penguin.Instance == null)
        {
            Debug.LogError("Penguin instance is null");
            return;
        }

        if (AbovePlatform())
        {
            _fly = false;
            PenguinFSM.Instance.EnterState(PenguinStates.Descend);
            return;
        }

        // Debug.Log("<color=green>Entering Fly</color>");
        _rigidBody = Penguin.Instance.GetComponent<Rigidbody>();
        _fly = true;
        StartFlyCoroutine();
    }

    override public void Exit()
    {
        // Debug.Log("<color=red>Exiting Fly</color>");
        _fly = false;
        StopFlyCoroutine();
        if (_rigidBody != null)
        {
            _rigidBody.useGravity = true;
        }
    }

    override public void OnUpdate()
    {
        if (_fly)
        {
            if (AbovePlatform())
            {
                _fly = false;
                PenguinFSM.Instance.EnterState(PenguinStates.Descend);
            }
        }
    }

    private IEnumerator<float> FlyCoroutine()
    {
        _rigidBody.useGravity = false;
        while (_fly)
        {
            Fly();
            yield return Timing.WaitForSeconds(_flyDuration);
            _fly = false;
        }
        PenguinFSM.Instance.EnterState(PenguinStates.Descend);
    }

    private void StartFlyCoroutine()
    {
        _flyCoroutineHandle = Timing.RunCoroutine(FlyCoroutine());
    }

    private void StopFlyCoroutine()
    {
        if (_flyCoroutineHandle != null)
        {
            Timing.KillCoroutines(_flyCoroutineHandle);
        }
    }

    private void Fly()
    {
        Vector3 vel = _rigidBody.velocity;
        vel.y = 0f;
        _rigidBody.velocity = vel;
    }

    private bool AbovePlatform()
    {
        bool abovePlatform = false;
        Vector3 raycastOrigin = Penguin.Instance.transform.TransformPoint(new Vector3(0f, 2f, 0f));
        if (Physics.Raycast(raycastOrigin, Vector3.down, 100f, LayerMask.GetMask("Ground")))
        {
            abovePlatform = true;
        }
        return abovePlatform;
    }
}
