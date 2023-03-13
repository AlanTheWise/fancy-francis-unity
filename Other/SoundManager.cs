using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager sharedInstance;

    public AudioSource[] sounds;
    [HideInInspector] public List<AudioSource> pausedAudios;

    private void Start() {
        pausedAudios = new List<AudioSource>();
    }

    private void Awake() {
        if(sharedInstance == null){
            sharedInstance = this;
        }
    }

    public void PauseAll(){
        for(int i = 0; i < sounds.Length; i++){
            if(sounds[i].isPlaying){
                sounds[i].Pause();
                pausedAudios.Add(sounds[i]);
            }
        }
    }

    public void UnPauseAll(){
        foreach (AudioSource audio in pausedAudios){
            audio.UnPause();
        }
        pausedAudios.Clear();
    }
}
