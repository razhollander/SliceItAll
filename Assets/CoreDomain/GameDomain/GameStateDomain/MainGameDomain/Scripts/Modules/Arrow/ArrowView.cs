using System;
using UnityEngine;

public class ArrowView : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    
    private Action<Collision> _onCollisionEnter;
    private Action<Collider> _onTriggerEnter;

    public void Setup(Action<Collision> onCollisionEnter, Action<Collider> onTriggerEnter, float angularDrag)
    {
        _onCollisionEnter = onCollisionEnter;
        _onTriggerEnter = onTriggerEnter;
        _rigidbody.angularDrag = angularDrag;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _onCollisionEnter?.Invoke(collision);
    }
 
    private void OnTriggerEnter(Collider otherCollider)
    {
        _onTriggerEnter?.Invoke(otherCollider);
    }

    public void SetZAngularVelocity(float angularVelocity)
    {
        _rigidbody.angularVelocity = new Vector3(0, 0, angularVelocity);
    }
    
    public void SetZRotation(float angle)
    {
        _rigidbody.rotation = Quaternion.Euler(0, 0, angle);

    }

    public void FreezeMovement( bool isDisableGravity, bool isEnableKinematic)
    {
        SetVelocity(Vector3.zero);
        SetAngularVelocity(Vector3.zero);
        
        if (isDisableGravity)
        {
            SetGravity(false);
        } 
        
        if (isEnableKinematic)
        {
            SetIsKinematic(true);
        }
    }

    public float GetZRotation()
    {
        return _rigidbody.rotation.eulerAngles.z;
    }

    public void SetGravity(bool isEnabled)
    {
        _rigidbody.useGravity = isEnabled;
    }

    public void SetAngularVelocity(Vector3 velocity)
    {
        _rigidbody.angularVelocity = velocity;
    }

    public void SetVelocity(Vector3 velocity)
    {
        _rigidbody.velocity = velocity;
    }

    public void SetIsKinematic(bool isEnabled)
    {
        _rigidbody.isKinematic = isEnabled;
    }
}
