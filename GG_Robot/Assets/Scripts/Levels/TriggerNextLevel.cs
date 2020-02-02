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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            Follow._smoothing = 2f;

            nextFloorCollider.enabled = true;
            currentLevel.SetActive(false);
            AudioManager.Instance.Play("Teleport");
            player.transform.position = nextLevelPlayerPos.transform.position;

            Invoke("Reset", 3f);
        }
    }

    private void Reset()
    {
        Follow._smoothing = 5f;
    }
}
