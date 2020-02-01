using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_New_Level : MonoBehaviour
{
    public GameObject nextLevelPlayerPos;
    public GameObject player;

    public GameObject currentLevel;
    public GameObject nextLevel;

    //public GameObject nextLevelCameraPosition;
    //public GameObject mainCamera;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            nextLevel.SetActive(true);
            currentLevel.SetActive(false);
            player.transform.position = nextLevelPlayerPos.transform.position;
            //moveCam = true;
        }
    }
}
