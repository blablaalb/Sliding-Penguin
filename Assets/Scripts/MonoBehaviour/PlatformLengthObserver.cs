using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLengthObserver : MonoBehaviour
{
    [Tooltip("After distance between the penguin and the last instentiated platform reaches this value a new platform will be added.")]
    [SerializeField]
    private float _distanceThreshold = 150f;

    public event Action ReachedThreshold;

    internal void OnDestroy()
    {
        ReachedThreshold = null;
    }

    internal void Update()
    {
        if (HasReachedThreshold())
        {
            ReachedThreshold?.Invoke();
        }
    }

    /// <summary>
    /// Calculated the distance between the penguin and the last generated platform and returns true if the distance is less or equal to the specified threshold.
    /// </summary>
    /// <returns>True if distance is less or equal to the threshold. False otherwise.</returns>
    public bool HasReachedThreshold()
    {
        Platform lastPlatform = PlatformGenerator.Instance.LastPlatform;
        if (lastPlatform != null)
        {
            Vector3 lastPlatformPosition = lastPlatform.Position;
            Vector3 penguinPosition = Penguin.Instance.Position;
            float distance = lastPlatformPosition.z - penguinPosition.z;
            if (distance <= _distanceThreshold)
            {
                return true;
            }
        }

        return false;
    }
}
