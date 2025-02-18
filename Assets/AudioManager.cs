using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----------- Audio Source -----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----------- Audio Clip -----------")]
    public AudioClip Audio_BackgroundNoise;
    public AudioClip Audio_Scanner;
    public AudioClip Audio_Pickup;

    private void Start()
    {
        musicSource.clip = Audio_BackgroundNoise;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
