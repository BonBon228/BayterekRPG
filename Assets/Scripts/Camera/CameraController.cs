using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [Range(1,10)]

    private bool _isFight = true;
    public float smoothFactor;    
    public Vector3 minValues, maxValues;

    private void FixedUpdate() {
        Follow();
    }

    public void changeBool() {
        _isFight = !_isFight;
    }

    private void Follow() {
        if(_isFight) {
            Vector3 targetPosition = target.position + offset;
            Vector3 boundPosition = new Vector3(
                Mathf.Clamp(targetPosition.x, minValues.x, maxValues.x),
                Mathf.Clamp(targetPosition.y, minValues.y, maxValues.y),
                Mathf.Clamp(targetPosition.z, minValues.z, maxValues.z));

            transform.position = Vector3.Lerp(transform.position, boundPosition, smoothFactor*Time.deltaTime);
        }else {
            transform.position = new Vector3(127, 0, -10);
        }
    }
}
