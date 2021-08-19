using System.Collections.Generic;
using UnityEngine;
using System;
using MEC;

public class PenguinFSM : Singleton<PenguinFSM>
{
    [SerializeField]
    private PenguinState _currentState;
    [SerializeField]
    private AscendState _ascendState;
    [SerializeField]
    private FlyState _flyState;
    [SerializeField]
    private DescendState _descendState;
    [SerializeField]
    private LandState _landState;
    [SerializeField]
    private SlidingState _slidingState;
    [SerializeField]
    private OnGapState _onGapState;
    [SerializeField]
    private BellySlideUpState _slideOnBellyUpState;
    [SerializeField]
    private BellySlideHorizontal _slideOnBellyHorizontal;
    [SerializeField]
    private BellySlideStandUp _standUpFromBellySlideState;

    public Action<PenguinStates> StateChanged;
    public Action<JumpArea> Jumped;
    public Action<SlideArea> Slided;

    internal void Start()
    {
        EnterState(PenguinStates.Sliding);
    }

    internal void LateUpdate()
    {
        _currentState?.OnUpdate();
    }

    public void SetFly(bool fly)
    {
        _ascendState.SetFly(fly);
    }

    public void SetGap(Gap gap)
    {
        _onGapState.SetGap(gap);
    }

    /// <summary>
    /// Enters to the given state. If another state is running exits it.
    /// </summary>
    /// <param name="state">State to enter.</param>
    private void EnterState(PenguinState state)
    {
        if (_currentState != null)
        {
            ExitState();
        }
        _currentState = state;
        StateChanged?.Invoke(GetCurrentState());
        if (state == _ascendState)
        {
            GameObject go;
            if (OnGround(out go))
            {
                if (go.GetComponent<JumpArea>() is JumpArea jumpArea)
                {
                    Jumped?.Invoke(jumpArea);
                }
            }
        }
        else if (state == _slideOnBellyUpState)
        {
            GameObject go;
            if (OnGround(out go))
            {
                if (go.GetComponent<SlideArea>() is SlideArea slideArea)
                {
                    Slided?.Invoke(slideArea);
                }
            }
        }
        _currentState.Enter();
        // Debug.Log($"Entered the <b>{_currentState}</b>");
    }

    /// <summary>
    /// Enters to the given state. If another state is running exits it.
    /// </summary>
    /// <param name="state">State to enter.</param>
    public void EnterState(PenguinStates state)
    {
        switch (state)
        {
            case PenguinStates.Ascend:
                EnterState(_ascendState);
                break;
            case PenguinStates.Fly:
                EnterState(_flyState);
                break;
            case PenguinStates.Descend:
                EnterState(_descendState);
                break;
            case PenguinStates.Land:
                EnterState(_landState);
                break;
            case PenguinStates.Sliding:
                EnterState(_slidingState);
                break;
            case PenguinStates.OnGap:
                EnterState(_onGapState);
                break;
            case PenguinStates.SlideOnBellyUp:
                EnterState(_slideOnBellyUpState);
                break;
            case PenguinStates.SlideOnBellyHorizontal:
                EnterState(_slideOnBellyHorizontal);
                break;
            case PenguinStates.StandUpFromBellySlide:
                EnterState(_standUpFromBellySlideState);
                break;
            case PenguinStates.None:
                ExitState();
                break;

            default:
                Debug.LogError($"Unknown state: {state}");
                break;
        }
    }

    /// <summary>
    /// Exits current state.
    /// </summary>
    public void ExitState()
    {
        if (_currentState != null)
        {
            _currentState.Exit();
            _currentState = null;
        }
    }

    public PenguinStates GetCurrentState()
    {
        PenguinStates state = PenguinStates.None;
        if (_currentState == _ascendState)
        {
            state = PenguinStates.Ascend;
        }
        else if (_currentState == _descendState)
        {
            state = PenguinStates.Descend;
        }
        else if (_currentState == _flyState)
        {
            state = PenguinStates.Fly;
        }
        else if (_currentState == _landState)
        {
            state = PenguinStates.Land;
        }
        else if (_currentState == _slidingState)
        {
            state = PenguinStates.Sliding;
        }
        else if (_currentState == _slideOnBellyUpState)
        {
            state = PenguinStates.SlideOnBellyUp;
        }
        else if (_currentState == _slideOnBellyHorizontal)
        {
            state = PenguinStates.SlideOnBellyHorizontal;
        }
        else if (_currentState == _standUpFromBellySlideState)
        {
            state = PenguinStates.StandUpFromBellySlide;
        }
        else if (_currentState == _onGapState)
        {
            state = PenguinStates.OnGap;
        }
        return state;
    }

    private bool OnGround(out GameObject go)
    {
        return Penguin.Instance.IsGrounded(out go);
    }
}

public enum PenguinStates
{
    Ascend,
    Fly,
    Descend,
    Land,
    Sliding,
    OnGap,
    SlideOnBellyUp,
    SlideOnBellyHorizontal,
    StandUpFromBellySlide,
    None
}
