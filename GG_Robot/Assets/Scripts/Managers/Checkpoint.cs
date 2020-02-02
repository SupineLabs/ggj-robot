using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public GameObject OverrideLocation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (this.OverrideLocation != null)
            {
                GameManager.Instance.ActiveCheckpoint = this.OverrideLocation;
            } else
            {
                GameManager.Instance.ActiveCheckpoint = this.gameObject;
            }
        }
    }
}
