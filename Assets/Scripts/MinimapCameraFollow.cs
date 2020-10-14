using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraFollow : MonoBehaviour
{
    public Transform target;

    private void LateUpdate()
    {
        Vector2 newPosition = target.position;
        transform.position = newPosition;
    }
}
