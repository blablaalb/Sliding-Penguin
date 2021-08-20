using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//By David

public class SmoothCameraFollow : Singleton<SmoothCameraFollow>
{
    private Vector3 Offset;
    [SerializeField]
    private float _followPower;
    [SerializeField]
    private float _sidePower;
    [SerializeField]
    private Transform _target;

    private float Side { get; set; }

    override protected void Awake()
    {
        base.Awake();
        Offset = transform.position - _target.position;
        Side = 0;
    }

    void FixedUpdate()
    {
        Side = InputManager.Instance.GetXAxis();
        Vector3 actualTarget = _target.position + Offset + new Vector3(Side * _sidePower, 0f, 0f);

        transform.position = Vector3.Lerp(transform.position, actualTarget, _followPower * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, _target.position.z + Offset.z);
    }
}
