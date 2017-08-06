using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicOptions : MonoBehaviour {

    public static bool musicON = true;
    public static bool soundON = true;

    [SerializeField]
    private Slider musicVolume;
    [SerializeField]
    private Slider soundVolume;
    [SerializeField]
    private GameObject[] musicObjects;
    [SerializeField]
    private GameObject[] soundObjects;

    enum Type
    {
        Sound,
        Music
    };

    private void Start()
    {
        StartCoroutine(findSound("SoundMaker", Type.Sound)); StartCoroutine(findSound("MusicMaker", Type.Music));
    }

    IEnumerator findSound(string tag, Type type)
    {
        GameObject[] startingObjects = GameObject.FindGameObjectsWithTag(tag);
        while (true)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
            if (objects.Length != startingObjects.Length)
            {
                startingObjects = objects;
                switch (type)
                {
                    case Type.Sound:
                        soundObjects = objects;
                        break;
                    case Type.Music:
                        musicObjects = objects;
                        break;
                }
                UpdateVolume();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void UpdateVolume()
    {
        StartCoroutine(updateVolume(musicObjects,musicVolume));
        StartCoroutine(updateVolume(soundObjects,soundVolume));
    }

    private IEnumerator updateVolume(GameObject[] objects,Slider volume)
    {
        AudioSource audioSource;
        foreach (GameObject gameObject in objects)
        {
            audioSource = gameObject.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("There is no audio source component attached to the game object: " + gameObject.name);
                continue;
            }
            audioSource.volume = volume.value;
        }

        yield return null;
    }
}
