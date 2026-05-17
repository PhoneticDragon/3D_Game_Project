using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Source")]
    public AudioSource sfxSource;

    [Header("UI Sounds")]
    public AudioClip buttonClick;

    [Header("Gameplay Sounds")]
    public AudioClip damageSound;
    public AudioClip checkpointSound;
    public AudioClip laserHum;

    private void Awake()
    {
        Instance = this;
    }

    // UI
    public void PlayButtonClick()
    {
        sfxSource.PlayOneShot(buttonClick);
    }

    // Player feedback
    public void PlayDamage()
    {
        sfxSource.PlayOneShot(damageSound);
    }

    public void PlayCheckpoint()
    {
        sfxSource.PlayOneShot(checkpointSound);
    }
}