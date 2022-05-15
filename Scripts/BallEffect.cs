using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEffect : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform rotateTransform, scaleTransform;

    [SerializeField] private float stretchMultiplier = 0.005f;
    [SerializeField] private float squashMultiplier = 0.06f;
    [SerializeField] private float delayMultiplier = 0.2f;
    [SerializeField] private float scaleChangeRate = 20f;

    private Quaternion targetRotation, currentRotation;

    private float currentScale = 1f;
    private float targetScale = 1f;
    private Vector3 savedVelocity;
    private Vector3 savedContactNormal;
    private bool ground = false;
    private bool inverted;

    private void LateUpdate()
    {
        if (!ground)
        {
            targetRotation = Quaternion.LookRotation(rb.velocity, Vector3.forward);
            float _velocity = rb.velocity.magnitude;
            targetScale = 1f + _velocity * _velocity * stretchMultiplier;
            targetScale = Mathf.Clamp(targetScale, 1f, 2f);
        }

        currentScale = Mathf.Lerp(currentScale, targetScale, Time.deltaTime * scaleChangeRate);

        SquashScale(currentScale);

        if(!inverted && currentScale >= 1f)
        {
            inverted = true;
            rotateTransform.rotation = targetRotation = currentRotation = Quaternion.LookRotation(savedContactNormal, Vector3.forward);
        }

        currentRotation = Quaternion.Lerp(currentRotation, targetRotation, Time.deltaTime * 10f);
        rotateTransform.rotation = currentRotation;
    }

    private void SquashScale(float _value)
    {
        if (_value == 0) return;
        scaleTransform.localScale = new Vector3(1 / _value, _value, 1 / _value);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (ground) return;
        ground = true;

        savedVelocity = rb.velocity;
        savedContactNormal = collision.contacts[0].normal;
        rb.isKinematic = true;

        targetRotation = Quaternion.LookRotation(-collision.contacts[0].normal, Vector3.forward);

        targetScale = Mathf.Lerp(1f, 0.3f, savedVelocity.magnitude * squashMultiplier);

        float _velocityProjectionMagnitude = Vector3.Project(savedVelocity, -savedContactNormal).magnitude;
        float _groundedTime = _velocityProjectionMagnitude * delayMultiplier;
        _groundedTime = Mathf.Clamp(_groundedTime, 0f, 0.15f);

        transform.position = collision.contacts[0].point + collision.contacts[0].normal * 0.5f;

        Invoke(nameof(StartToStretch), _groundedTime);
        Invoke(nameof(DisableIsKinematic), _groundedTime * 1.5f);
    }

    private void StartToStretch()
    {
        targetScale = Mathf.Lerp(0.5f, 1f, 1f + savedVelocity.magnitude * stretchMultiplier);
        inverted = false;
    }
    private void DisableIsKinematic()
    {
        rb.isKinematic = false;
        Invoke(nameof(ExitSaveMove), 0.02f);
    }
    private void ExitSaveMove()
    {
        ground = false;
        rb.velocity = Vector3.Reflect(savedVelocity, savedContactNormal);
    }
}