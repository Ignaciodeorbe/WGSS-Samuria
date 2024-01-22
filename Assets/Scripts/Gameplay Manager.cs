using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    SwordMovement swordMovementScript;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    List<AudioClip> listSounds = new List<AudioClip>();

    List<AudioClip> gameplaySounds = new List<AudioClip>();

    [SerializeField]
    AudioClip scream;

    float timerScale = 3;

    float soundTimer = 1;
    int soundCounter = 0;

    int level = 0;

    bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        NextLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            soundTimer -= Time.deltaTime;

            if(soundTimer < 0)
            {
                
                audioSource.Play();
                if (soundCounter == gameplaySounds.Count - 1)
                {
                    swordMovementScript.CanMove = true;
                }
                else
                {
                    soundCounter++;
                    audioSource.clip = gameplaySounds[soundCounter];
                    SetTimer();
                }
            }
        }
    }

    public void SetTimer()
    {
        soundTimer *= timerScale + Random.Range(0.5f,0.8f); 
    }

    public void NextLevel()
    {
        level++;
        MakeSounds();
        SetTimer();
        started= true;
    }

    public void MakeSounds()
    {
        soundCounter = 0;

        for(int i = 0; i < level; i++)
        {
            gameplaySounds.Add(listSounds[Random.Range(0, listSounds.Count)]);
        }

        gameplaySounds.Add(scream);

        audioSource.clip = gameplaySounds[soundCounter];
    }
}
