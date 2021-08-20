using UnityEngine;
using System;

[Serializable]
public class BellySlideHorizontal : PenguinState
{
    private Rigidbody _rigidBody;
    private bool _rotate;
    private bool _entered;
    [SerializeField, Range(0f, 5f)]
    private float _maxJumpHeight = 1f;

    public override void Enter()
    {
        if (_rigidBody == null)
        {
            _rigidBody = Penguin.Instance.GetComponent<Rigidbody>();
        }

        _rotate = true;
        _entered = true;
    }

    public override void OnUpdate()
    {
        if (_entered)
        {
            if (_rotate)
            {
                Rotate();
                _rotate = false;
            }
            else
            {
                if (Penguin.Instance.IsGrounded())
                {
                    PenguinFSM.Instance.EnterState(PenguinStates.StandUpFromBellySlide);
                }
            }
        }

        if (Penguin.Instance.Position.y > _maxJumpHeight)
        {
            _rigidBody.useGravity = true;
        }
    }

    public override void Exit()
    {
        _rotate = false;
        _entered = false;
        _rigidBody.useGravity = true;
        // Assign rotation in case the penguin for some reason didn't rotate completely.
        _rigidBody.rotation = Quaternion.Euler(Vector3.zero);
    }

    private Vector3 GetCurrentRotation()
    {
        Vector3 rotation = _rigidBody.rotation.eulerAngles;
        float x = rotation.x;
        rotation.x = (x > 180) ? x - 360f : x;
        return rotation;
    }

    /// <summary>
    /// Rotates rigid body with leantween.
    /// Enables gravity on rigid body after rotation is completed.
    /// </summary>
    private void Rotate()
    {
        float currentXRotation = GetCurrentRotation().x;
        LeanTween.value(Penguin.Instance.gameObject, currentXRotation, 0f, 0.6f).setOnUpdate(
            (float val) =>
            {
                Vector3 newRotation = new Vector3(val, 0f, 0f);
                Quaternion rot = Quaternion.Euler(newRotation);
                _rigidBody.rotation = rot;
            }
         ).setOnComplete(() =>
         {
             _rigidBody.useGravity = true;
         });
    }

}