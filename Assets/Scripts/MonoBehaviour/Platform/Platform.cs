using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private float _childMinRotation = -25f;
    [SerializeField]
    private float _childMaxRotation = 25f;

    public Vector3 Position => transform.position;
    public Quaternion Rotation => transform.rotation;

    public const float LENGTH = 13.2f;

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetRotation(Vector3 rotation)
    {
        transform.rotation = Quaternion.Euler(rotation);
    }

    public void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    public void SetParent(GameObject parent)
    {
        transform.SetParent(parent.transform);
    }

    public void PlaceChild(PlatformChild child)
    {
        Vector3 position = transform.TransformPoint(GetRandomLocalPosition());
        child.SetParent(gameObject);
        child.SetRotation(transform.rotation);
        float zRot = GetRandomRotation();
        child.SetPosition(position);
        child.Rotate(zRot);
    }

    /// <summary>
    /// Adds the length of the platform to the platform's global position and returns it.
    /// </summary>
    /// <returns>Global position of the platofrm's end.</returns>
    public Vector3 CalculatePlatformEnd()
    {
        Vector3 end = transform.position + LENGTH * transform.forward;
        return end;
    }

    private float GetRandomRotation()
    {
        float r = UnityEngine.Random.Range(_childMinRotation, _childMaxRotation);
        return r;
    }

    private Vector3 GetRandomLocalPosition()
    {
        Vector3 position = Vector3.zero;
        position.z = UnityEngine.Random.Range(0f, LENGTH);
        return position;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
