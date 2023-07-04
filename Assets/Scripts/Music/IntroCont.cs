using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCont : MonoBehaviour
{

    public GameObject music;

    void Awake()
    {

        DontDestroyOnLoad(music);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if ((scene.buildIndex != 0) && (scene.buildIndex != 1))
        {
            Destroy(music);
        }
    }
}
