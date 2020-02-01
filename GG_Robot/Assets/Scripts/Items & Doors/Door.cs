using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    public Sprite openSprite;
    public BoxCollider2D barrier;

    private SpriteRenderer doorSprite;

    void Start()
    {
        doorSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isOpen)
        {
            Destroy(gameObject);
        }
    }
}
