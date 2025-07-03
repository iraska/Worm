using UnityEngine;

public class EffectsController : MonoBehaviour
{
    public static EffectsController Instance;

    [SerializeField] AudioSource bgmSource, sfxSource;
    [SerializeField] AudioClip collectClip, winClip;

    float lastCollectTime = 0f;
    float collectCooldown = 0.3f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        PlayBGM();
    }

    public void PlayBGM()
    {
        if (bgmSource != null && !bgmSource.isPlaying)
            bgmSource.Play();
    }

    public void StopBGM()
    {
        if (bgmSource != null && bgmSource.isPlaying)
            bgmSource.Stop();
    }

    public void PlayCollectSound()
    {
        if (Time.time - lastCollectTime > collectCooldown)
        {
            if (sfxSource != null && collectClip != null)
            {
                sfxSource.PlayOneShot(collectClip);
                lastCollectTime = Time.time;
            }
        }
    }

    public void PlayWinSound()
    {
        if (sfxSource != null && winClip != null)
            sfxSource.PlayOneShot(winClip);
    }
}
