using System;
using UnityEngine;

public class ArrowView : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private ParticleSystem _thrusterParticleSystem;
    
    private Action<Collision> _onCollisionEnter;
    private Action<Collider> _onTriggerEnter;
    private Action<ParticleSystem> _onParticleCollisionEnter;

    public void SetupCallbacks(Action<Collision> onCollisionEnter, Action<Collider> onTriggerEnter, Action<ParticleSystem> onParticleCollisionEnter)
    {
        _onCollisionEnter = onCollisionEnter;
        _onTriggerEnter = onTriggerEnter;
        _onParticleCollisionEnter = onParticleCollisionEnter;
    }

    public void SetAngularDrag(float angularDrag)
    {
        _rigidbody.angularDrag = angularDrag;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _onCollisionEnter?.Invoke(collision);
    }
    
    private void OnParticleCollision(GameObject particleSystemGO)
    {
        _onParticleCollisionEnter(particleSystemGO.GetComponent<ParticleSystem>());
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

    public void FreezeMovement(bool isDisableGravity, bool isEnableKinematic)
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
    
    public void EnableThruster(bool isEnabled)
    {
        if (isEnabled)
        {
            _thrusterParticleSystem.Play();
        }
        else
        {
            _thrusterParticleSystem.Stop();
        }
    }
}
