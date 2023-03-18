using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private AudioSource mainSound;
    public static AudioManager instance;
    public SoundEffectSO[] soundSO;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixer;
            mainSound = GetComponent<AudioSource>();
        }

    }
    void Start()
    {

        Play("Background");

    }

    void Update()
    {
        // if(SceneManager.GetActiveScene().name == "Start Menu")
        //   PauseSound("Background");
        //   else
        //   UnpauseSound("Background");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound" + name + " not found");
            return;
        }
        s.source.Play();

    }

    public void UnpauseSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound" + name + " not found");
            return;
        }
        s.source.UnPause();
    }

    public void PlayOneShot(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound" + name + " not found");
            return;
        }
        s.source.PlayOneShot(s.source.clip);
    }
    public void PauseSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound" + name + " not found");
            return;
        }


        s.source.Pause();
    }


    public void SelectedPause()
    {

    }

    public void SelectedUnPause()
    {

    }

    public void StopAudio(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound" + name + " not found");
            return;
        }
        s.source.Stop();
    }
    public void PlaySO(int id)
    {
        for (int i = 0; i < soundSO.Length; i++)
        {
            {
                soundSO[id].PlaySO();
            }
        }
    }



    public void DisableAudios()
    {


    }
}