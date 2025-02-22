using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource audioSrc => GetComponent<AudioSource>();

    public void PlaySound(AudioClip clip, float volume = 1f, bool destroyed = false, float p1 = 0.6f, float p2 = 1.4f)
    {
        audioSrc.pitch = Random.Range(p1, p2);
        audioSrc.PlayOneShot(clip, volume);
    }
}