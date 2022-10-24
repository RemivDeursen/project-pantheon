using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCell : MonoBehaviour {
    [SerializeField] private bool isValid = true;
    private Transform _heldObject;

    public bool IsValid {
        get => isValid;
        set => isValid = value;
    }

    private void OnEnable() {
        DragController.OnDragEnd += CheckCurrentObject;
    }

    private void CheckCurrentObject() {
        if (!_heldObject) {
            isValid = true;
        }else if (_heldObject && transform.position == _heldObject.position) {
            isValid = false;
        } else if (_heldObject && transform.position != _heldObject.position) {
            ClearObject();
        }
    }

    public Transform GetObject() {
        if (_heldObject == null) {
            return transform;
        }

        return _heldObject;
    }

    public void SetObject(Transform obj) {
        _heldObject = obj;
        isValid = false;
    }

    public void ClearObject() {
        _heldObject = null;
        isValid = true;
    }
}