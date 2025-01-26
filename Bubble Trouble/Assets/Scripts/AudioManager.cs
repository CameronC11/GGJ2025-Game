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
    [SerializeField] private AudioClip screamClip;
    [SerializeField] private AudioClip popClip;



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
        if (screamClip == null)
        {
            screamClip = Resources.Load<AudioClip>("cartoon-scream-1-6835");
        }
        if (popClip == null)
        {
            popClip = Resources.Load<AudioClip>("pop-39222");
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


    public void PlayPopAndLaugh()
    {
        if (popClip != null)
        {
            // Create a temporary AudioSource for the pop sound
            AudioSource tempSource = gameObject.AddComponent<AudioSource>();
            tempSource.clip = popClip;
            tempSource.time = 0.22f; // Start 0.1 seconds into the clip
            tempSource.Play();
            
            // Clean up the temporary AudioSource after it's done
            StartCoroutine(CleanupTempAudioSource(tempSource, popClip.length - 0.1f));
            StartCoroutine(PlayLaughAfterDelay(0.2f));
        }
    }

    private IEnumerator CleanupTempAudioSource(AudioSource tempSource, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(tempSource);
    }



    private IEnumerator PlayLaughAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (witchLaughClip != null)
        {
            sfxSource.PlayOneShot(witchLaughClip);
        }
    }

    // Keep this for backward compatibility
    public void PlayWitchLaugh()
    {
        if (witchLaughClip != null)
        {
            sfxSource.PlayOneShot(witchLaughClip);
        }
    }


    public void PlayScream()
    {
        if (screamClip != null)
        {
            sfxSource.PlayOneShot(screamClip);
        }
    }

}
