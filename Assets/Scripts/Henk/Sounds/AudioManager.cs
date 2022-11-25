using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public float soundVolume { get; private set; } = StaticVariables.SoundVolume;

    public static AudioManager Instance;

    private void Awake()
    {

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume * soundVolume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        // First run -> Instantiate this one
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            // Destroy the old Instance and set the new one, for the new scene!
            Destroy(Instance.gameObject);
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

    }

    private void Start()
    {
        Play("BackgroundMusic");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + s.name + " was not found!");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + s.name + " was not found!");
            return;
        }
        s.source.loop = false;
        s.source.Stop();
    }

    public bool IsPLaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + s.name + " was not found!");
            return false;
        }
        return s.source.isPlaying;
    }

    public void UpdateVolume(float newVolume)
    {
        AudioSource[] audios = GetComponents<AudioSource>();
        soundVolume = newVolume;
        // There are equal amounts of AudioSources to Sounds
        for (int i = 0; i < audios.Length; i++)
        {
            audios[i].volume = sounds[i].volume * newVolume;
        }
    }
}