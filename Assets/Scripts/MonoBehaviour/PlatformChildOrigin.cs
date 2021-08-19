using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformChildOrigin : MonoBehaviour
{
    public void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    public void SetRotation(Vector3 rotation)
    {
        transform.localRotation = Quaternion.Euler(rotation);
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetParent(GameObject parent)
    {
        transform.SetParent(parent.transform);
    }

    public void RotateZ(float z)
    {
        transform.Rotate(new Vector3(0f, 0f, z));
    }
}
