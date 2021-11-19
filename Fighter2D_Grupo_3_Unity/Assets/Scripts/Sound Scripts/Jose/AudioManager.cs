using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour{

    public Sound[] sounds;
    public static AudioManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        foreach(Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string name, float v){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s != null){
            Debug.Log("Reproduce" + name);
            s.source.volume = v;
            s.source.Play();
        }else{
            Debug.Log("NO existe" + name);
        }
    }
}
