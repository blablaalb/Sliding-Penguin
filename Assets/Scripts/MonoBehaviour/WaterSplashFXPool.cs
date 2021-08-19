using System.Collections.Generic;
using UnityEngine;
using System;

public class WaterSplashFXPool : GenericPool<WaterSplashFX>
{
    public override WaterSplashFX Get()
    {
        WaterSplashFX waterSplashFX = base.Get();
        waterSplashFX.transform.SetParent(this.gameObject.transform);
        return waterSplashFX;
    }
}
