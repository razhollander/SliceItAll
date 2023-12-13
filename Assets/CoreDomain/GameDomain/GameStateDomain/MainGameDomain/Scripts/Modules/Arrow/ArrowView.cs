using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

public class ArrowView : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _jumpForce=5;
    [SerializeField] private float _jumpAngleRelativeToFloor = 60;
    [SerializeField] private Transform _rendererTransform;
    [SerializeField] private float _startRotationLoopSpeed = 1;
    [SerializeField] private float _spacePressedRotationLoopSpeed = 1;
    [SerializeField] private float _angularDrag = 5;
    [SerializeField] private Collider _arrowHeadCollider;
    [SerializeField] private float _maxStabAngleWithSurface = 60;
    [SerializeField] private float _shootAngleRelativeToFloor = -75;
    [SerializeField] private float _shootRotationDuration = 0.5f;
    [SerializeField] private float _shootVelocity = 10f;
    //[SerializeField] private float _minimalStabAngle = 45;
    private float _prevZRotation;
    private bool _canStabInCurrentLoop = false;
    
    private void Awake()
    {
        Debug.Log("Target");
        _rigidbody.angularDrag = _angularDrag;
        Camera.main.GetComponent<SmoothFollow>().SetTarget(transform);
        //_arrowHeadCollider.on
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Enter! {collision.contacts.Length}");
        var isCollisionPopable = collision.transform.GetComponent<PopableView>() != null;
        
        if (_canStabInCurrentLoop && !isCollisionPopable && collision.contacts.Length > 0 && DidStabContactPoint(collision.contacts[0]))
        {
            Debug.Log("Stab!");
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.isKinematic = true;
            _canStabInCurrentLoop = false;
        }
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        Debug.Log("Trigger enter");
        var otherPopableView = otherCollider.GetComponent<PopableView>();

        if (otherPopableView != null)
        {
            otherPopableView.Pop();
        }
    }

    private bool DidStabContactPoint(ContactPoint contactPoint)
    {
        Vector3 contactPointNormal = contactPoint.normal;
        Vector3 hitVector = -transform.right;
        Debug.Log($"contactPointNormal {contactPointNormal}, hitVector {hitVector}, angle {Vector3.Angle(hitVector, contactPointNormal)}");
        return Vector3.Angle(hitVector, contactPointNormal) < _maxStabAngleWithSurface;
    }

    void Update()
    {
#if UNITY_EDITOR
        // for live testing
        _rigidbody.angularDrag = _angularDrag;
#endif

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Shoot().Forget();
        }
    }

    private async UniTask Shoot()
    {
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.useGravity = false;
        var shootVector = Quaternion.Euler(0, 0, _shootAngleRelativeToFloor) *Vector3.right;

        await DOTween.To(
            () => ConvertAngleToBeBetween0To360(_rigidbody.rotation.eulerAngles.z)-_shootAngleRelativeToFloor,
            x => _rigidbody.rotation = ConvertAngleBetween0To360ToQuaternion(x+_shootAngleRelativeToFloor),
            0, _shootRotationDuration);
        //await _rigidbody.rotation = shootVector, _shootRotationDuration);
        _rigidbody.useGravity = true;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.velocity = shootVector * _shootVelocity;
    }

    private Quaternion ConvertAngleBetween0To360ToQuaternion(float angle)
    {
        return Quaternion.Euler(0, 0, angle);
    }
    private void FixedUpdate()
    {
        var currentZRotation = ConvertAngleToBeBetween0To360(transform.eulerAngles.z);
        var shouldStartANewLoop = _prevZRotation is < 280 and > 270 && currentZRotation is > 260 and <= 270;
        
        if (shouldStartANewLoop)
        {
            SetLoopAngularVelocity();
        }

        if (!_canStabInCurrentLoop)
        {
            var minimalStabAngle = 90f;
            var didPassMinimalStabAngle = _prevZRotation < minimalStabAngle+10f && _prevZRotation > minimalStabAngle && currentZRotation > minimalStabAngle-10 && currentZRotation <= minimalStabAngle;

            if (didPassMinimalStabAngle)
            {
                _canStabInCurrentLoop = true;
            }
        }

        _prevZRotation = currentZRotation;
    }

    private float ConvertAngleToBeBetween0To360(float angle)
    {
        var between0To360 = angle % 360f;
         
        if (between0To360 < 0)
        {
            between0To360 += 360;
        }

        return between0To360;
    }

    private void Jump()
    {
        _rigidbody.isKinematic = false;
        SetJumpVelocity();
        SetJumpAngularVelocity();
    }

    private void SetJumpAngularVelocity()
    {
        SetZAngularVelocity(_spacePressedRotationLoopSpeed);
    }
    
    private void SetLoopAngularVelocity()
    {
        SetZAngularVelocity(_startRotationLoopSpeed);
    }

    private void SetZAngularVelocity(float angularVelocity)
    {
        _rigidbody.angularVelocity = new Vector3(0, 0, angularVelocity);
    }
    
    private void SetJumpVelocity()
    {
        _rigidbody.velocity = Quaternion.Euler(0, 0, _jumpAngleRelativeToFloor) * Vector3.right * _jumpForce;
    }
}
