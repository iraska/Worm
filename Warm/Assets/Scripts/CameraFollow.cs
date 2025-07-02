using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset = new Vector3(0, 15, 0);

    void LateUpdate()
    {
        if (target != null)
            transform.position = target.position + offset;
    }     
}