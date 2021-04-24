using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound {
    [SerializeField] string name;
    public AudioClip audioClip;
    public AudioSource audioSource;
    public bool loop;
    [SerializeField] bool isMusic;
    public string GetName() {
        return name;
    }
    public bool IsMusic() {
        return isMusic;
    }
}
