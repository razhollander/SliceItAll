using System;
using System.Collections;
using System.Collections.Generic;
using CoreDomain.Scripts.Extensions;
using UnityEngine;
using DG.Tweening;
public class ArrowView : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _jumpForce=5;
    [SerializeField] private float _jumpAngleRelativeToFloor = 60;
    [SerializeField] private Transform _rendererTransform;
    [SerializeField] private float _endZRotationAngleRelativeToFloor = -75;
    [SerializeField] private float _currentZRotation;
    [SerializeField] private float _destinationZRotation;
    [SerializeField] private float _startRotationLoopSpeed = 1;
    [SerializeField] private float _spacePressedRotationLoopSpeed = 1;
    [SerializeField] private float _angularDrag = 5;
    [SerializeField] private float _angularDragPower = 1f;

    private bool _didPassCriticalAngleInCurrentLoop = false;
    
    private float _prevZRotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.angularDrag = _angularDrag;

        var _currentZRotation = transform.eulerAngles.z;
        var between0To360 = _currentZRotation % 360f;
         
        if (between0To360 < 0)
        {
            between0To360 += 360;
        }

        bool shouldStartANewLoop = 280 >_prevZRotation && _prevZRotation > 270 && 260 < between0To360 && between0To360 < 270;
        
        if (shouldStartANewLoop)
        {
            Debug.Log($"Start! _prevZRotation: {_prevZRotation}, between0To360: {between0To360}");
            _rigidbody.angularVelocity = new Vector3(0, 0, _startRotationLoopSpeed);
        }
        
        //if (!_rigidbody.angularVelocity.z.EqualsWithTolerance(0) && between0To360 < 180 && between0To360 > 0)
        //{
        //    Debug.Log($"angle: {between0To360}");
        //    //_rigidbody.angularDrag = Mathf.Lerp(180 - between0To360, 180, _angularDrag) /180 * _angularDragPower;
        //    _rigidbody.angularDrag = Mathf.Pow((180 - between0To360)/180, _angularDrag)  * _angularDragPower;
        //}

        _prevZRotation = between0To360;
    }

    private void DoRotateLoop()
    {
        _rigidbody.angularVelocity = new Vector3(0, 0, _spacePressedRotationLoopSpeed);
    }
    
    private void Jump()
    {
        _rigidbody.velocity = CalcJumpVector();
        DoRotateLoop();

        //_rigidbody.AddForce(CalcJumpVector());
    }

    private Vector3 CalcJumpVector()
    {
        return Quaternion.Euler(0, 0, _jumpAngleRelativeToFloor) * Vector3.right * _jumpForce;
    }
}
