using System.Collections.Generic;
using UnityEngine;
using System;

public class PropAnimator : MonoBehaviour
{
    [SerializeField]
    private float _movementDistance;
    [SerializeField]
    private float _movementTime;
    [SerializeField]
    private float _rotationAngle;
    [SerializeField]
    private bool _aniamteUpDown;
    [SerializeField]
    private bool _animateRotation;
    private Vector3 _startingLocalPosition;

    internal void Start()
    {
        _startingLocalPosition = transform.localPosition;
        if (_aniamteUpDown)
            AnimateUpDown();
    }

    private void FixedUpdate()
    {
        if (_animateRotation)
            AnimateRotation();
    }

    private void AnimateUpDown()
    {
        LeanTween.moveLocalY(gameObject, _startingLocalPosition.y + _movementDistance, _movementTime).setLoopType(LeanTweenType.pingPong);
    }

    private void AnimateRotation()
    {
        transform.Rotate(new Vector3(0f, _rotationAngle, 0f), Space.World);
    }

}
