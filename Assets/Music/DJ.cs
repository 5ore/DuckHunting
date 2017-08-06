using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DJ : MonoBehaviour {

    public AudioClip[] songs;

    private bool StartPlaying = false;

    private AudioSource audioSource;

    private void OnLevelWasLoaded(int level)
    {
        if (level == 1 && GetComponent<DontDestroyOnLoad>())
            Destroy(gameObject);

        if (level == 2)
            gameObject.AddComponent<DontDestroyOnLoad>();

    }

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = songs[Random.Range(0, songs.Length - 1)];
    }
    // Update is called once per frame
    void Update () {
        if (!audioSource.isPlaying && !StartPlaying)
        {
            StartCoroutine(WaitForNextSong());
        }
	}

    IEnumerator WaitForNextSong()
    {
        StartPlaying = true;
        yield return new WaitForSeconds(1f);
        audioSource.clip = songs[Random.Range(0, songs.Length - 1)];
        audioSource.Play();
        StartPlaying = false;
    }
}
