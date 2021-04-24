using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    [SerializeField] Sound[] sounds;
    Sound currentMusic;
    private void Awake() {
        SetUpSingleton();
        foreach (Sound i in sounds){
            i.audioSource = gameObject.AddComponent<AudioSource>();
            i.audioSource.clip = i.audioClip;
            i.audioSource.loop = i.loop;
            i.audioSource.spatialBlend = 0;
        }
        PlaySound("MenuMusic");
    }
    private void SetUpSingleton() {
        int numAudioManagers = FindObjectsOfType<AudioManager>().Length;
        if (numAudioManagers > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void PlaySound(string name) {
        if (currentMusic != null && currentMusic.GetName() == name) return;
        foreach(Sound i in sounds) {
            if (i.GetName() != name) {
                continue;
            } else {
                if (i.IsMusic() || currentMusic == null) {
                    if(currentMusic != null)
                        currentMusic.audioSource.Stop();
                    currentMusic = i;
                    currentMusic.audioSource.Play();
                } else {
                    i.audioSource.Play();
                }
                return;
            }
        }
    }
}
