using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject fadeInPanel;

    public void PlayGame ()
	{
        if (fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel,
                Vector3.zero,
                Quaternion.identity) as GameObject;
            Destroy(panel, 8);
        }


        SoundManagerScript.PlaySound("Decision1");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
       
    }

	public void QuitGame ()
	{
        SoundManagerScript.PlaySound("Cursor3");
		Debug.Log("Quit");
		Application.Quit();
	}

    public void About()
    {
        Application.OpenURL("https://juans-upon-a-time.github.io/");
    }


    public void Credits()
    {
        Application.OpenURL("https://juans-upon-a-time.github.io/About.html");
    }

    public void BackMainMenu()
    {
        SoundManagerScript.PlaySound("Decision1");
        Application.LoadLevel("StartMenu");
    }

    public void Chapterone()
    {
        if (fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel,
                Vector3.zero,
                Quaternion.identity) as GameObject;
            Destroy(panel, 8);
        }


        SoundManagerScript.PlaySound("Decision1");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void GameOver()
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
    }

}
