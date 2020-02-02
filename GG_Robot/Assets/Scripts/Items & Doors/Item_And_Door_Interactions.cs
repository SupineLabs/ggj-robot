using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_And_Door_Interactions : MonoBehaviour
{
    public static float jumpModifier = 10f;
    public static float speedModifier = 10f;

    public Item item;

    private int itemLayer = 10;
    private int playerLayer = 8;

    void Update() {

        // Drop currently held item.
        if (item != null && Input.GetKey(KeyCode.E)) {

            item.isPickedUp = false;

            if (item.isUpgrade)
            {
                // Remove the upgrade effect e.g. set jump height back to default.
            }

            item = null;  
        }
    }

    public bool hasItem { get => item != null; }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Picking up an item.
        if (item == null && col.GetComponent<Item>() != null)
        {
            item = col.GetComponent<Item>();
            item.isPickedUp = true;

            if (item.isUpgrade)
            {
                // Apply upgrade effects e.g. increased jump height.
            }
        }

        // Unlocking a door.
        if (col.name == "Door" && item != null && item.gameObject.name == "Key") {

            Door door = col.GetComponent<Door>();
            door.isOpen = true;

            item.isUsed = true;
            item = null;
        }
    }
}
