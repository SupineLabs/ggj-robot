using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_New_Level : MonoBehaviour
{
    public GameObject nextLevel;
    public GameObject player;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            player.transform.position = nextLevel.transform.position;
        }
    }
}
