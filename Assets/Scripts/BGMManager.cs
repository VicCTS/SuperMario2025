using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private AudioSource _audioSource;

    public AudioClip gameMusic;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartBGM();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartBGM()
    {
        _audioSource.loop = true;
        _audioSource.clip = gameMusic;
        _audioSource.Play();

        //_audioSource.Pause();
        //_audioSource.Stop();
    }
}
