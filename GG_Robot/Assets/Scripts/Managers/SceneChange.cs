using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void ChangeToGame()
    {
        SceneManager.LoadScene("UpToDateScene");
    }
    public void winGame()
    {
        SceneManager.LoadScene("Win");
    }
    public void loadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
