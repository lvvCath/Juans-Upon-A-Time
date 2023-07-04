using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BgMusicCont : MonoBehaviour
{

    public GameObject music;

    void Awake()
    {

        DontDestroyOnLoad(music);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if ((scene.buildIndex != 4) && (scene.buildIndex != 5) && (scene.buildIndex != 6))
        {
            Destroy(music);
        }
    }
}
