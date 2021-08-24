using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;

public class Invincibility : MonoBehaviour
{
    [SerializeField]
    private bool _invincible;
    private ParticleSystem _particle;

    public bool Invincible => _invincible;
}