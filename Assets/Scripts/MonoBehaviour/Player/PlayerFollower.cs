using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerFollower : MonoBehaviour
{
    private Transform _target;
    private float _zOffset;

    // Start is called before the first frame update
    void Start()
    {
        _target = Penguin.Instance.transform;
        _zOffset = _target.position.z - transform.position.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, _target.position.z - _zOffset);
    }
}
