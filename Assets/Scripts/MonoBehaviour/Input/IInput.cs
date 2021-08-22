using System;

public interface IInput
{
    event Action<SwipeDirection> Swiped;
    float GetXAxis();
}