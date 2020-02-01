using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    [SerializeField]
    private GameObject _activeCheckpoint;

    [SerializeField]
    private Vector3 _spawnLocation;

    public static GameManager Instance { get => _instance; set => _instance = value; }
    public Vector3 SpawnLocation { get => _spawnLocation; set => _spawnLocation = value; }
    public GameObject ActiveCheckpoint { get => _activeCheckpoint; set => _activeCheckpoint = value; }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(_instance.gameObject);
            _instance = this;
        }
    }

    public void ResetPlayer()
    {
        Stats.Instance.gameObject.transform.position = _activeCheckpoint.transform.position;
        Camera.main.transform.position = new Vector3(_spawnLocation.x, _spawnLocation.y, -10);
    }
}
