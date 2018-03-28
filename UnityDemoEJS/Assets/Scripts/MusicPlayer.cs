using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    //only one music player 
    static MusicPlayer instance;
    static bool AudioBegin = false;


    void Start()
    {

    }
    void Awake(){
        if (instance != null) {
            Destroy(gameObject); //kill duplicate
        }
        else{
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
            PlayMusic();
        }
        
    }
    void Update() {
      
    }

    public void PlayMusic()
    {
        var audioSource = this.GetComponent<AudioSource>();
        audioSource.Play();
        if (!AudioBegin)
            AudioBegin = true;
    }

}
