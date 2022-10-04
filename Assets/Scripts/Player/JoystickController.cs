using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : Controller, IDragHandler, IEndDragHandler
{
    Vector3 _moveDir;
    Vector3 _moveDirModified;
    Vector3 _initPosition;
    [SerializeField] float _maxMagnitude;

    void Start()
    {
        _initPosition = transform.position;
    }

    public override Vector3 GetMovementInput()
    {
        _moveDirModified = new Vector3(_moveDir.x, 0f, _moveDir.y) / _maxMagnitude;

        return _moveDirModified;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _moveDir = Vector3.ClampMagnitude((Vector3)eventData.position - _initPosition, _maxMagnitude);

        transform.position = _initPosition + _moveDir;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = _initPosition;
        _moveDir = Vector3.zero;
    }
}