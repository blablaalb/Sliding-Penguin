using UnityEngine;
using System.Linq;

/// <summary>
/// This is a base class for all objects that are child of the Platform object (obstacle, accelerator, score  etc) .
/// </summary>
public class PlatformChild : MonoBehaviour
{
    protected PlatformChildOrigin _platformOrigin;

    protected virtual void Awake()
    {
        _platformOrigin = GetComponentInParent<PlatformChildOrigin>();
    }

    public virtual void SetPosition(Vector3 position)
    {
        _platformOrigin.SetPosition(position);
    }

    public virtual void SetRotation(Vector3 rotation)
    {
        _platformOrigin.SetRotation(rotation);
    }

    public virtual void SetRotation(Quaternion rotation)
    {
        _platformOrigin.SetRotation(rotation);
    }

    public void Rotate(float rotation)
    {
        _platformOrigin.RotateZ(rotation);
    }

    public virtual void SetParent(GameObject parent)
    {
        _platformOrigin.SetParent(parent);
    }

    public Bounds GetBounds()
    {
        return gameObject.GetComponentInChildren<MeshRenderer>().bounds;
    }
}