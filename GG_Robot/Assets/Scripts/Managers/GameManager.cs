using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public GameEvent FadeOut;

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

    private void Start()
    {
        AudioManager.Instance.Play("Music");
        AudioManager.Instance.Play("Engine");
    }

    public void ResetPlayer()
    {
        FadeOut.Raise();
        // stop camera following player so you can see them drop out of the world
        Camera.main.GetComponent<Follow>().target = null;
        StartCoroutine(Unhide());
    }

    public IEnumerator Unhide()
    {
        yield return new WaitForSeconds(2f);

        // teleport to last checkpoint
        Stats.Instance.gameObject.transform.position = _activeCheckpoint.transform.position;

        // reset animation so he's sitting there when he restarts
        Stats.Instance.gameObject.GetComponent<Animator>().SetBool("Jump", false);

        // send camera to checkpoint & start following player again
        Camera.main.transform.position = new Vector3(_activeCheckpoint.transform.position.x, _activeCheckpoint.transform.position.y, _activeCheckpoint.transform.position.z - 10);
        Camera.main.GetComponent<Follow>().target = Stats.Instance.gameObject.transform;
        StopAllCoroutines();
    }
}
