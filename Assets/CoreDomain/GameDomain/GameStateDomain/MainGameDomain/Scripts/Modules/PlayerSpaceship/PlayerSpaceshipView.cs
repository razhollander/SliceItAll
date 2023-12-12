using System;
using UnityEngine;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerSpaceship
{
    public class PlayerSpaceshipView : MonoBehaviour
    {
        [SerializeField] private Transform _rendererTransform;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _rotationLerpFactor = 5f;
        [SerializeField] private ThrusterView[] _thrusters;
        
        private float _currentZRotation = 0;
        private Action<Collider> _onCollisionEnter;

        public void SetupCallbacks(Action<Collider> onCollisionEnter)
        {
            _onCollisionEnter = onCollisionEnter;
        }

        private void OnTriggerEnter(Collider collision)
        {
            _onCollisionEnter?.Invoke(collision);
        }

        private void RotateOnZAxis(float zRotation)
        {
            _rendererTransform.rotation = Quaternion.Euler(0,0,zRotation);
        }

        public void SetVelocity(float xVelocity)
        {
            _rigidbody.velocity = new Vector2(xVelocity, 0);
        }

        public void LerpToRotation(float rotationInDegrees)
        {
            var newZRotation = Mathf.Lerp(_currentZRotation, rotationInDegrees, _rotationLerpFactor * Time.deltaTime);
            _currentZRotation = newZRotation;
            RotateOnZAxis(newZRotation);
        }

        public void EnableThruster(bool isEnabled)
        {
            foreach (var thrusterView in _thrusters)
            {
                thrusterView.EnabledParticles(isEnabled);
            }
        }
        
        public void EnableThrusterBoost(bool isEnabled)
        {
            foreach (var thrusterView in _thrusters)
            {
                thrusterView.EnableBoost(isEnabled);
            }
        }
        
        public void SetRotation(float rotationInDegrees)
        {
            _currentZRotation = rotationInDegrees;
            RotateOnZAxis(_currentZRotation);
        }
    }
}