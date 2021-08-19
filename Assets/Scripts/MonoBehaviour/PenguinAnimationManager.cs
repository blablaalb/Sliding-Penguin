using System.Collections.Generic;
using UnityEngine;
using System;

public class PenguinAnimationManager : Singleton<PenguinAnimationManager>
{
    [SerializeField]
    private Animator _penguinAnimator;
    private PlatformController _platformController;

    override protected void Awake()
    {
        base.Awake();
        _platformController = FindObjectOfType<PlatformController>();
        _platformController.PlatformRotated += OnPlatformMoved;
    }

    private void Start()
    {
        PenguinFSM.Instance.StateChanged += OnStateChanged;
    }

    override protected void OnDestroy()
    {
        _platformController.PlatformRotated -= OnPlatformMoved;
        if (PenguinFSM.Instance != null)
        {
            PenguinFSM.Instance.StateChanged -= OnStateChanged;
        }
        base.OnDestroy();
    }

    private void OnPlatformMoved(int x)
    {
        _penguinAnimator.SetInteger("Direction", x);
    }

    private void OnStateChanged(PenguinStates state)
    {
        switch (state)
        {
            case PenguinStates.Ascend:
                Jump();
                break;
            case PenguinStates.Descend:
                InAir();
                break;
            case PenguinStates.Fly:
                Fly();
                break;
            case PenguinStates.Land:
                Land();
                break;
            case PenguinStates.SlideOnBellyUp:
                SlideOnBelly();
                break;
            case PenguinStates.StandUpFromBellySlide:
                StandUpFromBellySlide();
                break;

            default:
                break;
        }
    }

    public void Jump()
    {
        // Debug.Log("<color=blue>Jump</color>");
        _penguinAnimator.CrossFade("Base Layer.Jump", 0.2f);
    }

    public void Fly()
    {
        // Debug.Log("<color=blue>Fly</color>");
        _penguinAnimator.CrossFade("Base Layer.Fly", 0.9f);
    }

    public void InAir()
    {
        // Debug.Log("<color=blue>In Air</color>");
        _penguinAnimator.CrossFade("Base Layer.InAir", 0.2f);
    }

    public void Land()
    {
        // Debug.Log("<color=blue>Land</color>");
        _penguinAnimator.CrossFade("Base Layer.Land", 0.2f);
    }

    public void SlideOnBelly()
    {
        // Debug.Log("<color=blue>Belly_Sliding_Start</color>");
        _penguinAnimator.CrossFade("Base Layer.Belly_Sliding_Start", 0.1f);
    }

    public void StandUpFromBellySlide()
    {
        // Debug.Log("<color=blue>Belly_Sliding_End</color>");
        _penguinAnimator.CrossFade("Base Layer.Belly_Sliding_End", 0.2f);
    }
}
