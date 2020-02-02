using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public AudioMixerGroup fx;
    [Header("Sound Lists")]
    [Tooltip("Sounds to be played locally. E.G Music, UI Effects. Usually non-diegetic audio.")]
    public Sound[] localSounds;

    public static AudioManager Instance { get => _instance; set => _instance = value; }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(_instance.gameObject);
            _instance = this;
        }

        foreach (Sound s in localSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.priority = s.priority;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.outputAudioMixerGroup = fx;
            s.source.spatialBlend = 0;
            s.source.loop = s.loop;
        }
    }

    #region LocalSoundControls
    /// <summary>
    /// Plays local sounds defined in the audio manager.
    /// </summary>
    /// <param name="name"></param>
    public void Play(string name)
    {
        Sound s = Array.Find(localSounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Failed Playing. Sound: " + name + " not found! maybe you misspelled it dummy");
        }

        s.source.Play();
    }

    public bool IsPlaying(string name)
    {
        Sound s = Array.Find(localSounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Failed Return. Sound: " + name + " not found! maybe you misspelled it dummy");
        }

        return s.source.isPlaying;
    }
    public float Time(string name)
    {
        Sound s = Array.Find(localSounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Failed Return. Sound: " + name + " not found! maybe you misspelled it dummy");
        }

        return s.source.time;
    }


    public void Pause(string name)
    {
        Sound s = Array.Find(localSounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Falied Pausing. Sound: " + name + " not found! maybe you misspelled it dummy");
        }

        s.source.Pause();
    }
    public void Volume(string name, float value)
    {
        Sound s = Array.Find(localSounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Falied Pausing. Sound: " + name + " not found! maybe you misspelled it dummy");
        }

        s.source.volume = value;
    }
    public void Pitch(string name, float value)
    {
        Sound s = Array.Find(localSounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Falied Pausing. Sound: " + name + " not found! maybe you misspelled it dummy");
        }

        s.source.pitch = value;
    }
    public float getVolume(string name)
    {
        Sound s = Array.Find(localSounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Falied Pausing. Sound: " + name + " not found! maybe you misspelled it dummy");
        }

        float volume = s.source.volume;
        return volume;
    }
    public float getPitch(string name)
    {
        Sound s = Array.Find(localSounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Falied Pausing. Sound: " + name + " not found! maybe you misspelled it dummy");
        }

        float pitch = s.source.pitch;
        return pitch;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
