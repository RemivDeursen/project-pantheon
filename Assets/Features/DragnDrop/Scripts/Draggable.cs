using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour {
    private Collider _collider;
    private void OnEnable() {
        _collider = GetComponent<BoxCollider>();
        DragController.OnDragBegin += StopCollision;
        DragController.OnDragEnd += StartCollision;
    }

    void StopCollision() {
        _collider.enabled = false;
    }
    void StartCollision() {
        _collider.enabled = true;
    }
}
