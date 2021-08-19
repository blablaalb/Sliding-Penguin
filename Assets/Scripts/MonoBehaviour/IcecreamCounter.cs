using System.Collections.Generic;
using UnityEngine;
using System;

public class IcecreamCounter : Singleton<IcecreamCounter>
{
    public int Collected { get; private set; }

    public event Action<Icecream> IcecreamCollected;

    public void CollectIcecream(Icecream icecream)
    {
        Collected += 1;
        IcecreamCollected?.Invoke(icecream);
    }
}
