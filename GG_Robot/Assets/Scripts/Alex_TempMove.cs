using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alex_TempMove : MonoBehaviour
{

    private float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        Vector3 move = transform.right * x;

        transform.position += move * speed * Time.deltaTime;
    }
}
