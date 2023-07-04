using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Prologue1 : MonoBehaviour
{
    public VideoClip videoToPlay;
    private VideoPlayer videoPlayer;
    private AudioSource audioSource;
    // Use this for initialization

    void Start()
    {
        Application.runInBackground = false;
        StartCoroutine(playVideo());
      
    }
    IEnumerator playVideo()
    {

        GameObject camera = GameObject.Find("Main Camera");
        var videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        audioSource = gameObject.AddComponent<AudioSource>();
     
        videoPlayer.playOnAwake = true;
        audioSource.playOnAwake = true;
        audioSource.Pause();
        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.skipOnDrop = false;
        videoPlayer.waitForFirstFrame = false;
 
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
 
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);
 
        videoPlayer.clip = videoToPlay;
        videoPlayer.Prepare();
 
        WaitForSeconds waitTime = new WaitForSeconds(0);
        while (!videoPlayer.isPrepared)
        {
            Debug.Log("Preparing Video");
 
            yield return waitTime;
 
            break;
        }
        Debug.Log("Done Preparing Video");     
 
        videoPlayer.Play();
 
        audioSource.Play();
        Debug.Log("Playing Video");

        videoPlayer.loopPointReached += EndReached;
    }

  

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}

