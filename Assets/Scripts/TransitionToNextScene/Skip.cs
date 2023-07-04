using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Skip : MonoBehaviour
{
    public void SkipPrologue()
    {

        Application.LoadLevel("TitleChapterOne");
        SoundManagerScript.PlaySound("Decision1");
       

    }

    public void SkipNextScene()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SoundManagerScript.PlaySound("Decision1");


    }
}
