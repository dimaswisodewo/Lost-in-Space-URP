using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tweening : MonoBehaviour
{
    public static Tweening Instance;
    public float moveDuration = 1f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void MoveToPosition(Rigidbody2D rb, Vector2 destination)
    {
         rb.DOMove(destination, moveDuration);
    }

    public void RotateToDegree(Transform transform, Vector3 degree, float duration)
    {
        transform.DORotate(degree, duration, RotateMode.FastBeyond360);
    }
}
