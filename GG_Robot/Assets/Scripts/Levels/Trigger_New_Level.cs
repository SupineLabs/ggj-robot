using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_New_Level : MonoBehaviour
{
    public GameObject nextLevel;
    public GameObject nextLevelCameraPosition;
    public GameObject player;
    public GameObject cam;

    //private bool moveCam = false;

    //private float speed = 0.005f;
    //private void Update()
    //{
    //    if (moveCam) {
    //        if (cam.transform.position != nextLevelCameraPosition.transform.position)
    //        {
    //            cam.transform.position = Vector3.Lerp(cam.transform.position, nextLevelCameraPosition.transform.position, speed);
    //        }
    //        else {
    //            moveCam = false;
    //        }
    //    }
    //}

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            player.transform.position = nextLevel.transform.position;
            //moveCam = true;
        }
    }
}
