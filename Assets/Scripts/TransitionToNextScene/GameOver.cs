using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void GameOverback()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
    }

    public void EndToMain()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
    }
}
