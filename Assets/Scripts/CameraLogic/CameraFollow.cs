using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _rotationAngleX;
    [SerializeField] private float _distance;
    [SerializeField] private float _offsetY;

    private void LateUpdate()
    {
        if (_target == null) return;

        Quaternion rotation = Quaternion.Euler(_rotationAngleX, 0, 0);

        Vector3 position=rotation * new Vector3(0,0,-_distance) + FolowingPosition();

        transform.rotation= rotation;
        transform.position= position;   
    }
    
    private Vector3 FolowingPosition()
    {
        Vector3 followingPosition= _target.position;
        followingPosition.y += _offsetY;
        return followingPosition;
    }
}
