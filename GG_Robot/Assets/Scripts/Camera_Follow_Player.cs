using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow_Player : MonoBehaviour
{
    public GameObject player;
    private float speed = 0.005f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z - 5f);

        if (transform.position != newPos)
        {
            transform.position = Vector3.Lerp(transform.position, newPos, speed);
        }
    }
}
