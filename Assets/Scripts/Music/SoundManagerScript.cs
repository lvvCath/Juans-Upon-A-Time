using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    public static AudioClip ButtonSfx, PlaySfx;
    static AudioSource audioSrc;
    public static SoundManagerScript instance;

    void Start()
    {
        if (instance == null)
            instance = this;
        else {
            Destroy(gameObject);
            return;
            }

        DontDestroyOnLoad(gameObject);

        ButtonSfx = Resources.Load<AudioClip>("Cursor3");
        PlaySfx = Resources.Load<AudioClip>("Decision1");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip) {
        switch (clip) {
            case "Cursor3":
                audioSrc.PlayOneShot(ButtonSfx);
                break;
            case "Decision1":
                audioSrc.PlayOneShot(PlaySfx);
                break;

        }
    }

}
