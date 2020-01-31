using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _smoothing;

    void FixedUpdate()
    {
        Vector3 newPos = new Vector3(_target.position.x, _target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPos, (_smoothing * 0.01f));
    }
}
