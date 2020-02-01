using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject player;

    // Line drawing.
    private LineRenderer lineRend;
    private Vector2 l_start;
    private Vector2 l_end;


    // Player position, my position, ideal next position,.
    private Vector3 p_pos;
    private Vector3 pos;
    private Vector3 i_pos;

    // Offsets to get ideal position.
    private float x_off = 0.5f;
    private float y_off = 0f;
    private float right = -0.5f;
    private float left = 0.5f;

    // Other.
    public bool isPickedUp = false;
    public bool isUsed = false;
    public float speed = 0.1f;

    // Update is called once per frame.
    void Update()
    {
        // Update position + distance between player and me.
        p_pos = player.transform.position;
        i_pos = new Vector3(p_pos.x - x_off, p_pos.y - y_off, p_pos.z);
        pos = transform.position;
        Vector3 to_p = pos - p_pos;


        // Is the player moving left or right?
        float x = Input.GetAxis("Horizontal");

        if (x < 0)
        {
            x_off = right;
        }
        //else if (x is 0)
        //{
        //    
        //}
        else if (x > 0)
        {
            x_off = left;
        }

        if (isPickedUp)
        {
            // Move towards players positions.
            transform.position = Vector3.Lerp(pos, i_pos, speed);
        }

        if (isUsed)
        {
            Destroy(gameObject);
        }
    }
}
