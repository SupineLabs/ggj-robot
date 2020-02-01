using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_And_Door_Interactions : MonoBehaviour
{
    public Key key;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Key")
        {
            key = col.GetComponent<Key>();
            key.isPickedUp = true;
        }

        if (col.name == "Door" && key != null) {

            Door door = col.GetComponent<Door>();
            door.isOpen = true;

            key.isUsed = true;
            key = null;
        }
    }
}
