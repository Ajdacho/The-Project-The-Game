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
    public AudioClip Audio_Paper;
    public AudioClip Audio_Explosion;
    public AudioClip Audio_Fire;
    public AudioClip Audio_Building;
    public AudioClip Audio_Door;

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
