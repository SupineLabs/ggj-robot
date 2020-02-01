using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death_Zone : MonoBehaviour
{
    public string sceneName;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Death Zone") {

            SceneManager.LoadScene(sceneName);
        }
    }
}
