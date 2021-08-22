using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;

public class KeyboardInput : Singleton<KeyboardInput>, IInput
{
    public event Action<SwipeDirection> Swiped;

    public float GetXAxis()
    {
        if (Input.GetKeyDown(KeyCode.A)) return -1;
        else
        if (Input.GetKeyDown(KeyCode.D)) return 1;
        else return 0;
    }

    internal void Update()
    {
        // Simulate swipe on keyboard.
        if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Swiped?.Invoke(SwipeDirection.Right);
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Swiped?.Invoke(SwipeDirection.Left);
            }
        }
    }
}