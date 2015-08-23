using UnityEngine;
using System.Collections.Generic;

public class MusicPlayer : MonoBehaviour {

    public AudioSource[] musicLayers;
    public AudioSource spottedMusic;

    public float volumeSpeed = 3;
    public float spottedSpeed = 1;
    public float volumeMultiplier = .2f;

    float targetVol = 0;
    float currentVol = 0;
    float currentSpotted = 0;
    float targetSpotted = 0;

    //we might as well store our scores here, since this object gets carried between scenes.
    public float finalScore = 0;
    public List<string> letterGrades;

    //keep alive between scenes, but don't duplicate.
    private static MusicPlayer instance = null;
    public static MusicPlayer Instance
    {
        get { return instance; }
    }
    void Awake() {
         if (instance != null && instance != this) {
             Destroy(this.gameObject);
             return;
         } 
        else {
             instance = this;
         }
         DontDestroyOnLoad(this.gameObject);
     }

	// Use this for initialization
	void Start () {
        foreach(AudioSource a in musicLayers) {
            a.Play();
        }
        spottedMusic.Play();
        letterGrades = new List<string>();
	}
	
	// Update is called once per frame
	void Update () {
        //keep music synced
        for(int i = 1; i < musicLayers.Length; i++) {
            musicLayers[i].timeSamples = musicLayers[0].timeSamples;
        }
        spottedMusic.timeSamples = musicLayers[0].timeSamples;
        //smoothly change volume
        if (currentVol != targetVol)
        {
            if (currentVol < targetVol)
            {
                if (currentVol + Time.deltaTime * volumeSpeed > targetVol)
                    currentVol = targetVol;
                else
                    currentVol += Time.deltaTime * volumeSpeed;
            }
            else
            {
                if (currentVol - Time.deltaTime * volumeSpeed < targetVol)
                    currentVol = targetVol;
                else
                    currentVol -= Time.deltaTime * volumeSpeed;
            }
        }

        //change music volumes
        musicLayers[0].volume = 1;
        for(int i = 1; i < musicLayers.Length; i++) {
            float iVolume = currentVol * volumeMultiplier * (musicLayers.Length - 1) - (i - 1);
            iVolume = Mathf.Max(0, iVolume);
            iVolume = Mathf.Min(1, iVolume);
            musicLayers[i].volume = iVolume;
        }

        //spotted music
        if (currentSpotted != targetSpotted)
        {
            if (currentSpotted < targetSpotted)
            {
                if (currentSpotted + Time.deltaTime * volumeSpeed > targetSpotted)
                    currentSpotted = targetSpotted;
                else
                    currentSpotted += Time.deltaTime * spottedSpeed;
            }
            else
            {
                if (currentSpotted - Time.deltaTime * spottedSpeed < targetSpotted)
                    currentSpotted = targetSpotted;
                else
                    currentSpotted -= Time.deltaTime * spottedSpeed;
            }
        }

        spottedMusic.volume = currentSpotted;
	}

    public void setVolume(float vol)
    {
        targetVol = vol;
    }

    public void setSpotted(float vol)
    {
        targetSpotted = vol;
    }
}
