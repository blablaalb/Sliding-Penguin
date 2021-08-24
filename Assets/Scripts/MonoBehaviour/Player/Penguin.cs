using System.Collections.Generic;
using UnityEngine;
using System;
using MEC;

public class Penguin : Singleton<Penguin>
{
    [SerializeField]
    private float _force = 1;
    [SerializeField]
    private ForceMode _forceMode;
    private Rigidbody _rigidBody;
    [SerializeField]
    private float _jumpForce;
    private bool _stopped;
    private float _rayRadius;
    private Invincibility _invincibility;

    public Vector3 Position => transform.position;
    public Vector3 Velocity => _rigidBody.velocity;

    override protected void Awake()
    {
        base.Awake();
        SphereCollider collider = GetComponent<SphereCollider>();
        _rigidBody = GetComponent<Rigidbody>();
        _rayRadius = collider.radius;
        _invincibility = GetComponent<Invincibility>();
        Obstacle.ObstacleHit += OnObstacleHit;
    }

    override protected void OnDestroy()
    {
        Obstacle.ObstacleHit -= OnObstacleHit;
    }

    internal void FixedUpdate()
    {
        if (_stopped)
        {
            return;
        }
        MoveForward();
    }

    internal void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<ICollectable>() is ICollectable collectable)
        {
            CollectCollectable(collectable);
        }
    }

    private void CollectCollectable(ICollectable collectable)
    {
        collectable.CollectSelf();
    }

    private void OnObstacleHit(Obstacle obstacle)
    {
        if (!_invincibility.Invincible)
            Stop();
    }

    private void MoveForward()
    {
        // Vector3 velocity = _rigidBody.velocity;
        // velocity.z = _force;
        // _rigidBody.velocity = velocity;
        _rigidBody.MovePosition(transform.position + (transform.forward * _force) * Time.deltaTime);
    }

    public void AddForce()
    {
        _rigidBody.AddForce(transform.forward * _force, _forceMode);
    }

    public void Jump()
    {
        Vector3 vel = _rigidBody.velocity;
        vel.y = _jumpForce;
        _rigidBody.velocity = vel;
    }

    public void Accelerate(float acceleration)
    {
        _rigidBody.AddForce(transform.forward * (acceleration + _force), ForceMode.Impulse);
    }

    public void Stop()
    {
        _stopped = true;
        _rigidBody.velocity = Vector3.zero;
    }

    public bool IsGrounded(out GameObject ground)
    {
        // Debug.Log("<color=yellow>Checking for ground</color>");
        ground = null;
        bool grounded = false;
        //NOTE: casting a ray from center of a spehre causes strange behaviour. We may report this to Unity.
        float offset = _rayRadius * 2f;
        Vector3 raycastPosition = transform.TransformPoint(new Vector3(0f, offset, 0f));
        float rayLength = offset + 0.01f;
        RaycastHit sphereCastHit;
        if (Physics.SphereCast(origin: raycastPosition, radius: _rayRadius, direction: Vector3.down, hitInfo: out sphereCastHit, maxDistance: rayLength, layerMask: LayerMask.GetMask("Ground")))
        {
            ground = sphereCastHit.collider.gameObject;
            grounded = true;
            Debug.DrawLine(raycastPosition, sphereCastHit.point, Color.red, 60f);
            // Debug.Break();
        }

        return grounded;
    }

    public bool IsGrounded()
    {
        GameObject go = null;
        return IsGrounded(out go);
    }

    public Vector3 ColliderCenter()
    {
        Vector3 center = GetComponent<SphereCollider>().center;
        center = transform.TransformPoint(center);
        return center;
    }
}

