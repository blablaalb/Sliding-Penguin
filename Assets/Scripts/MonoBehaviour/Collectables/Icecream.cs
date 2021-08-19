using System.Collections.Generic;
using UnityEngine;
using System;
using MEC;

/// <summary>
/// Icecream makes the player invincible for few seconds.
/// </summary>
public class Icecream : PlatformChild, ICollectable
{
    [SerializeField]
    private float _invincibilityDuration = 2f;

    public void CollectSelf()
    {
        IcecreamCounter.Instance.CollectIcecream(this);
        gameObject.SetActive(false);
    }

    private IEnumerator<float> BecameInvincibleCoroutine()
    {
        yield return Timing.WaitForSeconds(_invincibilityDuration);
    }

    private void StartInvincibility()
    {
        Debug.Log("I'm invincible");
    }
    private void EndInvincibility() { }

}
