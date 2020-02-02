using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TriggerNextLevel : MonoBehaviour
{
    public GameObject nextLevelPlayerPos;
    public GameObject player;

    public GameObject currentLevel;

    public TilemapCollider2D nextFloorCollider;

    public GameObject interactables;
    public Transform[] interactablesChildren;

    private void Start()
    {
        interactablesChildren = interactables.GetComponentsInChildren<Transform>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Follow._smoothing = 2f;

            nextFloorCollider.enabled = true;
            currentLevel.SetActive(false);
            AudioManager.Instance.Play("Teleport");
            collision.gameObject.transform.position = nextLevelPlayerPos.transform.position;


            foreach (Transform go in interactablesChildren)
            {
                GameObject transformObject = go.gameObject;
                if (transformObject.GetComponent<BoxCollider2D>() != null)
                {
                    Debug.Log(transformObject);
                    if (!transformObject.GetComponent<BoxCollider2D>().enabled)
                    {
                        transformObject.GetComponent<BoxCollider2D>().enabled = true;
                        if (transformObject.GetComponentInChildren<PolygonCollider2D>() != null)
                        {
                            transformObject.GetComponentInChildren<PolygonCollider2D>().enabled = true;
                        }
                    }
                }
            }

            Invoke("Reset", 3f);
        }
    }

    private void Reset()
    {
        Follow._smoothing = 5f;
    }
}
