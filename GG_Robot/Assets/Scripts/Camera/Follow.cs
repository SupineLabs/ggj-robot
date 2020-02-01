using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    public static float _smoothing = 5f;

    void FixedUpdate()
    {
        Vector3 newPos = new Vector3(_target.position.x, _target.position.y, _target.position.z - 10f);
        transform.position = Vector3.Lerp(transform.position, newPos, (_smoothing * 0.01f));
    }
}
