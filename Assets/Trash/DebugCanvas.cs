using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

// Only for debugging purposes. Remvoe afterwards.
public class DebugCanvas : Singleton<DebugCanvas>
{
    [SerializeField]
    private TextMeshProUGUI _swipedText;
    [SerializeField]
    private TextMeshProUGUI _minMaxRotText;

    public void SetSwipeText(bool swiped)
    {
        _swipedText.SetText($"Swiped: {swiped}");
    }

    public void SetRotationText(float minRot, float maxRot)
    {
        _minMaxRotText.SetText($"Min: {minRot}, Max: {maxRot}");
    }
}
