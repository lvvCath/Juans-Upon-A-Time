using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Title Chapter SceneTransition Timed after 5 sec...

public class LoadNewScene : MonoBehaviour
{

    void Start()
    {
        Invoke("MyLoadingFunction", 5f);
    }
    void MyLoadingFunction()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
