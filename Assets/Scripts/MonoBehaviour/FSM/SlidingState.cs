using UnityEngine;
using System;

[Serializable]
public class SlidingState : PenguinState
{
    private bool _run;
    private float _rayPositionYOffset = 2f;

    public override void Enter()
    {
        // Debug.Log("<color=green>Entering Sliding</color>");
        _run = true;
    }

    public override void Exit()
    {
        // Debug.Log("<color=red>Exiting Sliding</color>");
        _run = false;
    }

    public override void OnUpdate()
    {
        if (!_run)
        {
            return;
        }

        GameObject ground = null;
        GameObject forwardGO;
        if (OnGround(out ground))
        {
            if (ground.GetComponent<JumpArea>() is JumpArea jumpArea)
            {
                if (!jumpArea.Jumped)
                {
                    PenguinFSM.Instance.SetFly(false);
                    PenguinFSM.Instance.EnterState(PenguinStates.Ascend);
                }
            }
            // else if (ground.GetComponent<SlideArea>() is SlideArea slideArea)
            // {
            //     if (!slideArea.Slided)
            //     {
            //         PenguinFSM.Instance.EnterState(PenguinStates.SlideOnBellyUp);
            //     }
            // }


            else if (RaycastForward(3f, out forwardGO))
            {
                if (forwardGO.GetComponent<SlideArea>() is SlideArea slideArea)
                {
                    if (!slideArea.Slided)
                    {
                        PenguinFSM.Instance.EnterState(PenguinStates.SlideOnBellyUp);
                    }
                }
            }

        }
        else
        {
            if (!OnGround(out ground))
            {
                if (ground == null)
                {
                    Gap gap;
                    if (OnGap(out gap))
                    {
                        PenguinFSM.Instance.SetGap(gap);
                        PenguinFSM.Instance.EnterState(PenguinStates.OnGap);
                    }
                    else if (AboveVoid())
                    {
                        // if (Penguin.Instance.Position.y < -2f)
                        // {
                            PenguinFSM.Instance.SetFly(true);
                            PenguinFSM.Instance.EnterState(PenguinStates.Ascend);
                        // }
                    }
                }
            }
        }
    }

    private bool OnGap(out Gap gap)
    {
        bool onGap = false;
        gap = null;
        Vector3 origin = Penguin.Instance.transform.TransformPoint(new Vector3(0f, _rayPositionYOffset, 0f));
        RaycastHit hit;
        if (Physics.Raycast(origin, Vector3.down, out hit, _rayPositionYOffset + 0.01f, LayerMask.GetMask("Gap")))
        {
            gap = hit.collider.gameObject.GetComponent<Gap>();
            onGap = true;
        }
        return onGap;
    }

    private bool AboveVoid()
    {
        Vector3 origin = Penguin.Instance.transform.TransformPoint(new Vector3(0f, 2f, 0f));
        return !Physics.Raycast(origin, Vector3.down, 100f, LayerMask.GetMask("Ground", "Gap"));
    }

    private PenguinStates GetCurrentState()
    {
        return PenguinFSM.Instance.GetCurrentState();
    }

    private bool OnGround(out GameObject go)
    {
        return Penguin.Instance.IsGrounded(out go);
    }

    private bool RaycastForward(float rayLength, out GameObject go)
    {
        bool hit = false;
        go = null;
        RaycastHit raycastHit;
        Vector3 rayOrigin = Penguin.Instance.ColliderCenter();
        if (Physics.Raycast(rayOrigin, Penguin.Instance.transform.forward, out raycastHit, rayLength, LayerMask.GetMask("Ground")))
        {
            hit = true;
            go = raycastHit.collider.gameObject;
            Debug.DrawLine(rayOrigin, raycastHit.point, Color.red, 100000f);
        }

        return hit;
    }
}