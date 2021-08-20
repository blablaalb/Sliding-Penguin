using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _distance = 3.0f;
    [SerializeField]
    private float _height = 3.0f;
    [SerializeField]
    private float _damping = 5.0f;
    [SerializeField]
    private bool _smoothRotation = true;
    [SerializeField]
    private bool _followBehind = true;
    [SerializeField]
    private float _rotationDamping = 10.0f;

    private Quaternion _defaultRotation;

    private void Start()
    {
        _defaultRotation = transform.localRotation;
    }

    private void FixedUpdate()
    {
        Vector3 wantedPosition;
        if (_followBehind)
        {
            wantedPosition = _target.TransformPoint(0, _height, -_distance);
        }
        else
        {
            wantedPosition = _target.TransformPoint(0, _height, _distance);
        }

        transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * _damping);

        if (_smoothRotation)
        {
            // Quaternion wantedRotation = Quaternion.LookRotation(_target.position - transform.position, _target.up);
            Quaternion wantedRotation = _defaultRotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * _rotationDamping);
        }
        else
        {
            transform.LookAt(_target, _target.up);
        }
    }
}