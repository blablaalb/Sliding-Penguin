using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BellySlideUpState : PenguinState
{
    private bool _rotate;
    private Rigidbody _rigidBody;
    [SerializeField]
    private Vector3 _rotateAngleVelocity = new Vector3(-45f, 0f, 0f);
    private float _targetXRotation = -45f;

    override public void Enter()
    {
        if (_rigidBody == null)
        {
            _rigidBody = Penguin.Instance.GetComponent<Rigidbody>();
        }
        _rotate = true;
        _rigidBody.useGravity = false;
    }

    override public void OnUpdate()
    {
        if (_rotate == true)
        {
            float currentXRotation = GetCurrentRotation().x;
            if (currentXRotation > _targetXRotation)
            {
                Rotate45DegressUp();
            }
            else
            {
                if (!Penguin.Instance.IsGrounded())
                {
                    PenguinFSM.Instance.EnterState(PenguinStates.SlideOnBellyHorizontal);
                }
            }
        }
    }

    override public void Exit()
    {
        _rotate = false;
    }

    private void Rotate45DegressUp()
    {
        Quaternion deltaRotation = Quaternion.Euler(_rotateAngleVelocity * Time.deltaTime);
        _rigidBody.MoveRotation(_rigidBody.rotation * deltaRotation);
    }

    private Vector3 GetCurrentRotation()
    {
        Vector3 rotation = _rigidBody.rotation.eulerAngles;
        float x = rotation.x;
        rotation.x = (x > 180) ? x - 360f : x;
        return rotation;
    }
}