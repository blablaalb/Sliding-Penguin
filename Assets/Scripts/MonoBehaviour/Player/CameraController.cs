using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform _leftFlippedPosition;
    [SerializeField]
    private Transform _rightFlippedPosition;
    [SerializeField]
    private Transform _leftEdgePosition;
    [SerializeField]
    private Transform _rightEdgePosition;
    [SerializeField]
    private Transform _middlePosition;
    [SerializeField]
    private float _transitionTime = 1.5f;
    private PlatformController _platformController;

    internal void Awake()
    {
        _platformController = FindObjectOfType<PlatformController>();
    }

    internal void Start()
    {
        _platformController.Flipped += OnPlatformFlipped;
        _platformController.PlatformRotated += OnPlatformRotated;
    }

    internal void OnDestroy()
    {
        if (_platformController != null)
        {
            _platformController.Flipped -= OnPlatformFlipped;
            _platformController.PlatformRotated -= OnPlatformRotated;
        }
    }

    private void OnPlatformFlipped(int direction)
    {
        if (direction == -1) OnFlippedLeft();
        else
        if (direction == 1) OnFlippedRight();
    }

    private void OnPlatformRotated(int direction)
    {
        if (_platformController.TargetRotation == 80f) OnMovedLeft();
        else
        if (_platformController.TargetRotation == -80f) OnMovedRight();
        else
        if (_platformController.TargetRotation == 0f) MoveMiddle();
    }

    private void OnFlippedLeft()
    {
        LeanTween.cancel(this.gameObject);
        LeanTween.moveLocal(this.gameObject, _leftFlippedPosition.localPosition, _transitionTime);
        LeanTween.rotateLocal(this.gameObject, _leftFlippedPosition.localRotation.eulerAngles, _transitionTime);
    }

    private void OnFlippedRight()
    {
        LeanTween.cancel(this.gameObject);
        LeanTween.moveLocal(this.gameObject, _rightFlippedPosition.localPosition, _transitionTime);
        LeanTween.rotateLocal(this.gameObject, _rightFlippedPosition.localRotation.eulerAngles, _transitionTime);
    }

    private void OnMovedRight()
    {
        LeanTween.cancel(this.gameObject);
        LeanTween.moveLocal(this.gameObject, _rightEdgePosition.localPosition, _transitionTime);
        LeanTween.rotateLocal(this.gameObject, _rightEdgePosition.localRotation.eulerAngles, _transitionTime);
    }

    private void OnMovedLeft()
    {
        LeanTween.cancel(this.gameObject);
        LeanTween.moveLocal(this.gameObject, _leftEdgePosition.localPosition, _transitionTime);
        LeanTween.rotateLocal(this.gameObject, _leftEdgePosition.localRotation.eulerAngles, _transitionTime);
    }

    private void MoveMiddle()
    {
        LeanTween.cancel(this.gameObject);
        LeanTween.moveLocal(this.gameObject, _middlePosition.localPosition, _transitionTime);
        LeanTween.rotateLocal(this.gameObject, _middlePosition.localRotation.eulerAngles, _transitionTime);
    }
}
