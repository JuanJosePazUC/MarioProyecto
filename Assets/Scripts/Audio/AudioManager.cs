using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour{
    public Sound[] sounds;

    private void Awake() {

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start() {
        Play("MainTheme");
    }

    public void Play (string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null){
            Debug.LogWarning("Sound: "+ name +" not found!");
            return;
        }
        s.source.Play();
    }

    public void Mute (string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null){
            Debug.LogWarning("Sound: "+ name +" not found!");
            return;
        }

        if(s.source.mute){
            s.source.mute = false;
        }else{
            s.source.mute = true;
        }
    }

    public void Pause (string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null){
            Debug.LogWarning("Sound: "+ name +" not found!");
            return;
        }

        if(s.source.pitch == 1){
            s.source.pitch = 0;
        }else{
            s.source.pitch = 1;
        }
    }

}
