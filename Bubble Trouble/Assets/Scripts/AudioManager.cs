using UnityEngine;
using System.Collections;


public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;
    
    [SerializeField] private AudioClip bgmClip;
    [SerializeField] private AudioClip boilClip;
    [SerializeField] private AudioClip witchLaughClip;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);

        // Setup audio sources if not assigned
        if (bgmSource == null)
        {
            bgmSource = gameObject.AddComponent<AudioSource>();
            bgmSource.loop = true;
            bgmSource.volume = 0.5f; // Set BGM volume to 50%
            bgmSource.playOnAwake = false;
        }
        
        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.volume = 1f;
        }

        // Load audio clips if not assigned in inspector
        if (bgmClip == null)
        {
            bgmClip = Resources.Load<AudioClip>("pixely_exciting_background");
        }
        if (boilClip == null)
        {
            boilClip = Resources.Load<AudioClip>("boil");
        }
        if (witchLaughClip == null)
        {
            witchLaughClip = Resources.Load<AudioClip>("witch-laugh");
        }
    }


    private void Start()
    {
        PlayBGM();
    }

    public void PlayBGM()
    {
        if (bgmClip != null)
        {
            bgmSource.clip = bgmClip;
            if (!bgmSource.isPlaying)
            {
                bgmSource.Play();
                Debug.Log("Started playing BGM");
            }
        }
        else
        {
            Debug.LogWarning("BGM clip is not assigned!");
        }
    }


    public void PlayBoilSound()
    {
        if (boilClip != null)
        {
            sfxSource.clip = boilClip;
            sfxSource.Play();
            StartCoroutine(StopSoundAfterDelay(1.5f));
        }
    }

    private IEnumerator StopSoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        sfxSource.Stop();
    }


    public void PlayWitchLaugh()
    {
        if (witchLaughClip != null)
        {
            sfxSource.PlayOneShot(witchLaughClip);
        }
    }
}
