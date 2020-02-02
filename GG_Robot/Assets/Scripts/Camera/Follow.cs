using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField]
    public Transform target;

    public static float _smoothing = 5f;

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 newPos = new Vector3(target.position.x, target.position.y, target.position.z - 10f);
            transform.position = Vector3.Lerp(transform.position, newPos, (_smoothing * 0.01f));
        }
    }
}
