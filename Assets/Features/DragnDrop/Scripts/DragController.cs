using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class DragController : MonoBehaviour {
    //Create Event
    public delegate void DragAction();

    public static event DragAction OnDragBegin;
    public static event DragAction OnDragEnd;
    
    //Set up serialized fields
    [SerializeField] private Camera newCamera;
    [SerializeField] private int maxDistance = 100;
    [SerializeField] private GameObject oldTargetIndicator;
    [SerializeField] private GameObject newTargetIndicator;

    //fields
    [SerializeField] private bool isDragging = false;

    void FixedUpdate() {
        Ray ray = newCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, maxDistance)) {
            if (Input.GetMouseButton(0)) {
                Debug.DrawLine(ray.origin, hit.point);
                StartDragging(hit);
            }
            else {
                if(isDragging)
                    StopDragging(hit);
            }
        }
    }

    private Vector3 _startLocation;
    private GameObject _oldLocIndicator;
    private GameObject _newLocIndicator;
    private Transform _heldObject = null;

    void StartDragging(RaycastHit hit) {
        if (!isDragging && hit.transform.TryGetComponent(out Draggable draggable)) {
            //Track and hold the objects old location and the object itself
            _heldObject = hit.transform;
            _startLocation = draggable.transform.localPosition;
            
            //Place a dummy object on the old location for refference
            _oldLocIndicator = Instantiate(
                oldTargetIndicator, 
                _startLocation, 
                quaternion.identity);
            _newLocIndicator = Instantiate(
                newTargetIndicator, 
                hit.point, 
                quaternion.identity);
            //Trigger the drag event which makes all Draggable objects disable collision
            if (OnDragBegin != null) OnDragBegin();
            isDragging = true;
            
        } else if (isDragging) {
            //Set the location of the held object to the cursor
            if (hit.transform.TryGetComponent(out DragCell cell)) {
                _gizmoPosition = cell.transform.position;
                _gizmoText1 = "IsValid: " + cell.IsValid;
                if (_heldObject != null && _newLocIndicator != null) 
                    _newLocIndicator.transform.position = cell.transform.position;
            }
        }
    }

    void StopDragging(RaycastHit hit) {
        if (OnDragEnd != null) OnDragEnd();
        if (_oldLocIndicator) Destroy(_oldLocIndicator);
        if (_newLocIndicator) Destroy(_newLocIndicator);
        
        //Set the location of the held object to the target if valid, else the old location
        if (_heldObject != null) {
            if (hit.transform.TryGetComponent(out DragCell cell) && cell.IsValid) {
                _heldObject.position = cell.transform.position;
                cell.SetObject(_heldObject);
            }
            else {
                _heldObject.transform.position = _startLocation;
            }
        }
        isDragging = false;
    }

    private Vector3 _gizmoPosition = Vector3.zero;
    private string _gizmoText1 = "Temp";
    void OnDrawGizmos() {
        if (_gizmoPosition == Vector3.zero) _gizmoPosition = transform.position;
        Handles.Label(_gizmoPosition, _gizmoText1);
    }
}