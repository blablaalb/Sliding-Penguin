using System.Collections.Generic;
using UnityEngine;
using System;

public class CollectScoreOnScreenFXPool : GenericPool<CollectScoreOnScreenFX>
{
    [SerializeField]
    private Canvas _mainCanvas;

    public override CollectScoreOnScreenFX Get()
    {
        CollectScoreOnScreenFX scoreFX = base.Get();
        scoreFX.transform.SetParent(_mainCanvas.transform);
        return scoreFX;
    }
}
