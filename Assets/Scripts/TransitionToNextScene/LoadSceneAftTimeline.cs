using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//From Title Chapter Scene to UnangKabanata02 Scene

public class LoadSceneAftTimeline : MonoBehaviour
{
    public void LoadNewScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("UnangKabanata02");
    }


}

